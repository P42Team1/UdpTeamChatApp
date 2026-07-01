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
            buttonCreateChat = new Button();
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
            buttonReturnToLogIn = new Button();
            buttonRegistrate = new Button();
            labelPassword_Reg = new Label();
            textBoxPassword_Reg = new TextBox();
            labelEmail_Reg = new Label();
            textBoxEmail_Reg = new TextBox();
            labelUsername_Reg = new Label();
            textBoxUsername_Reg = new TextBox();
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
            button1.Location = new Point(522, 147);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(107, 39);
            button1.TabIndex = 0;
            button1.Text = "Send";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(49, 83);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(196, 27);
            textBox1.TabIndex = 1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(595, 72);
            textBox3.Margin = new Padding(3, 4, 3, 4);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(142, 27);
            textBox3.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 52);
            label1.Name = "label1";
            label1.Size = new Size(114, 20);
            label1.TabIndex = 3;
            label1.Text = "ServerIPAddress";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(595, 41);
            label2.Name = "label2";
            label2.Size = new Size(35, 20);
            label2.TabIndex = 4;
            label2.Text = "Port";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(49, 149);
            textBox4.Margin = new Padding(3, 4, 3, 4);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(393, 27);
            textBox4.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(49, 119);
            label3.Name = "label3";
            label3.Size = new Size(67, 20);
            label3.TabIndex = 6;
            label3.Text = "Message";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(7, 225);
            textBox5.Margin = new Padding(3, 4, 3, 4);
            textBox5.Multiline = true;
            textBox5.Name = "textBox5";
            textBox5.ScrollBars = ScrollBars.Vertical;
            textBox5.Size = new Size(759, 411);
            textBox5.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(261, 84);
            textBox2.Margin = new Padding(3, 4, 3, 4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(142, 27);
            textBox2.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(261, 53);
            label4.Name = "label4";
            label4.Size = new Size(76, 20);
            label4.TabIndex = 9;
            label4.Text = "ServerPort";
            // 
            // button2
            // 
            button2.Location = new Point(770, 71);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(107, 39);
            button2.TabIndex = 10;
            button2.Text = "Connect";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(914, 71);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(107, 39);
            button3.TabIndex = 11;
            button3.Text = "Disconnect";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonCreateChat);
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
            groupBox1.Location = new Point(25, 132);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(774, 645);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Send to chat";
            // 
            // buttonCreateChat
            // 
            buttonCreateChat.Location = new Point(649, 83);
            buttonCreateChat.Name = "buttonCreateChat";
            buttonCreateChat.Size = new Size(94, 29);
            buttonCreateChat.TabIndex = 12;
            buttonCreateChat.Text = "New chat";
            buttonCreateChat.UseVisualStyleBackColor = true;
            buttonCreateChat.Click += buttonCreateChat_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(448, 60);
            label7.Name = "label7";
            label7.Size = new Size(39, 20);
            label7.TabIndex = 11;
            label7.Text = "Chat";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1", "2" });
            comboBox1.Location = new Point(448, 84);
            comboBox1.Margin = new Padding(3, 4, 3, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(172, 28);
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
            groupBox2.Location = new Point(833, 132);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(769, 645);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Send privately";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(2, 225);
            textBox8.Margin = new Padding(3, 4, 3, 4);
            textBox8.Multiline = true;
            textBox8.Name = "textBox8";
            textBox8.ScrollBars = ScrollBars.Vertical;
            textBox8.Size = new Size(759, 411);
            textBox8.TabIndex = 10;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(213, 83);
            textBox7.Margin = new Padding(3, 4, 3, 4);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(508, 27);
            textBox7.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(45, 52);
            label5.Name = "label5";
            label5.Size = new Size(68, 20);
            label5.TabIndex = 14;
            label5.Text = "User Port";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(213, 52);
            label6.Name = "label6";
            label6.Size = new Size(67, 20);
            label6.TabIndex = 12;
            label6.Text = "Message";
            // 
            // button4
            // 
            button4.Location = new Point(325, 127);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(107, 39);
            button4.TabIndex = 10;
            button4.Text = "Send";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(45, 83);
            textBox6.Margin = new Padding(3, 4, 3, 4);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(142, 27);
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
            panelChat.Margin = new Padding(3, 4, 3, 4);
            panelChat.Name = "panelChat";
            panelChat.Size = new Size(1641, 964);
            panelChat.TabIndex = 14;
            // 
            // panelPayAttention
            // 
            panelPayAttention.Controls.Add(labelPayAttentionHelp);
            panelPayAttention.Controls.Add(labelPayAttention);
            panelPayAttention.Dock = DockStyle.Fill;
            panelPayAttention.Location = new Point(0, 0);
            panelPayAttention.Margin = new Padding(3, 4, 3, 4);
            panelPayAttention.Name = "panelPayAttention";
            panelPayAttention.Size = new Size(1641, 964);
            panelPayAttention.TabIndex = 14;
            // 
            // labelPayAttentionHelp
            // 
            labelPayAttentionHelp.AutoSize = true;
            labelPayAttentionHelp.Font = new Font("Segoe UI", 12F);
            labelPayAttentionHelp.Location = new Point(14, 204);
            labelPayAttentionHelp.Name = "labelPayAttentionHelp";
            labelPayAttentionHelp.Size = new Size(1555, 28);
            labelPayAttentionHelp.TabIndex = 1;
            labelPayAttentionHelp.Text = "Щоб переміщатись по панелям зайдіть в View > Other Windows > Document Outline(Ctrl + Alt + T). Щоб переміщатись натисність на стрілочку яка вказує вниз або вверх";
            // 
            // labelPayAttention
            // 
            labelPayAttention.AutoSize = true;
            labelPayAttention.Font = new Font("Segoe UI", 8F);
            labelPayAttention.Location = new Point(3, 61);
            labelPayAttention.Name = "labelPayAttention";
            labelPayAttention.Size = new Size(1676, 19);
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
            panelLogin.Margin = new Padding(3, 4, 3, 4);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(1641, 964);
            panelLogin.TabIndex = 14;
            // 
            // buttonGotoRegistrate
            // 
            buttonGotoRegistrate.Location = new Point(630, 421);
            buttonGotoRegistrate.Margin = new Padding(3, 4, 3, 4);
            buttonGotoRegistrate.Name = "buttonGotoRegistrate";
            buttonGotoRegistrate.Size = new Size(121, 31);
            buttonGotoRegistrate.TabIndex = 13;
            buttonGotoRegistrate.Text = "Go To Registrate";
            buttonGotoRegistrate.UseVisualStyleBackColor = true;
            buttonGotoRegistrate.Click += buttonGotoRegistrate_Click;
            // 
            // buttonLogIn
            // 
            buttonLogIn.Location = new Point(595, 484);
            buttonLogIn.Margin = new Padding(3, 4, 3, 4);
            buttonLogIn.Name = "buttonLogIn";
            buttonLogIn.Size = new Size(197, 31);
            buttonLogIn.TabIndex = 12;
            buttonLogIn.Text = "Log In";
            buttonLogIn.UseVisualStyleBackColor = true;
            buttonLogIn.Click += buttonLogIn_Click;
            // 
            // labelPassword_Log
            // 
            labelPassword_Log.AutoSize = true;
            labelPassword_Log.Location = new Point(586, 359);
            labelPassword_Log.Name = "labelPassword_Log";
            labelPassword_Log.Size = new Size(70, 20);
            labelPassword_Log.TabIndex = 11;
            labelPassword_Log.Text = "Password";
            // 
            // textBoxPassword_Log
            // 
            textBoxPassword_Log.Location = new Point(586, 383);
            textBoxPassword_Log.Margin = new Padding(3, 4, 3, 4);
            textBoxPassword_Log.Name = "textBoxPassword_Log";
            textBoxPassword_Log.Size = new Size(215, 27);
            textBoxPassword_Log.TabIndex = 10;
            // 
            // labelUsername_Log
            // 
            labelUsername_Log.AutoSize = true;
            labelUsername_Log.Location = new Point(586, 285);
            labelUsername_Log.Name = "labelUsername_Log";
            labelUsername_Log.Size = new Size(75, 20);
            labelUsername_Log.TabIndex = 7;
            labelUsername_Log.Text = "Username";
            // 
            // textBoxUsername_Log
            // 
            textBoxUsername_Log.Location = new Point(586, 309);
            textBoxUsername_Log.Margin = new Padding(3, 4, 3, 4);
            textBoxUsername_Log.Name = "textBoxUsername_Log";
            textBoxUsername_Log.Size = new Size(215, 27);
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
            panelRegistrate.Margin = new Padding(3, 4, 3, 4);
            panelRegistrate.Name = "panelRegistrate";
            panelRegistrate.Size = new Size(1641, 964);
            panelRegistrate.TabIndex = 0;
            // 
            // buttonReturnToLogIn
            // 
            buttonReturnToLogIn.Location = new Point(705, 460);
            buttonReturnToLogIn.Margin = new Padding(3, 4, 3, 4);
            buttonReturnToLogIn.Name = "buttonReturnToLogIn";
            buttonReturnToLogIn.Size = new Size(123, 31);
            buttonReturnToLogIn.TabIndex = 7;
            buttonReturnToLogIn.Text = "Return to Log In";
            buttonReturnToLogIn.UseVisualStyleBackColor = true;
            buttonReturnToLogIn.Click += buttonReturnToLogIn_Click;
            // 
            // buttonRegistrate
            // 
            buttonRegistrate.Location = new Point(674, 500);
            buttonRegistrate.Margin = new Padding(3, 4, 3, 4);
            buttonRegistrate.Name = "buttonRegistrate";
            buttonRegistrate.Size = new Size(190, 31);
            buttonRegistrate.TabIndex = 6;
            buttonRegistrate.Text = "Registrate";
            buttonRegistrate.UseVisualStyleBackColor = true;
            buttonRegistrate.Click += buttonRegistrate_Click;
            // 
            // labelPassword_Reg
            // 
            labelPassword_Reg.AutoSize = true;
            labelPassword_Reg.Location = new Point(662, 397);
            labelPassword_Reg.Name = "labelPassword_Reg";
            labelPassword_Reg.Size = new Size(70, 20);
            labelPassword_Reg.TabIndex = 5;
            labelPassword_Reg.Text = "Password";
            // 
            // textBoxPassword_Reg
            // 
            textBoxPassword_Reg.Location = new Point(662, 421);
            textBoxPassword_Reg.Margin = new Padding(3, 4, 3, 4);
            textBoxPassword_Reg.Name = "textBoxPassword_Reg";
            textBoxPassword_Reg.Size = new Size(215, 27);
            textBoxPassword_Reg.TabIndex = 4;
            // 
            // labelEmail_Reg
            // 
            labelEmail_Reg.AutoSize = true;
            labelEmail_Reg.Location = new Point(662, 333);
            labelEmail_Reg.Name = "labelEmail_Reg";
            labelEmail_Reg.Size = new Size(46, 20);
            labelEmail_Reg.TabIndex = 3;
            labelEmail_Reg.Text = "Email";
            // 
            // textBoxEmail_Reg
            // 
            textBoxEmail_Reg.Location = new Point(662, 357);
            textBoxEmail_Reg.Margin = new Padding(3, 4, 3, 4);
            textBoxEmail_Reg.Name = "textBoxEmail_Reg";
            textBoxEmail_Reg.Size = new Size(215, 27);
            textBoxEmail_Reg.TabIndex = 2;
            // 
            // labelUsername_Reg
            // 
            labelUsername_Reg.AutoSize = true;
            labelUsername_Reg.Location = new Point(662, 275);
            labelUsername_Reg.Name = "labelUsername_Reg";
            labelUsername_Reg.Size = new Size(75, 20);
            labelUsername_Reg.TabIndex = 1;
            labelUsername_Reg.Text = "Username";
            // 
            // textBoxUsername_Reg
            // 
            textBoxUsername_Reg.Location = new Point(662, 299);
            textBoxUsername_Reg.Margin = new Padding(3, 4, 3, 4);
            textBoxUsername_Reg.Name = "textBoxUsername_Reg";
            textBoxUsername_Reg.Size = new Size(215, 27);
            textBoxUsername_Reg.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1641, 964);
            Controls.Add(panelChat);
            Controls.Add(panelRegistrate);
            Controls.Add(panelLogin);
            Controls.Add(panelPayAttention);
            Margin = new Padding(3, 4, 3, 4);
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
        private Button buttonCreateChat;
    }
}
