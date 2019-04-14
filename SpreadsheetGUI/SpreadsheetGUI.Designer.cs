using System.Drawing;
using System.Windows.Forms;

namespace SpreadsheetGUI {
    partial class SpreadsheetGui {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.ContentBox = new System.Windows.Forms.TextBox();
            this.ContentLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.ValueLabel = new System.Windows.Forms.Label();
            this.ValueBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.spreadsheetPanel1 = new SSGui.SpreadsheetPanel();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.newSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentBox
            // 
            this.ContentBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContentBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContentBox.Location = new System.Drawing.Point(121, 523);
            this.ContentBox.Name = "ContentBox";
            this.ContentBox.Size = new System.Drawing.Size(875, 26);
            this.ContentBox.TabIndex = 1;
            this.ContentBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContentBox_KeyDown);
            // 
            // ContentLabel
            // 
            this.ContentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ContentLabel.AutoSize = true;
            this.ContentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContentLabel.Location = new System.Drawing.Point(19, 526);
            this.ContentLabel.Name = "ContentLabel";
            this.ContentLabel.Size = new System.Drawing.Size(96, 20);
            this.ContentLabel.TabIndex = 2;
            this.ContentLabel.Text = "Cell Content";
            // 
            // NameLabel
            // 
            this.NameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(19, 562);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(81, 20);
            this.NameLabel.TabIndex = 4;
            this.NameLabel.Text = "Cell Name";
            // 
            // NameBox
            // 
            this.NameBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(253)))));
            this.NameBox.Location = new System.Drawing.Point(121, 562);
            this.NameBox.Name = "NameBox";
            this.NameBox.ReadOnly = true;
            this.NameBox.Size = new System.Drawing.Size(139, 26);
            this.NameBox.TabIndex = 5;
            // 
            // ValueLabel
            // 
            this.ValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ValueLabel.AutoSize = true;
            this.ValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueLabel.Location = new System.Drawing.Point(273, 562);
            this.ValueLabel.Name = "ValueLabel";
            this.ValueLabel.Size = new System.Drawing.Size(80, 20);
            this.ValueLabel.TabIndex = 6;
            this.ValueLabel.Text = "Cell Value";
            // 
            // ValueBox
            // 
            this.ValueBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ValueBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(253)))));
            this.ValueBox.Location = new System.Drawing.Point(363, 562);
            this.ValueBox.Name = "ValueBox";
            this.ValueBox.ReadOnly = true;
            this.ValueBox.Size = new System.Drawing.Size(132, 26);
            this.ValueBox.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(123)))), ((int)(((byte)(123)))));
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MinimumSize = new System.Drawing.Size(0, 40);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.menuStrip1.Size = new System.Drawing.Size(1008, 40);
            this.menuStrip1.Stretch = false;
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(43, 40);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.DropDownClosed += new System.EventHandler(this.menuStrip1_EnabledChanged);
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_Click);
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openItem
            // 
            this.openItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSheetToolStripMenuItem});
            this.openItem.Name = "openItem";
            this.openItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openItem.Size = new System.Drawing.Size(180, 22);
            this.openItem.Text = "Open";
            this.openItem.Click += new System.EventHandler(this.OpenItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(72, 40);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.DropDownClosed += new System.EventHandler(this.menuStrip1_EnabledChanged);
            this.optionsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // fileDialog1
            // 
            this.fileDialog1.FileName = "openFileDialog1";
            // 
            // spreadsheetPanel1
            // 
            this.spreadsheetPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadsheetPanel1.AutoScroll = true;
            this.spreadsheetPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.spreadsheetPanel1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.spreadsheetPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(69)))), ((int)(((byte)(90)))));
            this.spreadsheetPanel1.Location = new System.Drawing.Point(0, 40);
            this.spreadsheetPanel1.Margin = new System.Windows.Forms.Padding(6);
            this.spreadsheetPanel1.MinimumSize = new System.Drawing.Size(300, 300);
            this.spreadsheetPanel1.Name = "spreadsheetPanel1";
            this.spreadsheetPanel1.Size = new System.Drawing.Size(1008, 474);
            this.spreadsheetPanel1.TabIndex = 0;
            this.spreadsheetPanel1.SelectionChanged += new SSGui.SelectionChangedHandler(this.spreadsheetPanel1_SelectionChanged);
            this.spreadsheetPanel1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Spreadsheet_KeyDown);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(1)))), ((int)(((byte)(65)))));
            this.ErrorLabel.Location = new System.Drawing.Point(503, 562);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(44, 20);
            this.ErrorLabel.TabIndex = 9;
            this.ErrorLabel.Text = "Error";
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(252, 11);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(100, 20);
            this.usernameBox.TabIndex = 10;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(191, 14);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(55, 13);
            this.usernameLabel.TabIndex = 11;
            this.usernameLabel.Text = "Username";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(358, 14);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(52, 13);
            this.passwordLabel.TabIndex = 12;
            this.passwordLabel.Text = "password";
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(416, 11);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(100, 20);
            this.passwordBox.TabIndex = 13;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(526, 11);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 14;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // newSheetToolStripMenuItem
            // 
            this.newSheetToolStripMenuItem.Name = "newSheetToolStripMenuItem";
            this.newSheetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newSheetToolStripMenuItem.Text = "New Sheet";
            // 
            // SpreadsheetGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 603);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.ValueBox);
            this.Controls.Add(this.ValueLabel);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.ContentLabel);
            this.Controls.Add(this.ContentBox);
            this.Controls.Add(this.spreadsheetPanel1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(595, 558);
            this.Name = "SpreadsheetGui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spreadsheet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.Load += new System.EventHandler(this.Spreadsheet_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Spreadsheet_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private SSGui.SpreadsheetPanel spreadsheetPanel1;
        private TextBox ContentBox;
        private Label ContentLabel;
        private Label NameLabel;
        private TextBox NameBox;
        private Label ValueLabel;
        private TextBox ValueBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private OpenFileDialog fileDialog1;
        private Label ErrorLabel;
        private SaveFileDialog saveFileDialog1;
        private TextBox usernameBox;
        private Label usernameLabel;
        private Label passwordLabel;
        private TextBox passwordBox;
        private Button connectButton;
        private ToolStripMenuItem newSheetToolStripMenuItem;
    }
}

