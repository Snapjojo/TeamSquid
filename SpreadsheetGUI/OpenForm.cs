using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public partial class OpenForm : Form
    {
        // Delegate to return to GUI on successful login
        public delegate void StartApp();
        StartApp start;

        // Delegate to update Spreadsheet options
        public delegate IEnumerable<string> GetOptions();
        GetOptions options;

        public OpenForm(GetOptions options, StartApp start)
        {
            this.options = options;
            this.start = start;
            InitializeComponent();
            RefreshNames();
        }

        private void RefreshNames()
        {
            options_box = new ListBox();
            options_box.Items.Add(" - New Spreadsheet - ");
            error_label.Text = "Loading options...";
            foreach (string option in options())
                options_box.Items.Add(option);
        }
    }
}
