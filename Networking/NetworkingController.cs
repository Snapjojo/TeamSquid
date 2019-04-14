using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkingController
{
    /// <summary>
    /// This delegate is invoked when network activity has occurred. 
    /// </summary>
    /// <param name="ss"></param>
    public delegate void NetworkAction(SocketState ss);

    /// <summary>
    /// Delegate invoked when a connection is terminated by partner
    /// </summary>
    /// <param name="ss"></param>
    public delegate void SocketClosed(int ID);

    /// <summary>
    /// Modular class for managing sockets, connections, and sending/receiving information
    /// </summary>
    public static class Network
    {
        private const int port = 0; //TODO ??

        /// <summary>
        /// Start attempting to connect to a server
        /// </summary>
        /// <param name="ip">The address of the server</param>
        public static void ConnectToServer(string hostName, string username, string password, NetworkAction _call)
        {
            System.Diagnostics.Debug.WriteLine("connecting  to " + hostName);

            // Create a TCP/IP socket, then add it to a new SocketState
            Socket socket;
            IPAddress ipAddress;
            MakeSocket(hostName, out socket, out ipAddress);
            SocketState ss = new SocketState(socket);

            //  Save the ProcessMessage method
            ss.callMe = _call;

            //  Start up the socket
            socket.BeginConnect(ipAddress, port, ConnectedCallback, ss);

            //  Follow through with FirstContact, which will start a continuous loop
            _call(ss);
        }

        /// <summary>
        /// Creates a Socket object for the given host string
        /// </summary>
        /// <param name="hostName">The host name or IP address</param>
        /// <param name="socket">The created Socket</param>
        /// <param name="ipAddress">The created IPAddress</param>
        public static void MakeSocket(string hostName, out Socket socket, out IPAddress ipAddress)
        {
            ipAddress = IPAddress.None;
            socket = null;
            try
            {
                // Establish the remote endpoint for the socket.
                IPHostEntry ipHostInfo;

                // Determine if the server address is a URL or an IP
                try
                {
                    ipHostInfo = Dns.GetHostEntry(hostName);
                    bool foundIPV4 = false;
                    foreach (IPAddress addr in ipHostInfo.AddressList)
                        if (addr.AddressFamily != AddressFamily.InterNetworkV6)
                        {
                            foundIPV4 = true;
                            ipAddress = addr;
                            break;
                        }
                    // Didn't find any IPV4 addresses
                    if (!foundIPV4)
                    {
                        System.Diagnostics.Debug.WriteLine("Invalid addres: " + hostName);
                        throw new ArgumentException("Invalid address");
                    }
                }
                catch (Exception)
                {
                    // see if host name is actually an ipaddress, i.e., 155.99.123.456
                    System.Diagnostics.Debug.WriteLine("using IP");
                    ipAddress = IPAddress.Parse(hostName);
                }

                // Create a TCP/IP socket.
                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);

                // Disable Nagle's algorithm - can speed things up for tiny messages, 
                // such as for a game
                socket.NoDelay = true;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create socket. Error occured: " + e);
                throw new ArgumentException("Invalid address");
            }
        }

        /// <summary>
        /// Called when the client wants more data, after the completion of callMe (provided by Controller)
        /// </summary>
        /// <param name="ss"></param>
        public static void GetData(SocketState ss)
        {
            ss.theSocket.BeginReceive(ss.messageBuffer, 0, ss.messageBuffer.Length, SocketFlags.None, ReceiveCallback, ss);
        }

        /// <summary>
        /// Sends a message to the server through the socket.
        /// </summary>
        /// <param name="s">The connection socket</param>
        /// <param name="message">The request</param>
        public static void Send(Socket s, string message)
        {
            //  Adds newline onto desired message (per specification), then converts to bytes
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            try
            {
                s.BeginSend(messageBytes, 0, messageBytes.Length, SocketFlags.None, SendCallback, s);
            }
            catch (Exception e)
            {
                s.Disconnect(false);
            }
        }

        /// <summary>
        /// This method is automatically invoked on its own thread when a connection is made.
        /// </summary>
        /// <param name="ar"></param>
        private static void ConnectedCallback(IAsyncResult ar)
        {
            Console.WriteLine("contact from server");

            // Get the SocketState associated with this connection 
            SocketState ss = (SocketState)ar.AsyncState;

            // This is required to complete the "handshake" with the server. Both parties agree a connection is made.
            ss.theSocket.EndConnect(ar);
        }

        /// <summary>
        /// This method is invoked on its own thread when data arrives.
        /// </summary>
        /// <param name="ar"></param>
        private static void ReceiveCallback(IAsyncResult ar)
        {
            int numBytes = 0;

            // Get the SocketState representing the connection on which data was received
            SocketState ss = (SocketState)ar.AsyncState;

            //  Attempt to receive information, although partner may have disconnected.
            try
            {
                numBytes = ss.theSocket.EndReceive(ar);
            }
            catch (SocketException se)
            {
                ss.closed(ss.ID);
            }

            // Convert the raw bytes to a string           
            if (numBytes > 0)
            {
                string message = Encoding.UTF8.GetString(ss.messageBuffer, 0, numBytes);
                //TODO Format string as proper JSON
                ss.sb.Append(message);
                ss.callMe(ss);

                try
                {
                    ss.theSocket.BeginReceive(ss.messageBuffer, 0, ss.messageBuffer.Length,
                        SocketFlags.None, ReceiveCallback, ss);
                }
                catch (SocketException se)
                {
                    ss.closed(ss.ID);
                }
            }

            // Wait for more data from the server. This creates an "event loop".
            // ReceiveCallback will be invoked every time new data is available on the socket.
            //ss.theSocket.BeginReceive(ss.messageBuffer, 0, ss.messageBuffer.Length,
            //  SocketFlags.None, ReceiveCallback, ss);

        }

        /// <summary>
        /// A callback invoked when a send operation completes
        /// Move this function to a standalone networking library. 
        /// </summary>
        /// <param name="ar"></param>
        private static void SendCallback(IAsyncResult ar)
        {
            Socket s = (Socket)ar.AsyncState;
            // Nothing much to do here, just conclude the send operation so the socket is happy.
            try
            {
                s.EndSend(ar);
            }
            catch (SocketException se)
            {

            }
        }

    }

    /// <summary>
    /// A SocketState represents all of the information needed
    /// to handle one connection.
    /// </summary>
    public class SocketState
    {
        public Socket theSocket;
        public byte[] messageBuffer { get; }
        public StringBuilder sb { get; }

        public NetworkAction callMe;

        public SocketClosed closed;

        public int ID { get; set; }

        public SocketState(Socket s)
        {
            theSocket = s;
            messageBuffer = new byte[4096];
            sb = new StringBuilder();
            ID = -5;    //  Random id for client-end, server will update immediately
        }
    }

    public class ConnectionState
    {
        public TcpListener Listener { get; }
        public NetworkAction callMe { get; }

        public ConnectionState(TcpListener listen, NetworkAction call)
        {
            this.Listener = listen;
            this.callMe = call;
        }
    }
}
