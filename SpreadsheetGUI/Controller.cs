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
        private static string username;
        private static string password;

        //  Public properties
        public Form MyForm { get; set; }

        public static List<string> spreadsheetNames;

        public static bool canUpdate;

        private SpreadsheetView window;

        public Spreadsheet ssModule;

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
        }

        internal void SetAuthentication(string password_text, string username_text)
        {
            username = username_text;
            password = password_text;
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
            if (canUpdate)
            {
                Object content = ssModule.GetCellValue(name);
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

                canUpdate = false;
            }
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
                HandleChange(name);
                ssModule.SetContentsOfCell(name, content);

                window.UpdateErrorLabel(false, "");
                window.DrawCell(col, row, ssModule.GetCellValue(name).ToString());
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
                if (counter >= 10000)
                {
                    throw new Exception("Could not connect to the server after 5 seconds!");
                }
            }


            string totalData = ss.sb.ToString();
            string[] parts = Regex.Split(totalData, @"(?<=[\n])");
            parts[0] = parts[0].Substring(0, parts[0].Length - 1);

            JsonSerializerSettings settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            Message message = (Message)JsonConvert.DeserializeObject(parts[0].ToString(), typeof(Message), settings);

            //Ensure this is the same controller created in the view
            SetSpreadsheetNames(message.spreadsheets);

            //Set the callback to be our processMessage method.
            ss.callMe = ProcessMessage;
            Network.ConfigureCallBack(ss);
        }

        internal void CanUpdate()
        {
            canUpdate = true;
        }


        /// <summary>
        /// This method processes server messages, line by line, from a supplied SocketState's string buffer.
        /// </summary>
        /// <param name="ss"></param>
        private void ProcessMessage(SocketState ss)
        {
            string totalData = ss.sb.ToString();
            string[] parts = Regex.Split(totalData, @"(?<=[\n\n])");


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

            // Loop until we have processed all messages.
            lock (_lock)
            {
                foreach (string p in parts)
                {
                    // Ignore empty strings added by the regex splitter and single endline characters from the split
                    if (p.Length == 0)
                    {
                        ss.sb.Remove(0, p.Length);
                        continue;
                    }
                    if (p == "\n")
                    {
                        try
                        {
                            ss.sb.Remove(0, p.Length);
                        } catch (Exception) { }
                        continue;
                    }

                    // The regex splitter will include the last string even if it doesn't end with a '\n',
                    if (p[p.Length - 1] != '\n')
                        break;

                    JsonSerializerSettings settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                    Message message = new Message();
                    try
                    {
                        message = (Message)JsonConvert.DeserializeObject(p.ToString(), typeof(Message), settings);
                    }
                    catch (Exception) { }

                    switch (message.type)
                    {
                        case "error":
                            if (message.code == 1)
                                Console.WriteLine("Invalid Authorization - Incorrect Username or Password");
                            else
                                Console.WriteLine("Circular Dependency Detected");
                            break;
                        case "full send":
                            lock (_lock)
                            {
                                foreach (string cellName in message.spreadsheet.Keys)
                                {
                                    ssModule.SetContentsOfCell(cellName, message.spreadsheet[cellName]);
                                    char i0 = cellName[0];
                                    int col = char.ToUpper(i0) - 65;
                                    int row = int.Parse(cellName.Substring(1, cellName.Length - 1)) - 1;
                                    MyForm.Invoke(new MethodInvoker(delegate { HandleUpdate(col, row, message.spreadsheet[cellName]); }));
                                }
                            }
                            break;
                        default:
                            break;
                    }

                    // Then remove it from the SocketState's growable buffer
                    try
                    {
                        ss.sb.Remove(0, p.Length);
                    }
                    catch(ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("Oops! Something went wrong with the network. Try again!");
                    }
                }
                DrawFromFile();
            }
            try
            {
                MyForm.Invoke(new MethodInvoker(() => MyForm.Invalidate(true)));
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
        public bool StartConnection(string address)
        {
            //  Connect to Socket
            try
            {
                //  Give network FirstContact so we can get our variables before reading more msgs
                Network.ConnectToServer(address, FirstContact, ProcessMessage);
            }
            //  If failed, let View know
            catch (Exception)
            {
                return false;
            }
            //  If no exceptions, no problem!
            return true;
        }

        public void SetSpreadsheetNames(List<string> names)
        {
            spreadsheetNames = new List<string>();
            foreach (string sheet in names)
            {
                spreadsheetNames.Add(sheet);
            }
        }

        public IEnumerable<string> GetSpreadsheetNames()
        {
            //spreadsheetNames = new List<string> { "a", "b", "c" };      //  TODO: Delete this line
            foreach (string name in spreadsheetNames)
            {
                yield return name;
            }
        }


        /// <summary>
        /// Sends server request Json messages based around a supplied enum key.
        /// </summary>
        /// <param name="keyword"></param>
        public void SendJson(MessageKey key, int col = 0, int row = 0, string sheet = null)
        {
            switch (key)
            {
                case MessageKey.Edit:
                    {
                        Message message = new Message();
                        string jsonMessage;
                        string cellName = window.GetName(col, row);
                        var cellValue = sheet;
                        if (cellValue.GetType() == typeof(Formula))
                            cellValue = "=" + cellValue.ToString();
                        List<string> dependees = new List<string>();
                        foreach (string dependee in ssModule.dg.GetDependees(cellName))
                        {
                            dependees.Add(dependee);
                        }
                        message.type = "edit";
                        message.value = cellValue;
                        message.cell = cellName;
                        message.dependencies = dependees;

                        //Format jsonMessage to ignore null properties.
                        jsonMessage = JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });

                        jsonMessage = jsonMessage + "\n\n";
                        Network.Send(socket, jsonMessage);
                        break;
                    }
                case MessageKey.Open:
                    {
                        Message message = new Message();
                        message.type = "open";
                        message.name = sheet;
                        message.username = username;
                        message.password = password;

                        string jsonMessage = JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });

                        jsonMessage = jsonMessage + "\n\n";
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

                        jsonMessage = jsonMessage + "\n\n";
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

                        jsonMessage = jsonMessage + "\n\n";
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


