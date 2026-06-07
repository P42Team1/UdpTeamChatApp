namespace UdpTeamChatApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelLogin = new Panel();
            labelRegistrateLink = new Label();
            label5 = new Label();
            label4 = new Label();
            textBoxPassword_Log = new TextBox();
            textBoxLogin_Log = new TextBox();
            buttonLogin = new Button();
            label2 = new Label();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox1 = new TextBox();
            label3 = new Label();
            button1 = new Button();
            label1 = new Label();
            panelServer = new Panel();
            panelRegistrate = new Panel();
            label9 = new Label();
            textBoxEmail_Reg = new TextBox();
            label7 = new Label();
            label8 = new Label();
            textBoxPassword_Reg = new TextBox();
            textBoxLogin_Reg = new TextBox();
            buttonRegistrate = new Button();
            panelLogin.SuspendLayout();
            panelServer.SuspendLayout();
            panelRegistrate.SuspendLayout();
            SuspendLayout();
            // 
            // panelLogin
            // 
            panelLogin.Controls.Add(labelRegistrateLink);
            panelLogin.Controls.Add(label5);
            panelLogin.Controls.Add(label4);
            panelLogin.Controls.Add(textBoxPassword_Log);
            panelLogin.Controls.Add(textBoxLogin_Log);
            panelLogin.Controls.Add(buttonLogin);
            panelLogin.Dock = DockStyle.Fill;
            panelLogin.Location = new Point(0, 0);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(1035, 576);
            panelLogin.TabIndex = 7;
            // 
            // labelRegistrateLink
            // 
            labelRegistrateLink.AutoSize = true;
            labelRegistrateLink.Location = new Point(477, 300);
            labelRegistrateLink.Name = "labelRegistrateLink";
            labelRegistrateLink.Size = new Size(71, 15);
            labelRegistrateLink.TabIndex = 5;
            labelRegistrateLink.Text = "REGISTRATE";
            labelRegistrateLink.Click += label6_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(435, 243);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 4;
            label5.Text = "Password:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(435, 195);
            label4.Name = "label4";
            label4.Size = new Size(40, 15);
            label4.TabIndex = 3;
            label4.Text = "Login:";
            // 
            // textBoxPassword_Log
            // 
            textBoxPassword_Log.Location = new Point(435, 261);
            textBoxPassword_Log.Name = "textBoxPassword_Log";
            textBoxPassword_Log.Size = new Size(169, 23);
            textBoxPassword_Log.TabIndex = 2;
            // 
            // textBoxLogin_Log
            // 
            textBoxLogin_Log.Location = new Point(435, 213);
            textBoxLogin_Log.Name = "textBoxLogin_Log";
            textBoxLogin_Log.Size = new Size(169, 23);
            textBoxLogin_Log.TabIndex = 1;
            // 
            // buttonLogin
            // 
            buttonLogin.Location = new Point(455, 329);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(121, 27);
            buttonLogin.TabIndex = 0;
            buttonLogin.Text = "Log in";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(213, 26);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 4;
            label2.Text = "Port";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(213, 44);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(110, 23);
            textBox2.TabIndex = 2;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(22, 93);
            textBox3.Margin = new Padding(3, 2, 3, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(302, 23);
            textBox3.TabIndex = 5;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(22, 44);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(172, 23);
            textBox1.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 76);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 6;
            label3.Text = "Message";
            // 
            // button1
            // 
            button1.Location = new Point(487, 42);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(82, 22);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 26);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 3;
            label1.Text = "IPAddress";
            // 
            // panelServer
            // 
            panelServer.Controls.Add(label1);
            panelServer.Controls.Add(button1);
            panelServer.Controls.Add(label3);
            panelServer.Controls.Add(textBox1);
            panelServer.Controls.Add(textBox3);
            panelServer.Controls.Add(textBox2);
            panelServer.Controls.Add(label2);
            panelServer.Dock = DockStyle.Fill;
            panelServer.Location = new Point(0, 0);
            panelServer.Name = "panelServer";
            panelServer.Size = new Size(1035, 576);
            panelServer.TabIndex = 8;
            // 
            // panelRegistrate
            // 
            panelRegistrate.Controls.Add(label9);
            panelRegistrate.Controls.Add(textBoxEmail_Reg);
            panelRegistrate.Controls.Add(label7);
            panelRegistrate.Controls.Add(label8);
            panelRegistrate.Controls.Add(textBoxPassword_Reg);
            panelRegistrate.Controls.Add(textBoxLogin_Reg);
            panelRegistrate.Controls.Add(buttonRegistrate);
            panelRegistrate.Dock = DockStyle.Fill;
            panelRegistrate.Location = new Point(0, 0);
            panelRegistrate.Name = "panelRegistrate";
            panelRegistrate.Size = new Size(1035, 576);
            panelRegistrate.TabIndex = 6;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(433, 300);
            label9.Name = "label9";
            label9.Size = new Size(39, 15);
            label9.TabIndex = 11;
            label9.Text = "Email:";
            // 
            // textBoxEmail_Reg
            // 
            textBoxEmail_Reg.Location = new Point(433, 318);
            textBoxEmail_Reg.Name = "textBoxEmail_Reg";
            textBoxEmail_Reg.Size = new Size(169, 23);
            textBoxEmail_Reg.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(433, 256);
            label7.Name = "label7";
            label7.Size = new Size(60, 15);
            label7.TabIndex = 9;
            label7.Text = "Password:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(433, 208);
            label8.Name = "label8";
            label8.Size = new Size(40, 15);
            label8.TabIndex = 8;
            label8.Text = "Login:";
            // 
            // textBoxPassword_Reg
            // 
            textBoxPassword_Reg.Location = new Point(433, 274);
            textBoxPassword_Reg.Name = "textBoxPassword_Reg";
            textBoxPassword_Reg.Size = new Size(169, 23);
            textBoxPassword_Reg.TabIndex = 7;
            // 
            // textBoxLogin_Reg
            // 
            textBoxLogin_Reg.Location = new Point(433, 226);
            textBoxLogin_Reg.Name = "textBoxLogin_Reg";
            textBoxLogin_Reg.Size = new Size(169, 23);
            textBoxLogin_Reg.TabIndex = 6;
            // 
            // buttonRegistrate
            // 
            buttonRegistrate.Location = new Point(455, 362);
            buttonRegistrate.Name = "buttonRegistrate";
            buttonRegistrate.Size = new Size(121, 27);
            buttonRegistrate.TabIndex = 5;
            buttonRegistrate.Text = "Registrate";
            buttonRegistrate.UseVisualStyleBackColor = true;
            buttonRegistrate.Click += buttonRegistrate_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1035, 576);
            Controls.Add(panelLogin);
            Controls.Add(panelRegistrate);
            Controls.Add(panelServer);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            panelServer.ResumeLayout(false);
            panelServer.PerformLayout();
            panelRegistrate.ResumeLayout(false);
            panelRegistrate.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panelLogin;
        private Button buttonLogin;
        private Label label5;
        private Label label4;
        private TextBox textBoxPassword_Log;
        private TextBox textBoxLogin_Log;
        private Label labelRegistrateLink;
        private Label label2;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox1;
        private Label label3;
        private Button button1;
        private Label label1;
        private Panel panelServer;
        private Panel panelRegistrate;
        private Label label7;
        private Label label8;
        private TextBox textBoxPassword_Reg;
        private TextBox textBoxLogin_Reg;
        private Button buttonRegistrate;
        private Label label9;
        private TextBox textBoxEmail_Reg;
    }
}
