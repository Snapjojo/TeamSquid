namespace SpreadsheetGUI
{
    partial class LoginForm
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
            this.server_label = new System.Windows.Forms.Label();
            this.username_label = new System.Windows.Forms.Label();
            this.password_label = new System.Windows.Forms.Label();
            this.server_text = new System.Windows.Forms.TextBox();
            this.username_text = new System.Windows.Forms.TextBox();
            this.password_text = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.connect_button = new System.Windows.Forms.Button();
            this.error_text = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // server_label
            // 
            this.server_label.AutoSize = true;
            this.server_label.Location = new System.Drawing.Point(107, 31);
            this.server_label.Name = "server_label";
            this.server_label.Size = new System.Drawing.Size(54, 17);
            this.server_label.TabIndex = 0;
            this.server_label.Text = "Server:";
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Location = new System.Drawing.Point(107, 91);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(77, 17);
            this.username_label.TabIndex = 1;
            this.username_label.Text = "Username:";
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(106, 129);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(77, 17);
            this.password_label.TabIndex = 2;
            this.password_label.Text = "Password: ";
            // 
            // server_text
            // 
            this.server_text.Location = new System.Drawing.Point(190, 31);
            this.server_text.Name = "server_text";
            this.server_text.Size = new System.Drawing.Size(145, 22);
            this.server_text.TabIndex = 3;
            // 
            // username_text
            // 
            this.username_text.Location = new System.Drawing.Point(190, 88);
            this.username_text.Name = "username_text";
            this.username_text.Size = new System.Drawing.Size(145, 22);
            this.username_text.TabIndex = 4;
            // 
            // password_text
            // 
            this.password_text.Location = new System.Drawing.Point(190, 126);
            this.password_text.Name = "password_text";
            this.password_text.Size = new System.Drawing.Size(145, 22);
            this.password_text.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "____________________________________________";
            // 
            // connect_button
            // 
            this.connect_button.Location = new System.Drawing.Point(177, 173);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(75, 23);
            this.connect_button.TabIndex = 7;
            this.connect_button.Text = "Connect";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_Click);
            // 
            // error_text
            // 
            this.error_text.AutoSize = true;
            this.error_text.Location = new System.Drawing.Point(12, 220);
            this.error_text.Name = "error_text";
            this.error_text.Size = new System.Drawing.Size(0, 17);
            this.error_text.TabIndex = 8;
            this.error_text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 246);
            this.Controls.Add(this.error_text);
            this.Controls.Add(this.connect_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password_text);
            this.Controls.Add(this.username_text);
            this.Controls.Add(this.server_text);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.username_label);
            this.Controls.Add(this.server_label);
            this.Name = "StartupForm";
            this.Text = "Welcome - Please Log In";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label server_label;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.TextBox server_text;
        private System.Windows.Forms.TextBox username_text;
        private System.Windows.Forms.TextBox password_text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.Label error_text;
    }
}