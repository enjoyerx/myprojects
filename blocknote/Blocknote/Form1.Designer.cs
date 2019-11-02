namespace Blocknote
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
            this.getSessionKeyButton = new System.Windows.Forms.Button();
            this.generateRSAButton = new System.Windows.Forms.Button();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.loginRejected = new System.Windows.Forms.Label();
            this.filenameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.getTextButton = new System.Windows.Forms.Button();
            this.blocknote = new System.Windows.Forms.GroupBox();
            this.sendTextButton = new System.Windows.Forms.Button();
            this.serverResponses = new System.Windows.Forms.Label();
            this.blocknote.SuspendLayout();
            this.SuspendLayout();
            // 
            // getSessionKeyButton
            // 
            this.getSessionKeyButton.Location = new System.Drawing.Point(42, 26);
            this.getSessionKeyButton.Name = "getSessionKeyButton";
            this.getSessionKeyButton.Size = new System.Drawing.Size(177, 48);
            this.getSessionKeyButton.TabIndex = 4;
            this.getSessionKeyButton.Text = "Get AES";
            this.getSessionKeyButton.UseVisualStyleBackColor = true;
            this.getSessionKeyButton.Click += new System.EventHandler(this.getSessionKeyButton_Click);
            // 
            // generateRSAButton
            // 
            this.generateRSAButton.Location = new System.Drawing.Point(42, 107);
            this.generateRSAButton.Name = "generateRSAButton";
            this.generateRSAButton.Size = new System.Drawing.Size(177, 49);
            this.generateRSAButton.TabIndex = 5;
            this.generateRSAButton.Text = "Generate RSA";
            this.generateRSAButton.UseVisualStyleBackColor = true;
            this.generateRSAButton.Click += new System.EventHandler(this.generateRSAButton_Click);
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(302, 40);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(48, 20);
            this.labelLogin.TabIndex = 7;
            this.labelLogin.Text = "Login";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(302, 90);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(78, 20);
            this.labelPassword.TabIndex = 8;
            this.labelPassword.Text = "Password";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(433, 40);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(280, 26);
            this.textBoxLogin.TabIndex = 9;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(433, 90);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(280, 26);
            this.textBoxPassword.TabIndex = 10;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(433, 149);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(280, 32);
            this.loginButton.TabIndex = 11;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // loginRejected
            // 
            this.loginRejected.AutoSize = true;
            this.loginRejected.Location = new System.Drawing.Point(512, 121);
            this.loginRejected.Name = "loginRejected";
            this.loginRejected.Size = new System.Drawing.Size(109, 20);
            this.loginRejected.TabIndex = 12;
            this.loginRejected.Text = "Login rejected";
            // 
            // filenameBox
            // 
            this.filenameBox.Location = new System.Drawing.Point(29, 45);
            this.filenameBox.Name = "filenameBox";
            this.filenameBox.Size = new System.Drawing.Size(308, 26);
            this.filenameBox.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Enter file name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(374, 45);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(366, 129);
            this.textBox1.TabIndex = 15;
            // 
            // getTextButton
            // 
            this.getTextButton.Location = new System.Drawing.Point(29, 86);
            this.getTextButton.Name = "getTextButton";
            this.getTextButton.Size = new System.Drawing.Size(308, 34);
            this.getTextButton.TabIndex = 16;
            this.getTextButton.Text = "Get text";
            this.getTextButton.UseVisualStyleBackColor = true;
            this.getTextButton.Click += new System.EventHandler(this.getTextButton_Click);
            // 
            // blocknote
            // 
            this.blocknote.Controls.Add(this.sendTextButton);
            this.blocknote.Controls.Add(this.label1);
            this.blocknote.Controls.Add(this.textBox1);
            this.blocknote.Controls.Add(this.getTextButton);
            this.blocknote.Controls.Add(this.filenameBox);
            this.blocknote.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.blocknote.Location = new System.Drawing.Point(12, 221);
            this.blocknote.Name = "blocknote";
            this.blocknote.Size = new System.Drawing.Size(746, 189);
            this.blocknote.TabIndex = 17;
            this.blocknote.TabStop = false;
            this.blocknote.Visible = false;
            // 
            // sendTextButton
            // 
            this.sendTextButton.Location = new System.Drawing.Point(30, 140);
            this.sendTextButton.Name = "sendTextButton";
            this.sendTextButton.Size = new System.Drawing.Size(308, 34);
            this.sendTextButton.TabIndex = 17;
            this.sendTextButton.Text = "Send text";
            this.sendTextButton.UseVisualStyleBackColor = true;
            this.sendTextButton.Click += new System.EventHandler(this.sendTextButton_Click);
            // 
            // serverResponses
            // 
            this.serverResponses.AutoSize = true;
            this.serverResponses.Location = new System.Drawing.Point(38, 421);
            this.serverResponses.Name = "serverResponses";
            this.serverResponses.Size = new System.Drawing.Size(130, 20);
            this.serverResponses.TabIndex = 18;
            this.serverResponses.Text = "server responses";
            this.serverResponses.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.serverResponses);
            this.Controls.Add(this.blocknote);
            this.Controls.Add(this.loginRejected);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.generateRSAButton);
            this.Controls.Add(this.getSessionKeyButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.blocknote.ResumeLayout(false);
            this.blocknote.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button getSessionKeyButton;
        private System.Windows.Forms.Button generateRSAButton;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label loginRejected;
        private System.Windows.Forms.TextBox filenameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button getTextButton;
        private System.Windows.Forms.GroupBox blocknote;
        private System.Windows.Forms.Button sendTextButton;
        private System.Windows.Forms.Label serverResponses;
    }
}

