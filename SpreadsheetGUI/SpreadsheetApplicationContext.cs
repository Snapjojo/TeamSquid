//Hulk Buster

using System;
using System.Windows.Forms;

namespace SpreadsheetGUI {
    class SpreadsheetApplicationContext : ApplicationContext {
        // Number of open forms
        private int windowCount = 0;
        private static Controller controller;

        // Singleton ApplicationContext
        private static SpreadsheetApplicationContext context;

        /// <summary>
        /// Private constructor for singleton pattern
        /// </summary>
        private SpreadsheetApplicationContext() {
        }

        /// <summary>
        /// Returns the one DemoApplicationContext.
        /// </summary>
        public static SpreadsheetApplicationContext GetContext() {
            if (context == null) {
                context = new SpreadsheetApplicationContext();
            }
            return context;
        }

        /// <summary>
        /// Runs a form in this application context
        /// </summary>
        public bool RunNew() {
            // Create the window and the controller
            SpreadsheetGui window = new SpreadsheetGui();
            controller = new Controller(window);

            // One more form is running
            windowCount++;

            // When this form closes, we want to find out
            window.FormClosed += (o, e) => { if (--windowCount <= 0) ExitThread(); };

            // Run the form
            if (window.active)
            {
                window.Show();
                return true;
            }
            return false;
        }

        public static Controller getController()
        {
            return controller;
        }

        /// <summary>
        /// Runs a form in this application context
        /// </summary>
        public void OpenNew(String filename) {
            // Create the window and the controller
            SpreadsheetGui window = new SpreadsheetGui();
            new Controller(window, filename);

            // One more form is running
            windowCount++;

            // When this form closes, we want to find out
            window.FormClosed += (o, e) => { if (--windowCount <= 0) ExitThread(); };

            // Run the form
            window.Show();
        }


        /// <summary>
        /// Runs a form in this application context
        /// </summary>
        public void OpenHelp() {
            // Create the window and the controller
            HelpGui window = new HelpGui();

            // One more form is running
            windowCount++;

            // When this form closes, we want to find out
            window.FormClosed += (o, e) => { if (--windowCount <= 0) ExitThread(); };

            // Run the form
            window.Show();
        }

    }
}
