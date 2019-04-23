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
        public delegate void ReportSelection(string name);
        ReportSelection report;

        // Delegate to update Spreadsheet options
        public delegate IEnumerable<string> GetOptions();
        GetOptions options;

        string[] names;

        public OpenForm(GetOptions options, ReportSelection report)
        {
            this.options = options;
            this.report = report;
            InitializeComponent();
            RefreshNames();
            options_box.SetSelected(0, true);
        }

        private void RefreshNames()
        {
            options_box.Items.Clear();
            options_box.Items.Add(" - New Spreadsheet - ");
            error_label.Text = "Loading options...";

            foreach (string option in options())
                options_box.Items.Add(option);
            error_label.Text = "Updated " + DateTime.Now.ToString();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            RefreshNames();
        }

        private void open_button_Click(object sender, EventArgs e)
        {
            error_label.Text = "Opening...";
            
            //  Report the title of the spreadsheet
            report(options_box.SelectedItem.ToString());
        }
    }
}
