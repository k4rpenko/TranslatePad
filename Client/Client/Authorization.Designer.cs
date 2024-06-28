namespace Client
{
    partial class Authorization
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Sign_up_pass = new System.Windows.Forms.TextBox();
            this.Sign_up_pass2 = new System.Windows.Forms.TextBox();
            this.Sign_up_email = new System.Windows.Forms.TextBox();
            this.Sign_up_button = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label_in = new System.Windows.Forms.Label();
            this.Sign_in_button = new System.Windows.Forms.Button();
            this.Sign_in_pass = new System.Windows.Forms.TextBox();
            this.Sign_in_email = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(230, 15);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1003, 610);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.Sign_up_pass);
            this.tabPage1.Controls.Add(this.Sign_up_pass2);
            this.tabPage1.Controls.Add(this.Sign_up_email);
            this.tabPage1.Controls.Add(this.Sign_up_button);
            this.tabPage1.Location = new System.Drawing.Point(4, 46);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(995, 560);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sign up";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.label4.Location = new System.Drawing.Point(343, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(272, 26);
            this.label4.TabIndex = 9;
            this.label4.Text = "Використовуйте електронну пошту організації, \r\nщоб легко співпрацювати з колегами" +
    " по команді";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 347);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 5;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.checkBox1.Location = new System.Drawing.Point(346, 317);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Show password";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Sign_up_pass
            // 
            this.Sign_up_pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Sign_up_pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Sign_up_pass.Font = new System.Drawing.Font("Segoe UI", 15.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Sign_up_pass.ForeColor = System.Drawing.Color.White;
            this.Sign_up_pass.Location = new System.Drawing.Point(346, 266);
            this.Sign_up_pass.Name = "Sign_up_pass";
            this.Sign_up_pass.Size = new System.Drawing.Size(300, 35);
            this.Sign_up_pass.TabIndex = 3;
            this.Sign_up_pass.UseSystemPasswordChar = true;
            this.Sign_up_pass.TextChanged += new System.EventHandler(this.Sign_up_pass_TextChanged);
            // 
            // Sign_up_pass2
            // 
            this.Sign_up_pass2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Sign_up_pass2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Sign_up_pass2.Font = new System.Drawing.Font("Segoe UI", 15.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Sign_up_pass2.ForeColor = System.Drawing.Color.White;
            this.Sign_up_pass2.Location = new System.Drawing.Point(346, 213);
            this.Sign_up_pass2.Name = "Sign_up_pass2";
            this.Sign_up_pass2.Size = new System.Drawing.Size(300, 35);
            this.Sign_up_pass2.TabIndex = 2;
            this.Sign_up_pass2.UseSystemPasswordChar = true;
            this.Sign_up_pass2.TextChanged += new System.EventHandler(this.Sign_up_pass2_TextChanged);
            // 
            // Sign_up_email
            // 
            this.Sign_up_email.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Sign_up_email.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Sign_up_email.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Sign_up_email.ForeColor = System.Drawing.Color.White;
            this.Sign_up_email.Location = new System.Drawing.Point(346, 98);
            this.Sign_up_email.Multiline = true;
            this.Sign_up_email.Name = "Sign_up_email";
            this.Sign_up_email.Size = new System.Drawing.Size(300, 35);
            this.Sign_up_email.TabIndex = 1;
            this.Sign_up_email.TextChanged += new System.EventHandler(this.Sign_up_email_TextChanged);
            // 
            // Sign_up_button
            // 
            this.Sign_up_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(131)))), ((int)(((byte)(226)))));
            this.Sign_up_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Sign_up_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Sign_up_button.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Sign_up_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Sign_up_button.Location = new System.Drawing.Point(431, 377);
            this.Sign_up_button.Name = "Sign_up_button";
            this.Sign_up_button.Size = new System.Drawing.Size(130, 45);
            this.Sign_up_button.TabIndex = 0;
            this.Sign_up_button.Text = "Sign up";
            this.Sign_up_button.UseVisualStyleBackColor = false;
            this.Sign_up_button.Click += new System.EventHandler(this.Sign_up_button_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.checkBox2);
            this.tabPage2.Controls.Add(this.label_in);
            this.tabPage2.Controls.Add(this.Sign_in_button);
            this.tabPage2.Controls.Add(this.Sign_in_pass);
            this.tabPage2.Controls.Add(this.Sign_in_email);
            this.tabPage2.Location = new System.Drawing.Point(4, 46);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(995, 560);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sign in";
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(85)))), ((int)(((byte)(90)))));
            this.label3.Location = new System.Drawing.Point(343, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(272, 26);
            this.label3.TabIndex = 8;
            this.label3.Text = "Використовуйте електронну пошту організації, \r\nщоб легко співпрацювати з колегами" +
    " по команді";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.checkBox2.Location = new System.Drawing.Point(346, 265);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(108, 17);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "Show password";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label_in
            // 
            this.label_in.AutoSize = true;
            this.label_in.Location = new System.Drawing.Point(358, 300);
            this.label_in.Name = "label_in";
            this.label_in.Size = new System.Drawing.Size(0, 13);
            this.label_in.TabIndex = 6;
            this.label_in.Click += new System.EventHandler(this.label2_Click);
            // 
            // Sign_in_button
            // 
            this.Sign_in_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(131)))), ((int)(((byte)(226)))));
            this.Sign_in_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Sign_in_button.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sign_in_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Sign_in_button.Location = new System.Drawing.Point(431, 377);
            this.Sign_in_button.Name = "Sign_in_button";
            this.Sign_in_button.Size = new System.Drawing.Size(130, 45);
            this.Sign_in_button.TabIndex = 3;
            this.Sign_in_button.Text = "Sign in";
            this.Sign_in_button.UseVisualStyleBackColor = false;
            this.Sign_in_button.Click += new System.EventHandler(this.Sign_in_button_Click);
            // 
            // Sign_in_pass
            // 
            this.Sign_in_pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Sign_in_pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Sign_in_pass.Font = new System.Drawing.Font("Segoe UI", 15.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Sign_in_pass.ForeColor = System.Drawing.SystemColors.Window;
            this.Sign_in_pass.Location = new System.Drawing.Point(346, 213);
            this.Sign_in_pass.Name = "Sign_in_pass";
            this.Sign_in_pass.Size = new System.Drawing.Size(300, 35);
            this.Sign_in_pass.TabIndex = 2;
            this.Sign_in_pass.UseSystemPasswordChar = true;
            this.Sign_in_pass.TextChanged += new System.EventHandler(this.Sign_in_pass_TextChanged);
            // 
            // Sign_in_email
            // 
            this.Sign_in_email.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Sign_in_email.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Sign_in_email.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Sign_in_email.ForeColor = System.Drawing.Color.White;
            this.Sign_in_email.Location = new System.Drawing.Point(346, 98);
            this.Sign_in_email.Multiline = true;
            this.Sign_in_email.Name = "Sign_in_email";
            this.Sign_in_email.Size = new System.Drawing.Size(300, 35);
            this.Sign_in_email.TabIndex = 1;
            this.Sign_in_email.TextChanged += new System.EventHandler(this.Sign_in_email_TextChanged);
            // 
            // Authorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1004, 611);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.Color.Cyan;
            this.Name = "Authorization";
            this.Text = "Authorization";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox Sign_up_pass2;
        private System.Windows.Forms.TextBox Sign_up_email;
        private System.Windows.Forms.Button Sign_up_button;
        private System.Windows.Forms.Button Sign_in_button;
        private System.Windows.Forms.TextBox Sign_in_pass;
        private System.Windows.Forms.TextBox Sign_in_email;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_in;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Sign_up_pass;
    }
}