using Formulas;
using SS;
using SSGui;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using NetworkingController;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpreadsheetGUI
{
    public class Controller
    {
        //  Private Properties
        private static readonly object _lock = new object();
        private static Socket socket;

        //  Public properties
        public Form MyForm { get; set; }
        public List<string> SpreadsheetNames;

        private SpreadsheetView window;

        private Spreadsheet ssModule;

        /// <summary>
        /// Constructor contollor for when a new/blank spreadsheet is added.
        /// </summary>
        /// <param name="window"></param>
        public Controller(SpreadsheetView window)
        {
            this.window = window;
            ssModule = new Spreadsheet(new Regex("^[a-zA-Z]{1}[1-9]{1}[0-9]?$"));
            window.NewEvent += HandleNew;
            window.FileChosenEvent += HandleFileChosen;
            window.CloseEvent += HandleClose;
            window.SelectionEvent += HandleChange;
            window.UpdateEvent += HandleUpdate;
            window.SaveEvent += HandleSave;
        }

        /// <summary>
        /// Constructor for opening spreadsheet.
        /// </summary>
        /// <param name="window"></param>
        /// <param name="filename"></param>
        public Controller(SpreadsheetView window, String filename) : this(window)
        {

            TextReader openfile = null;
            try
            {
                ssModule = new Spreadsheet(openfile = File.OpenText(filename), new Regex("^[a-zA-Z]{1}[1-9]{1}[0-9]?$"));
                window.NewEvent += HandleNew;
                DrawFromFile();
            }
            catch
            {
                MessageBox.Show("Error occured when trying to open file.");
            }
        }

        /// <summary>
        /// Handles a request to close the window
        /// </summary>
        private void HandleClose()
        {
            window.SetChanged(ssModule.Changed);
        }

        /// <summary>
        /// Handles a request to open a new window.
        /// </summary>
        private void HandleNew()
        {
            window.OpenNew();
        }


        /// <summary>
        /// When a cell is selected, 
        /// updates the textboxes for contents, name, and value.
        /// </summary>
        /// <param name="name"></param>
        private void HandleChange(String name)
        {
            Object content = ssModule.GetCellContents(name);
            String convertContents;

            if (content.GetType() == typeof(Formula))
            {
                convertContents = "=" + content.ToString();
            }
            else
            {
                convertContents = content.ToString();
            }
            window.UpdateContentBox(convertContents);
            window.UpdateValueBox(ssModule.GetCellValue(name).ToString());
            window.UpdateNameBox(name);
        }

        /// <summary>
        /// When content box is changed, updates cell's contents
        /// redraws form values. If an error occurs updates Error Label.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="content"></param>
        private void HandleUpdate(int col, int row, String content)
        {

            String name = window.GetName(col, row);
            try
            {
                ssModule.SetContentsOfCell(name, content);

                window.UpdateErrorLabel(false, "");
                window.DrawCell(col, row, ssModule.GetCellValue(name).ToString());
                DrawFromFile();
                HandleChange(name);
            }
            catch (Exception e)
            {
                window.UpdateErrorLabel(true, "You have attempted to add a " + e.GetType().ToString() + " at " + name);
            }

        }

        /// <summary>
        /// Handles a request to open a file.
        /// </summary>
        private void HandleFileChosen(String filename)
        {
            window.OpenExisting(filename);
        }

        /// <summary>
        /// Saves the spreadsheet
        /// </summary>
        /// <param name="filename"></param>
        private void HandleSave(String filename)
        {
            TextWriter saveFile = null;
            ssModule.Save(saveFile = File.CreateText(filename));
            saveFile.Close();
        }

        /// <summary>
        /// Updates all cell values in spreasheet panel.
        /// </summary>
        private void DrawFromFile()
        {

            string temp = "";
            foreach (String cell in ssModule.GetNamesOfAllNonemptyCells())
            {
                temp += cell + ", ";
                int col = cell[0] - 65;
                int row = Convert.ToInt32(cell.Substring(1)) - 1;

                window.DrawCell(col, row, ssModule.GetCellValue(cell).ToString());
            }
        }

        //
        //Network interfacing beyond this point.
        //


        /// <summary>
        /// This method processes the initial handshake with the server.
        /// </summary>
        /// <param name="ss"></param>
        private void FirstContact(SocketState ss)
        {
            // Save the newly-created socket
            socket = ss.theSocket;

            //  Wait for the connection to be finished (avg time is 20 ms)
            int time = 5;
            int counter = 0;

            //  Put thread to sleep in small increments until the socket is connected
            while (!socket.Connected)
            {
                Thread.Sleep(time);
                counter += time;
                //  If we've been waiting for longer than 5 seconds, give up
                if (counter >= 5000)
                {
                    throw new Exception("Could not connect to the server after 5 seconds!");
                }
            }

            ////  Let the server speak to us
            Network.GetData(ss);



            // TODO Handle initial connectivity protocol
            string totalData = ss.sb.ToString();
            string[] parts = Regex.Split(totalData, @"(?<=[\n])");

            // Process only the first two messages
            lock (_lock)
            {
                foreach (string p in parts)
                {
                    //Add "spreadsheet name" to list of connectable spreadsheets.
                }
            }

            //Set the callback to be our processMessage method.
            ss.callMe = ProcessMessage;
        }


        /// <summary>
        /// This method processes server messages, line by line, from a supplied SocketState's string buffer.
        /// </summary>
        /// <param name="ss"></param>
        private void ProcessMessage(SocketState ss)
        {
            string totalData = ss.sb.ToString();
            string[] parts = Regex.Split(totalData, @"(?<=[\n])");

            // Loop until we have processed all messages.
            lock (_lock)
            {
                foreach (string p in parts)
                {
                    // Ignore empty strings added by the regex splitter
                    if (p.Length == 0)
                    {
                        ss.sb.Remove(0, p.Length);
                        continue;
                    }

                    // The regex splitter will include the last string even if it doesn't end with a '\n',
                    if (p[p.Length - 1] != '\n')
                        break;

                    JObject obj = JObject.Parse(p);
                    //TODO Do something with string segment (populate spreadsheet or create master string to populate spreadsheet.)
                    // Do we even need to parse individual lines? Maybe take every individual line and just make edit to SS as they come.

                    // Then remove it from the SocketState's growable buffer
                    ss.sb.Remove(0, p.Length);
                }
            }
            try
            {
                MyForm.Invoke(new MethodInvoker(() => MyForm.Invalidate(true))); //TODO Validate necessity
            }
            //  When the window is closed, this throws an exception. Will now close more gracefully
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Initializes connection from the View
        /// </summary>
        /// <param name="address">Where to find server</param>
        /// <param name="username">The user's username</param>
        /// <param name="password">The user's password</param>
        /// <param name="spreadsheet">The user's requested spreadsheet</param>
        /// <returns>True if connection is successful, false if not.</returns>
        public bool StartConnection(string address, string username, string password)
        {
            //  Connect to Socket
            try
            {
                //  Give network FirstContact so we can get our variables before reading more msgs
                Network.ConnectToServer(address, username, password, FirstContact, ProcessMessage);
            }
            //  If failed, let View know
            catch (Exception)
            {
                return false;
            }
            //  If no exceptions, no problem!
            return true;
        }


        public IEnumerable<string> GetSpreadsheetNames()
        {
            SpreadsheetNames = new List<string>();
            //  TODO ask server for names

            /***************DELETE***************/
            yield return "hello";
            yield return "how";
            yield return "are";
            yield return "you";
            yield return "today";
            yield return "?";
            /***************DELETE***************/
        }


        /// <summary>
        /// Sends server request Json messages based around a supplied enum key.
        /// </summary>
        /// <param name="keyword"></param>
        public void SendJson(MessageKey key, int col, int row)
        {
            switch (key)
            {
                case MessageKey.Edit:
                    {
                        Message message = new Message();
                        string jsonMessage;
                        string cellName = window.GetName(col, row);
                        var cellValue = ssModule.GetCellContents(cellName);
                        List<string> dependees = new List<string>();
                        foreach (string dependee in ssModule.dg.GetDependees(cellName))
                        {
                            dependees.Add(dependee);
                        }
                        message.type = "edit";
                        message.value = cellValue; //TODO Verify this cast doesn't cause issue.
                        message.cell = cellName;
                        message.dependencies = dependees;
                        Console.WriteLine(message);

                        //Format jsonMessage to ignore null properties.
                        jsonMessage = JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });

                        Console.WriteLine(jsonMessage);
                        Network.Send(socket, jsonMessage);
                        break;
                    }
                case MessageKey.Open:
                    {
                        Message message = new Message();
                        message.type = "open";
                        message.name = "";//TODO pipe in spreadsheet name from list.

                        string jsonMessage = JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });

                        Network.Send(socket, jsonMessage);
                        break;
                    }
                case MessageKey.Undo:
                    {
                        Message message = new Message();
                        message.type = "undo";

                        string jsonMessage = JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });

                        Network.Send(socket, jsonMessage);
                        break;
                    }
                case MessageKey.Revert:
                    {
                        Message message = new Message();
                        string cellName = window.GetName(col, row);
                        message.type = "revert";
                        message.cell = cellName;

                        string jsonMessage = JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });

                        Network.Send(socket, jsonMessage);
                        break;
                    }
                default:
                    break;
            }
        }

        public enum MessageKey
        {
            Edit = 0,
            Open = 1,
            Undo = 2,
            Revert = 3
        }
    }
}


