using SSGui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace SpreadsheetGUI {
    public partial class SpreadsheetGui : Form, SpreadsheetView {
        private Controller controller;
        private LoginForm login_form;
        private OpenForm open;
        private bool logged_in;
        private string ss_selected;
        public bool active;

        /// <summary>
        /// Intializes Spreadsheet
        /// </summary>
        public SpreadsheetGui()
        {
            controller = new Controller(this);
            active = true;

            //  Launch login form
            logged_in = false;
            login_form = new LoginForm(Login);
            login_form.FormClosed += new FormClosedEventHandler(CloseApp);
            Application.Run(login_form);

            //  Launch Spreadsheet
            InitializeComponent();

        }

        /// <summary>
        /// Handles logging in as called by LoginForm. Routes to Controller.StartConnection.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="user"></param>
        /// <param name="pssw"></param>
        /// <returns></returns>
        public bool Login(string server)
        {
            logged_in = controller.StartConnection(server);
            if (logged_in)
            {
                controller.MyForm = this;
                controller.SetAuthentication(login_form.password_text.Text, login_form.username_text.Text);
                return true;
            }
            return false;
        }

        public void GetSelection(string ss_selected)
        {
            this.ss_selected = ss_selected;

            //  Kill open form
            open.Close();
        }

        public void CloseApp(object sender, FormClosedEventArgs e)
        {
            if (!logged_in || ss_selected == "")
            {
                active = false;
                this.Close();
                Application.Exit();
            }
        }

        public string Open()
        {
            //  Launch open form
            open = new OpenForm(controller.GetSpreadsheetNames, GetSelection);
            open.FormClosed += new FormClosedEventHandler(CloseApp);
            open.ShowDialog();

            return ss_selected;
        }

        public void ReceiveSheetName(string name)
        {

        }

        /// <summary>
        /// Panel Changed
        /// </summary>
        private bool Changed;

        /// <summary>
        /// Fired when a file is chosen with a file dialog.
        /// The parameter is the chosen filename.
        /// </summary>
        public event Action<string> FileChosenEvent;

        /// <summary>
        /// Fired when a close action is requested.
        /// </summary>
        public event Action CloseEvent;

        /// <summary>
        /// Fired when a new action is requested.
        /// </summary>
        public event Action NewEvent;

        /// <summary>
        /// Fires when a cell is updated
        /// </summary>
        public event Action<int, int, string> UpdateEvent;

        /// <summary>
        /// Fires When a cell is selected
        /// </summary>
        public event Action<string> SelectionEvent;


        /// <summary>
        /// Inital load events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Spreadsheet_Load(object sender, EventArgs e) {
            spreadsheetPanel1.SetSelection(0, 0);
            SpreadsheetPanel1_SelectionChanged(spreadsheetPanel1);
            ErrorLabel.Visible = false;
            controller = SpreadsheetApplicationContext.getController();
        }

        /// <summary>
        /// Catches arrow keys for switching cells
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Spreadsheet_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right
                    || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) {
                ArrowKeys(e);
                SpreadsheetPanel1_SelectionChanged(spreadsheetPanel1);
            }
        }

        /// <summary>
        /// Opens new Window
        /// </summary>
        public void OpenNew() {
            //TODO Reimplement multiple windows using server. Link this to "open" with server and an existing window somehow.
            SpreadsheetApplicationContext.GetContext().RunNew();
        }

        /// <summary>
        /// Open an existing file in a new window
        /// </summary>
        /// <param name="filename"></param>
        public void OpenExisting(String filename) {
            SpreadsheetApplicationContext.GetContext().OpenNew(filename);
        }

        /// <summary>
        /// Handles the Click event of the openItem control.
        /// </summary>
        private void OpenItem_Click(object sender, EventArgs e) {
            string spreadsheetName = Open();
            controller.SendJson(Controller.MessageKey.Open, 0, 0, spreadsheetName);
        }

        /// <summary>
        /// Handles the Click event of the closeItem control.
        /// </summary>
        private void CloseItem_Click(object sender, EventArgs e) {
            Close();
        }

        /// <summary>
        /// Handles when enter is pressed while inside Contentbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentBox_KeyDown(object sender, KeyEventArgs e) {

            //When keypressed is enter
            switch (e.KeyCode) {
                case Keys.Enter:
                    if (UpdateEvent != null) {
                        //Update Cell
                        int row, col;
                        spreadsheetPanel1.GetSelection(out col, out row);
                        spreadsheetPanel1.Select();
                        controller.SendJson(Controller.MessageKey.Edit, col, row, ContentBox.Text);

                        //Move to next cell
                        Spreadsheet_KeyDown(spreadsheetPanel1, new KeyEventArgs(Keys.Down));
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handles new cell being selected and helpts to update
        /// Content Boxes.
        /// </summary>
        /// <param name="sender"></param>
        private void SpreadsheetPanel1_SelectionChanged(SpreadsheetPanel sender) {
            if (SelectionEvent != null) {
                int row, col;
                spreadsheetPanel1.GetSelection(out col, out row);
                SelectionEvent(GetName(col, row));
                ContentBox.Select();
                ContentBox.SelectionStart = 0;
                ContentBox.SelectionLength = ContentBox.Text.Length;
                controller.CanUpdate();
            }
        }

        /// <summary>
        /// Dispalys the Value of a cell.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="Value"></param>
        public void DrawCell(int col, int row, String Value) {
            spreadsheetPanel1.SetValue(col, row, Value);
        }

        /// <summary>
        /// Sets the value of Changed
        /// </summary>
        /// <param name="isChanged"></param>
        public void SetChanged(bool isChanged) {
            Changed = isChanged;
        }

        /// <summary>
        /// Opens the help menu if clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e) {
            SpreadsheetApplicationContext.GetContext().OpenHelp();
        }

        /// <summary>
        /// When called determines the arrow key.
        /// Shifts to the appropriate cell and scrolls page when necessary.
        /// </summary>
        /// <param name="e"></param>
        private void ArrowKeys(KeyEventArgs e) {
            int row, col;
            spreadsheetPanel1.GetSelection(out col, out row);
            double firstRow = spreadsheetPanel1.GetVScollPosition();
            double lastRow = ((spreadsheetPanel1.Bounds.Height - 30 - 20) / 20) + spreadsheetPanel1.GetVScollPosition();
            double mostLeft = spreadsheetPanel1.GetHScollPosition();
            double mostRight = ((spreadsheetPanel1.Bounds.Width - 30 - 20) / 80) + spreadsheetPanel1.GetHScollPosition();

            if (e.KeyCode == Keys.Right) {
                spreadsheetPanel1.SetSelection(col + 1, row);
                e.Handled = true;
                if (col + 1 == mostRight) {
                    spreadsheetPanel1.SetHScollPosition(1);

                }
            }

            if (e.KeyCode == Keys.Left) {
                spreadsheetPanel1.SetSelection(col - 1, row);
                e.Handled = true;
                if (col == mostLeft) {
                    spreadsheetPanel1.SetHScollPosition(-1);
                }
            }

            if (e.KeyCode == Keys.Down) {
                spreadsheetPanel1.SetSelection(col, row + 1);
                e.Handled = true;
                if (row + 1 == lastRow) {
                    spreadsheetPanel1.SetVScrollPosition(1);
                }

            }

            if (e.KeyCode == Keys.Up) {
                spreadsheetPanel1.SetSelection(col, row - 1);
                e.Handled = true;
                if (row == firstRow) {
                    spreadsheetPanel1.SetVScrollPosition(-1);
                }
            }
        }

        /// <summary>
        /// Updates the Text in NameBox
        /// </summary>
        /// <param name="name"></param>
        public void UpdateNameBox(string name) {
            this.NameBox.Text = name;
        }

        /// <summary>
        /// Updates the text in ContentBox
        /// </summary>
        /// <param name="content"></param>
        public void UpdateContentBox(string content) {
            this.ContentBox.Text = content;
        }

        /// <summary>
        /// Updates text in ValueBox
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueBox(string value) {
            this.ValueBox.Text = value;
        }

        /// <summary>
        /// Converts col and row integers in into Uppercase Cell name
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public String GetName(int col, int row) {
            char alph = (char)(col + 65);
            int num = row + 1;
            String name = alph.ToString() + num.ToString();

            return name;
        }

        /// <summary>
        /// Changes the color of File text when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileToolStripMenuItem_Click(object sender, EventArgs e) {
            if (fileToolStripMenuItem.Enabled) {
                fileToolStripMenuItem.ForeColor = Color.FromArgb(49, 52, 62);
                optionsToolStripMenuItem.ForeColor = Color.White;
            }
            else {
                fileToolStripMenuItem.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Changes the color of Options text when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (fileToolStripMenuItem.Enabled) {
                optionsToolStripMenuItem.ForeColor = Color.FromArgb(49, 52, 62);
                fileToolStripMenuItem.ForeColor = Color.White;
            }
            else {
                optionsToolStripMenuItem.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Changes File and Options back to white.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuStrip1_EnabledChanged(object sender, EventArgs e) {
            optionsToolStripMenuItem.ForeColor = Color.White;
            fileToolStripMenuItem.ForeColor = Color.White;
        }


        /// <summary>
        /// When error occurs makes ErrorLabel visible and updates text.
        /// If error has not occured, updates text and renders invisible.
        /// </summary>
        /// <param name="hasError"></param>
        /// <param name="error"></param>
        public void UpdateErrorLabel(bool hasError, string error) {
            if (hasError) {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = error;
            }
            else {
                ErrorLabel.Visible = false;
                ErrorLabel.Text = "";
            }
        }

        /// <summary>
        /// When form closes check that Changed is false, otherwise prompt to save.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Closing(object sender, FormClosingEventArgs e) {
            // Determine if text has changed in the textbox by comparing to original text.
            if(CloseEvent != null) {
                CloseEvent();
            }
        }

        /// <summary>
        /// The undo message type requests the most recent global change be returned to its previous
        /// state.This message should only include the type field "undo". The undo message type will
        ///rollback changes made by the "revert" message type.Multiple undo messages sequentially
        ///will continue to return the spreadsheet to past states, all the way up to the spreadsheet's
        ///state upon creation.
        /// </summary>
        private void UndoBtn_Click(object sender, EventArgs e)
        {
            controller.SendJson(Controller.MessageKey.Undo);
        }

        /// <summary>
        /// The revert message type requests the most recent global change of a given cell be returned
        ///to its previous state.This message should include the type field “revert” as well as the cell
        ///to have its contents returned to the previous state.The server should perform a circular
        ///dependency check upon receiving a revert message, sending an error code upon finding
        ///one.Multiple revert messages sequentially will continue to return the cell to past states, all
        ///the way up to the cell's state upon creation of the spreadsheet.
        /// </summary>
        private void RevertBtn_Click(object sender, EventArgs e)
        {
            int row, col;
            spreadsheetPanel1.GetSelection(out col, out row);
            controller.SendJson(Controller.MessageKey.Revert, col, row);
        }

    }
}

