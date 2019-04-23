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
            this.label_new_ss = new System.Windows.Forms.Label();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.text_new_ss = new System.Windows.Forms.TextBox();
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
            this.options_box.SelectedIndexChanged += new System.EventHandler(this.options_box_SelectedIndexChanged);
            // 
            // error_label
            // 
            this.error_label.AutoSize = true;
            this.error_label.Location = new System.Drawing.Point(12, 318);
            this.error_label.Name = "error_label";
            this.error_label.Size = new System.Drawing.Size(213, 17);
            this.error_label.TabIndex = 1;
            this.error_label.Text = "supercalifragulisticexpialidocious";
            // 
            // open_button
            // 
            this.open_button.Location = new System.Drawing.Point(114, 275);
            this.open_button.Name = "open_button";
            this.open_button.Size = new System.Drawing.Size(184, 29);
            this.open_button.TabIndex = 2;
            this.open_button.Text = "Open";
            this.open_button.UseVisualStyleBackColor = true;
            this.open_button.Click += new System.EventHandler(this.open_button_Click);
            // 
            // label_new_ss
            // 
            this.label_new_ss.AutoSize = true;
            this.label_new_ss.Location = new System.Drawing.Point(12, 227);
            this.label_new_ss.Name = "label_new_ss";
            this.label_new_ss.Size = new System.Drawing.Size(340, 17);
            this.label_new_ss.TabIndex = 3;
            this.label_new_ss.Text = "What would you like to name your new spreadsheet?";
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(369, 13);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(25, 211);
            this.vScrollBar1.TabIndex = 4;
            // 
            // text_new_ss
            // 
            this.text_new_ss.Location = new System.Drawing.Point(15, 247);
            this.text_new_ss.Name = "text_new_ss";
            this.text_new_ss.Size = new System.Drawing.Size(337, 22);
            this.text_new_ss.TabIndex = 5;
            this.text_new_ss.TextChanged += new System.EventHandler(this.text_new_ss_TextChanged);
            // 
            // OpenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 344);
            this.Controls.Add(this.text_new_ss);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.label_new_ss);
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
        private System.Windows.Forms.Label label_new_ss;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.TextBox text_new_ss;
    }
}