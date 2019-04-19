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
    public partial class LoginForm : Form
    {
        // Delegate to start application on successful login
        public delegate void StartApplication();
        StartApplication start;

        // Delegate to Controller's login function
        public delegate bool Login(string a, string b, string c);
        Login login;
        bool logged_in = false;

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="login"></param>
        /// <param name="start"></param>
        public LoginForm(Login login, StartApplication start)
        {
            this.start = start;
            this.login = login;
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connect_button_Click(object sender, EventArgs e)
        {
            error_text.Text = "Logging in to remote server...";
            if (!logged_in)
                Connect();
        }

        /// <summary>
        /// Connects to server when button is clicked.
        /// </summary>
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Connect();
        }


        /// <summary>
        /// Tries connecting when enter key is pressed when password is entered.
        /// </summary>
        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Connect();
            }

        }

        /// <summary>
        /// Initiates connection to the server
        /// </summary>
        private void Connect()
        {
            //TODO GENERATE List of spreadsheets in open dropdown.

            //Ensure fields are filled out
            if (server_text.Text == "")
            {
                MessageBox.Show("Please enter a server.");
                server_text.Focus();
                return;
            }
            if (username_text.Text == "")
            {
                MessageBox.Show("Please enter a username.");
                username_text.Focus();
                return;
            }
            if (password_text.Text == "")
            {
                MessageBox.Show("Please enter a password.");
                password_text.Focus();
                return;
            }

            // Have controller attempt connection
            bool success = login(server_text.Text, username_text.Text, password_text.Text);
            if (success)
            {
                //  Update form access & labels
                error_text.Text = "Success!";
                start();
            }
            else
            {
                error_text.Text = "Server was unavailable or invalid credentials. Please try again.";
                server_text.Focus();
            }
        }
    }
}
