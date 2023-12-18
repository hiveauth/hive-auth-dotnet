namespace hive_auth_dotnet
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnLogin = new System.Windows.Forms.Button();
			this.picQRCode = new System.Windows.Forms.PictureBox();
			this.btnSign = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picQRCode)).BeginInit();
			this.SuspendLayout();
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(12, 30);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(123, 20);
			this.txtUsername.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Username:";
			// 
			// btnLogin
			// 
			this.btnLogin.Location = new System.Drawing.Point(138, 28);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(123, 23);
			this.btnLogin.TabIndex = 2;
			this.btnLogin.Text = "Login";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// picQRCode
			// 
			this.picQRCode.Location = new System.Drawing.Point(12, 85);
			this.picQRCode.Name = "picQRCode";
			this.picQRCode.Size = new System.Drawing.Size(250, 250);
			this.picQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picQRCode.TabIndex = 3;
			this.picQRCode.TabStop = false;
			this.picQRCode.Visible = false;
			// 
			// btnSign
			// 
			this.btnSign.Enabled = false;
			this.btnSign.Location = new System.Drawing.Point(138, 56);
			this.btnSign.Name = "btnSign";
			this.btnSign.Size = new System.Drawing.Size(122, 23);
			this.btnSign.TabIndex = 2;
			this.btnSign.Text = "Sign Transaction";
			this.btnSign.UseVisualStyleBackColor = true;
			this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(272, 340);
			this.Controls.Add(this.picQRCode);
			this.Controls.Add(this.btnSign);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtUsername);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "HiveAuth Demo";
			((System.ComponentModel.ISupportInitialize)(this.picQRCode)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.PictureBox picQRCode;
		private System.Windows.Forms.Button btnSign;
	}
}

