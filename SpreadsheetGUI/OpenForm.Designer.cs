namespace SpreadsheetGUI
{
    partial class OpenForm
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
            this.options_box = new System.Windows.Forms.ListBox();
            this.error_label = new System.Windows.Forms.Label();
            this.open_button = new System.Windows.Forms.Button();
            this.refresh_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // options_box
            // 
            this.options_box.FormattingEnabled = true;
            this.options_box.ItemHeight = 16;
            this.options_box.Location = new System.Drawing.Point(12, 12);
            this.options_box.Name = "options_box";
            this.options_box.Size = new System.Drawing.Size(379, 212);
            this.options_box.TabIndex = 0;
            // 
            // error_label
            // 
            this.error_label.AutoSize = true;
            this.error_label.Location = new System.Drawing.Point(12, 318);
            this.error_label.Name = "error_label";
            this.error_label.Size = new System.Drawing.Size(0, 17);
            this.error_label.TabIndex = 1;
            // 
            // open_button
            // 
            this.open_button.Location = new System.Drawing.Point(12, 230);
            this.open_button.Name = "open_button";
            this.open_button.Size = new System.Drawing.Size(184, 29);
            this.open_button.TabIndex = 2;
            this.open_button.Text = "Open";
            this.open_button.UseVisualStyleBackColor = true;
            // 
            // refresh_button
            // 
            this.refresh_button.Location = new System.Drawing.Point(207, 230);
            this.refresh_button.Name = "refresh_button";
            this.refresh_button.Size = new System.Drawing.Size(184, 29);
            this.refresh_button.TabIndex = 3;
            this.refresh_button.Text = "Refresh";
            this.refresh_button.UseVisualStyleBackColor = true;
            // 
            // OpenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 344);
            this.Controls.Add(this.refresh_button);
            this.Controls.Add(this.open_button);
            this.Controls.Add(this.error_label);
            this.Controls.Add(this.options_box);
            this.Name = "OpenForm";
            this.Text = "Open";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox options_box;
        private System.Windows.Forms.Label error_label;
        private System.Windows.Forms.Button open_button;
        private System.Windows.Forms.Button refresh_button;
    }
}