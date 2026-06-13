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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            textBox1 = new TextBox();
            textBox3 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox4 = new TextBox();
            label3 = new Label();
            textBox5 = new TextBox();
            textBox2 = new TextBox();
            label4 = new Label();
            button2 = new Button();
            button3 = new Button();
            groupBox1 = new GroupBox();
            label7 = new Label();
            comboBox1 = new ComboBox();
            groupBox2 = new GroupBox();
            textBox8 = new TextBox();
            textBox7 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            button4 = new Button();
            textBox6 = new TextBox();
            panelChat = new Panel();
            panelPayAttention = new Panel();
            labelPayAttentionHelp = new Label();
            labelPayAttention = new Label();
            panelLogin = new Panel();
            buttonGotoRegistrate = new Button();
            buttonLogIn = new Button();
            labelPassword_Log = new Label();
            textBoxPassword_Log = new TextBox();
            labelUsername_Log = new Label();
            textBoxUsername_Log = new TextBox();
            panelRegistrate = new Panel();
            buttonRegistrate = new Button();
            labelPassword_Reg = new Label();
            textBoxPassword_Reg = new TextBox();
            labelEmail_Reg = new Label();
            textBoxEmail_Reg = new TextBox();
            labelUsername_Reg = new Label();
            textBoxUsername_Reg = new TextBox();
            buttonReturnToLogIn = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            panelChat.SuspendLayout();
            panelPayAttention.SuspendLayout();
            panelLogin.SuspendLayout();
            panelRegistrate.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(457, 110);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "Send";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(43, 62);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(172, 23);
            textBox1.TabIndex = 1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(521, 54);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 23);
            textBox3.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 39);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 3;
            label1.Text = "ServerIPAddress";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(521, 31);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 4;
            label2.Text = "Port";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(43, 112);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(344, 23);
            textBox4.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(43, 89);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 6;
            label3.Text = "Message";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(6, 169);
            textBox5.Multiline = true;
            textBox5.Name = "textBox5";
            textBox5.ScrollBars = ScrollBars.Vertical;
            textBox5.Size = new Size(665, 309);
            textBox5.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(262, 62);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 23);
            textBox2.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(262, 39);
            label4.Name = "label4";
            label4.Size = new Size(61, 15);
            label4.TabIndex = 9;
            label4.Text = "ServerPort";
            // 
            // button2
            // 
            button2.Location = new Point(674, 53);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 10;
            button2.Text = "Connect";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(800, 53);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 11;
            button3.Text = "Disconnect";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(textBox5);
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(22, 99);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(677, 484);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Send to chat";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(412, 39);
            label7.Name = "label7";
            label7.Size = new Size(32, 15);
            label7.TabIndex = 11;
            label7.Text = "Chat";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1", "2" });
            comboBox1.Location = new Point(412, 62);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 23);
            comboBox1.TabIndex = 10;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBox8);
            groupBox2.Controls.Add(textBox7);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(button4);
            groupBox2.Controls.Add(textBox6);
            groupBox2.Location = new Point(729, 99);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(673, 484);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Send privately";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(2, 169);
            textBox8.Multiline = true;
            textBox8.Name = "textBox8";
            textBox8.ScrollBars = ScrollBars.Vertical;
            textBox8.Size = new Size(665, 309);
            textBox8.TabIndex = 10;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(186, 62);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(445, 23);
            textBox7.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(39, 39);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 14;
            label5.Text = "User Port";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(186, 39);
            label6.Name = "label6";
            label6.Size = new Size(53, 15);
            label6.TabIndex = 12;
            label6.Text = "Message";
            // 
            // button4
            // 
            button4.Location = new Point(284, 95);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 10;
            button4.Text = "Send";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(39, 62);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(125, 23);
            textBox6.TabIndex = 13;
            // 
            // panelChat
            // 
            panelChat.Controls.Add(label2);
            panelChat.Controls.Add(groupBox2);
            panelChat.Controls.Add(textBox3);
            panelChat.Controls.Add(groupBox1);
            panelChat.Controls.Add(button2);
            panelChat.Controls.Add(button3);
            panelChat.Dock = DockStyle.Fill;
            panelChat.Location = new Point(0, 0);
            panelChat.Name = "panelChat";
            panelChat.Size = new Size(1436, 723);
            panelChat.TabIndex = 14;
            // 
            // panelPayAttention
            // 
            panelPayAttention.Controls.Add(labelPayAttentionHelp);
            panelPayAttention.Controls.Add(labelPayAttention);
            panelPayAttention.Dock = DockStyle.Fill;
            panelPayAttention.Location = new Point(0, 0);
            panelPayAttention.Name = "panelPayAttention";
            panelPayAttention.Size = new Size(1436, 723);
            panelPayAttention.TabIndex = 14;
            // 
            // labelPayAttentionHelp
            // 
            labelPayAttentionHelp.AutoSize = true;
            labelPayAttentionHelp.Font = new Font("Segoe UI", 12F);
            labelPayAttentionHelp.Location = new Point(12, 153);
            labelPayAttentionHelp.Name = "labelPayAttentionHelp";
            labelPayAttentionHelp.Size = new Size(1224, 21);
            labelPayAttentionHelp.TabIndex = 1;
            labelPayAttentionHelp.Text = "Щоб переміщатись по панелям зайдіть в View > Other Windows > Document Outline(Ctrl + Alt + T). Щоб переміщатись натисність на стрілочку яка вказує вниз або вверх";
            // 
            // labelPayAttention
            // 
            labelPayAttention.AutoSize = true;
            labelPayAttention.Font = new Font("Segoe UI", 8F);
            labelPayAttention.Location = new Point(3, 46);
            labelPayAttention.Name = "labelPayAttention";
            labelPayAttention.Size = new Size(1405, 13);
            labelPayAttention.TabIndex = 0;
            labelPayAttention.Text = resources.GetString("labelPayAttention.Text");
            // 
            // panelLogin
            // 
            panelLogin.Controls.Add(buttonGotoRegistrate);
            panelLogin.Controls.Add(buttonLogIn);
            panelLogin.Controls.Add(labelPassword_Log);
            panelLogin.Controls.Add(textBoxPassword_Log);
            panelLogin.Controls.Add(labelUsername_Log);
            panelLogin.Controls.Add(textBoxUsername_Log);
            panelLogin.Dock = DockStyle.Fill;
            panelLogin.Location = new Point(0, 0);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(1436, 723);
            panelLogin.TabIndex = 14;
            // 
            // buttonGotoRegistrate
            // 
            buttonGotoRegistrate.Location = new Point(551, 316);
            buttonGotoRegistrate.Name = "buttonGotoRegistrate";
            buttonGotoRegistrate.Size = new Size(106, 23);
            buttonGotoRegistrate.TabIndex = 13;
            buttonGotoRegistrate.Text = "Go To Registrate";
            buttonGotoRegistrate.UseVisualStyleBackColor = true;
            buttonGotoRegistrate.Click += buttonGotoRegistrate_Click;
            // 
            // buttonLogIn
            // 
            buttonLogIn.Location = new Point(521, 363);
            buttonLogIn.Name = "buttonLogIn";
            buttonLogIn.Size = new Size(172, 23);
            buttonLogIn.TabIndex = 12;
            buttonLogIn.Text = "Log In";
            buttonLogIn.UseVisualStyleBackColor = true;
            buttonLogIn.Click += buttonLogIn_Click;
            // 
            // labelPassword_Log
            // 
            labelPassword_Log.AutoSize = true;
            labelPassword_Log.Location = new Point(513, 269);
            labelPassword_Log.Name = "labelPassword_Log";
            labelPassword_Log.Size = new Size(57, 15);
            labelPassword_Log.TabIndex = 11;
            labelPassword_Log.Text = "Password";
            // 
            // textBoxPassword_Log
            // 
            textBoxPassword_Log.Location = new Point(513, 287);
            textBoxPassword_Log.Name = "textBoxPassword_Log";
            textBoxPassword_Log.Size = new Size(189, 23);
            textBoxPassword_Log.TabIndex = 10;
            // 
            // labelUsername_Log
            // 
            labelUsername_Log.AutoSize = true;
            labelUsername_Log.Location = new Point(513, 214);
            labelUsername_Log.Name = "labelUsername_Log";
            labelUsername_Log.Size = new Size(60, 15);
            labelUsername_Log.TabIndex = 7;
            labelUsername_Log.Text = "Username";
            // 
            // textBoxUsername_Log
            // 
            textBoxUsername_Log.Location = new Point(513, 232);
            textBoxUsername_Log.Name = "textBoxUsername_Log";
            textBoxUsername_Log.Size = new Size(189, 23);
            textBoxUsername_Log.TabIndex = 6;
            // 
            // panelRegistrate
            // 
            panelRegistrate.Controls.Add(buttonReturnToLogIn);
            panelRegistrate.Controls.Add(buttonRegistrate);
            panelRegistrate.Controls.Add(labelPassword_Reg);
            panelRegistrate.Controls.Add(textBoxPassword_Reg);
            panelRegistrate.Controls.Add(labelEmail_Reg);
            panelRegistrate.Controls.Add(textBoxEmail_Reg);
            panelRegistrate.Controls.Add(labelUsername_Reg);
            panelRegistrate.Controls.Add(textBoxUsername_Reg);
            panelRegistrate.Dock = DockStyle.Fill;
            panelRegistrate.Location = new Point(0, 0);
            panelRegistrate.Name = "panelRegistrate";
            panelRegistrate.Size = new Size(1436, 723);
            panelRegistrate.TabIndex = 0;
            // 
            // buttonRegistrate
            // 
            buttonRegistrate.Location = new Point(590, 375);
            buttonRegistrate.Name = "buttonRegistrate";
            buttonRegistrate.Size = new Size(166, 23);
            buttonRegistrate.TabIndex = 6;
            buttonRegistrate.Text = "Registrate";
            buttonRegistrate.UseVisualStyleBackColor = true;
            buttonRegistrate.Click += buttonRegistrate_Click;
            // 
            // labelPassword_Reg
            // 
            labelPassword_Reg.AutoSize = true;
            labelPassword_Reg.Location = new Point(579, 298);
            labelPassword_Reg.Name = "labelPassword_Reg";
            labelPassword_Reg.Size = new Size(57, 15);
            labelPassword_Reg.TabIndex = 5;
            labelPassword_Reg.Text = "Password";
            // 
            // textBoxPassword_Reg
            // 
            textBoxPassword_Reg.Location = new Point(579, 316);
            textBoxPassword_Reg.Name = "textBoxPassword_Reg";
            textBoxPassword_Reg.Size = new Size(189, 23);
            textBoxPassword_Reg.TabIndex = 4;
            // 
            // labelEmail_Reg
            // 
            labelEmail_Reg.AutoSize = true;
            labelEmail_Reg.Location = new Point(579, 250);
            labelEmail_Reg.Name = "labelEmail_Reg";
            labelEmail_Reg.Size = new Size(36, 15);
            labelEmail_Reg.TabIndex = 3;
            labelEmail_Reg.Text = "Email";
            // 
            // textBoxEmail_Reg
            // 
            textBoxEmail_Reg.Location = new Point(579, 268);
            textBoxEmail_Reg.Name = "textBoxEmail_Reg";
            textBoxEmail_Reg.Size = new Size(189, 23);
            textBoxEmail_Reg.TabIndex = 2;
            // 
            // labelUsername_Reg
            // 
            labelUsername_Reg.AutoSize = true;
            labelUsername_Reg.Location = new Point(579, 206);
            labelUsername_Reg.Name = "labelUsername_Reg";
            labelUsername_Reg.Size = new Size(60, 15);
            labelUsername_Reg.TabIndex = 1;
            labelUsername_Reg.Text = "Username";
            // 
            // textBoxUsername_Reg
            // 
            textBoxUsername_Reg.Location = new Point(579, 224);
            textBoxUsername_Reg.Name = "textBoxUsername_Reg";
            textBoxUsername_Reg.Size = new Size(189, 23);
            textBoxUsername_Reg.TabIndex = 0;
            // 
            // buttonReturnToLogIn
            // 
            buttonReturnToLogIn.Location = new Point(617, 345);
            buttonReturnToLogIn.Name = "buttonReturnToLogIn";
            buttonReturnToLogIn.Size = new Size(108, 23);
            buttonReturnToLogIn.TabIndex = 7;
            buttonReturnToLogIn.Text = "Return to Log In";
            buttonReturnToLogIn.UseVisualStyleBackColor = true;
            buttonReturnToLogIn.Click += buttonReturnToLogIn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1436, 723);
            Controls.Add(panelRegistrate);
            Controls.Add(panelLogin);
            Controls.Add(panelChat);
            Controls.Add(panelPayAttention);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            panelChat.ResumeLayout(false);
            panelChat.PerformLayout();
            panelPayAttention.ResumeLayout(false);
            panelPayAttention.PerformLayout();
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            panelRegistrate.ResumeLayout(false);
            panelRegistrate.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private TextBox textBox3;
        private Label label1;
        private Label label2;
        private TextBox textBox4;
        private Label label3;
        private TextBox textBox5;
        private TextBox textBox2;
        private Label label4;
        private Button button2;
        private Button button3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox textBox8;
        private TextBox textBox7;
        private Label label5;
        private Label label6;
        private Button button4;
        private TextBox textBox6;
        private Label label7;
        private ComboBox comboBox1;
        private Panel panelChat;
        private Panel panelPayAttention;
        private Label labelPayAttentionHelp;
        private Label labelPayAttention;
        private Panel panelLogin;
        private Panel panelRegistrate;
        private TextBox textBoxUsername_Reg;
        private Button buttonGotoRegistrate;
        private Button buttonLogIn;
        private Label labelPassword_Log;
        private TextBox textBoxPassword_Log;
        private Label labelUsername_Log;
        private TextBox textBoxUsername_Log;
        private Label labelPassword_Reg;
        private TextBox textBoxPassword_Reg;
        private Label labelEmail_Reg;
        private TextBox textBoxEmail_Reg;
        private Label labelUsername_Reg;
        private Button buttonRegistrate;
        private Button buttonReturnToLogIn;
    }
}
