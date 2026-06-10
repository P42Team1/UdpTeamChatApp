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
            groupBox2 = new GroupBox();
            textBox8 = new TextBox();
            textBox7 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            button4 = new Button();
            textBox6 = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(440, 91);
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
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(196, 27);
            textBox1.TabIndex = 1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(520, 37);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 39);
            label1.Name = "label1";
            label1.Size = new Size(114, 20);
            label1.TabIndex = 3;
            label1.Text = "ServerIPAddress";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(520, 14);
            label2.Name = "label2";
            label2.Size = new Size(35, 20);
            label2.TabIndex = 4;
            label2.Text = "Port";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(43, 112);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(344, 27);
            textBox4.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(43, 89);
            label3.Name = "label3";
            label3.Size = new Size(67, 20);
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
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(262, 39);
            label4.Name = "label4";
            label4.Size = new Size(76, 20);
            label4.TabIndex = 9;
            label4.Text = "ServerPort";
            // 
            // button2
            // 
            button2.Location = new Point(673, 36);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 10;
            button2.Text = "Connect";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(799, 36);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 11;
            button3.Text = "Disconnect";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox5);
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(21, 82);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(677, 484);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Send to everyone";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBox8);
            groupBox2.Controls.Add(textBox7);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(button4);
            groupBox2.Controls.Add(textBox6);
            groupBox2.Location = new Point(728, 82);
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
            textBox7.Size = new Size(445, 27);
            textBox7.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(39, 39);
            label5.Name = "label5";
            label5.Size = new Size(68, 20);
            label5.TabIndex = 14;
            label5.Text = "User Port";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(186, 39);
            label6.Name = "label6";
            label6.Size = new Size(67, 20);
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
            textBox6.Size = new Size(125, 27);
            textBox6.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1436, 578);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(textBox3);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
    }
}
