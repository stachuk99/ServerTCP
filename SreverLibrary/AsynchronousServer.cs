using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary
{
    /// <summary>
    /// Provides Base64 endoding messages to many clients
    /// </summary>
    public class AsynchronousServerBase64 : AbstractServerBase64
    {
        #region Fields
        NetworkStream stream;
        #endregion
        #region Constructors
        /// <summary>
        /// Deafault constructor
        /// </summary>
        /// <param name="ip">Ip address</param>
        /// <param name="port">tcp/ip port number</param>
        public AsynchronousServerBase64(IPAddress ip, int port) : base(ip, port)
        {

        }
        #endregion
        #region Functions
        public delegate void DataTransmissionDelegate(NetworkStream stream);
        /// <summary>
        /// Waits for connections any makes a new delegate for each client
        /// </summary>
        protected override void AcceptConnetion()
        {
            while (true)
            {
                TcpClient client = Listener.AcceptTcpClient();
                stream = client.GetStream();
                DataTransmissionDelegate transmissionDelegate = new DataTransmissionDelegate(BeginDataTransmission);
                transmissionDelegate.BeginInvoke(stream, TransmissionCallback, client);
            }
        }
        private void TransmissionCallback(IAsyncResult ar)
        {
            Console.WriteLine(ar.AsyncState.ToString());
            Console.WriteLine("Posprzątane");
        }
        /// <summary>
        /// Implements data transminion between clients and server
        /// </summary>
        /// <param name="stream">Transmission stream</param>
        protected override void BeginDataTransmission(NetworkStream stream)
        {
            byte[] buffer = new byte[BufferSize];
            while (true)
            {
                try
                {
                    int len = stream.Read(buffer, 0, BufferSize);
                    if (buffer[0] != 13 && buffer[1] != 10)
                    {
                        Console.WriteLine(len + " " + Encoding.Default.GetString(buffer));
                        char[] encodedBuffer = new char[BufferSize * 2];
                        _ = Convert.ToBase64CharArray(buffer, 0, len, encodedBuffer, 0);
                        stream.Write(Encoding.UTF8.GetBytes(encodedBuffer), 0, encodedBuffer.Length);
                        Console.WriteLine("Wiadomość w formacie Base64: " +
                            Encoding.Default.GetString(Encoding.UTF8.GetBytes(encodedBuffer)));
                    }
                }
                catch (IOException e)
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Runs the server
        /// </summary>
        public override void Start()
        {
            StartListening();
            AcceptConnetion();
        }
        #endregion
    }
}
