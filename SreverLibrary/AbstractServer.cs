using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace ServerLibrary
{
    /// <summary>
    /// Abstract class for Base64 servers
    /// </summary>
    public abstract class AbstractServerBase64
    {
        #region Fields
        int bufferSize = 1024;
        bool running;
        TcpListener listener;
        #endregion
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ip">IP addres of the server</param>
        /// <param name="port">Port numer of the server</param>
        public AbstractServerBase64(IPAddress ip, int port)
        {
            running = false;
            if (port < 49151 && port > 1024)
            {
                listener = new TcpListener(ip, port);
            }
            else throw new Exception("nieprawidłowy numer portu");
        }
        #endregion
        #region properties
        /// <summary>
        /// Property allows to get and modify buffer size, cant be chandeg when server is running or buffer is too big.
        /// </summary>
        public int BufferSize { get => bufferSize; set
            {
                if (value < 0 || value > 1024 * 1024) throw new Exception("nieprawidłowy rozmiar bufora");
                if (!running) bufferSize = value; 
                else throw new Exception("Nie można zmienić rozmiaru bufora w trakcie działania servera");
            } }
        protected TcpListener Listener { get => listener; set
            {
                if (!running) listener = value;
                else throw new Exception("Nie można zmienić listenera w trakcie działania servera");
            }
        }
        #endregion
        #region Functions
        /// <summary>
        /// Starts the TCPListener.
        /// </summary>
        protected void StartListening()
        {
            running = true;
            listener.Start();
        }
        /// <summary>
        /// Waits and accepts clients connection.
        /// </summary>
        protected abstract void AcceptConnetion();
        /// <summary>
        /// Implements data transminion between clien and server
        /// </summary>
        /// <param name="stream">Transminsion stream</param>
        protected abstract void BeginDataTransmission(NetworkStream stream);
        /// <summary>
        /// Makes server doing his job
        /// </summary>
        public abstract void Start();
        #endregion

    }
}
