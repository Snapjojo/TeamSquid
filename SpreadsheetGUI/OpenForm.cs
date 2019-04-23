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
            open_button.Enabled = false;
            label_new_ss.Visible = false;
            text_new_ss.Visible = false;
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

        private void open_button_Click(object sender, EventArgs e)
        {
            error_label.Text = "Opening...";

            //  If new spreadsheet is selected
            if (options_box.SelectedItem.ToString() == " - New Spreadsheet - ")
                report(text_new_ss.Text);

            //  Report the title of the spreadsheet
            else
                report(options_box.SelectedItem.ToString());
        }

        private void options_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selection;
            try
            {
                selection = options_box.SelectedItem.ToString();
            }
            catch (Exception) { return; }
            if (options_box.SelectedItem.ToString() != " - New Spreadsheet - ")
            {
                open_button.Enabled = true;
                label_new_ss.Visible = false;
                text_new_ss.Visible = false;
            }
            else
            {
                label_new_ss.Visible = true;
                text_new_ss.Visible = true;
            }
        }

        private void text_new_ss_TextChanged(object sender, EventArgs e)
        {
            if (text_new_ss.Text != "" && !text_new_ss.Text.Contains("New Spreadsheet"))
                open_button.Enabled = true;
            else
                open_button.Enabled = false;
        }
    }
}
