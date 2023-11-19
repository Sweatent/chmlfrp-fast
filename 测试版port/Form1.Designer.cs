namespace 测试版port
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
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(506, 164);
            button1.Name = "button1";
            button1.Size = new Size(145, 41);
            button1.TabIndex = 0;
            button1.Text = "启动隧道！";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Location = new Point(119, 136);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(338, 92);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 136);
            label1.Name = "label1";
            label1.Size = new Size(64, 24);
            label1.TabIndex = 2;
            label1.Text = "日志：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 57);
            label2.Name = "label2";
            label2.Size = new Size(121, 24);
            label2.TabIndex = 4;
            label2.Text = "token/密钥：";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(139, 54);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(299, 30);
            textBox1.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 254);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private Button button1;
        private RichTextBox richTextBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
    }
}