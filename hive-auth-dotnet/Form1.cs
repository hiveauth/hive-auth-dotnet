using System;
using System.Drawing;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;
using Newtonsoft.Json.Linq;

namespace hive_auth_dotnet
{
	public partial class Form1 : Form
	{
		private string m_auth_key;
		private string m_auth_host = "wss://hive-auth.arcange.eu";	// HAS server we are connecting to
		public Form1()
		{
			InitializeComponent();
		}
		void ProcessMessage(string msg)
		{
			Console.WriteLine(msg);
			JObject JMsg = JObject.Parse(msg);

			switch ((string)JMsg["cmd"])
			{
				case "auth_wait":
					// Update QRCode
					string json =
						new JObject(
							new JProperty("account", txtUsername.Text),
							new JProperty("uuid", JMsg["uuid"]),
							new JProperty("key", m_auth_key),
							new JProperty("host", m_auth_host)
							).ToString();

					string URI = "has://auth_req/" + CryptoJS.btoa(json);
					Console.WriteLine(json);
					Console.WriteLine(URI);
					using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
					using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(URI, QRCodeGenerator.ECCLevel.Q))
					using (QRCode qrCode = new QRCode(qrCodeData))
					{
						Bitmap qrCodeImage = qrCode.GetGraphic(20);
						picQRCode.Image = qrCodeImage;
						picQRCode.Visible = true;
					}
					break;
				case "auth_ack":
					try
					{
						picQRCode.Visible = false;
						// Try to decrypt and parse payload data
						string decrypted = CryptoJS.Decrypt((string)JMsg["data"], m_auth_key);
						JObject JData = JObject.Parse(decrypted);
						string token = (string)JData["token"];
						ulong expire = (ulong)JData["expire"];
						MessageBox.Show(string.Format("Authenticated with token: {0}", token),"Success");
					}
					catch (Exception ex)
					{
						picQRCode.Visible = false;
						// Decryption failed - ignore message
						Console.WriteLine("decryption failed", ex.Message);
					}
					break;
				case "auth_nack":
					MessageBox.Show("Authentication refused");
					break;
			}
		}
		static async Task Send(ClientWebSocket socket, string data) =>
			await socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(data)), WebSocketMessageType.Text, true, CancellationToken.None);

		async Task Receive(ClientWebSocket socket)
		{
			var buffer = new ArraySegment<byte>(new byte[2048]);
			do
			{
				WebSocketReceiveResult result;
				using (MemoryStream ms = new MemoryStream())
				{
					do
					{
						result = await socket.ReceiveAsync(buffer, CancellationToken.None);
						ms.Write(buffer.Array, buffer.Offset, result.Count);
					} while (!result.EndOfMessage);

					if (result.MessageType == WebSocketMessageType.Close)
						break;

					ms.Seek(0, SeekOrigin.Begin);
					using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
					{
						ProcessMessage(await reader.ReadToEndAsync());
					}
				}
			} while (true);
		}
		private async void btnLogin_Click(object sender, EventArgs e)
		{
			using (ClientWebSocket ws = new ClientWebSocket())
			{
				try
				{
					// Create a new authentication key
					m_auth_key = Guid.NewGuid().ToString();
					await ws.ConnectAsync(new Uri(m_auth_host), CancellationToken.None);

					// Create auth_req_data
					string auth_req_data =
						new JObject(
							new JProperty("app",
								new JObject(
									new JProperty("name", "has-demo-dotnet"),
									new JProperty("description", "Demo - HiveAuth with .NET")
								)
							)
						//,
						//new JProperty("token", null),		// Initialize this property if you already have an HiveAuth token
						//new JProperty("challenge", null)	// Initialize this proporty if you have a challenge
						).ToString();

					// Encrypt auth_req_data using our authentication key
					auth_req_data = CryptoJS.Encrypt(auth_req_data, m_auth_key);

					// Prepare HAS payload
					string payload =
						new JObject(
							new JProperty("cmd", "auth_req"),
							new JProperty("account", txtUsername.Text),
							new JProperty("data", auth_req_data)
						).ToString();
					// Send the auth_req to the HAS server
					await Send(ws, payload);
					// Wait for request processing
					await Receive(ws);
					Console.WriteLine("receive completed");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"ERROR - {ex.Message}");
					//break;
				}
			}
		}

		private void btnBroadcast_Click(object sender, EventArgs e)
		{

		}
	}
}
