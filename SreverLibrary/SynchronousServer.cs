using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary
{
    /// <summary>
    /// Provides Base64 endoding messages to one client
    /// </summary>
    public class SynchronousServerBase64 : AbstractServerBase64
    {
        #region Fields
        TcpClient client;
        NetworkStream stream;
        #endregion
        #region Constructors
        /// <summary>
        /// Deafault constructor
        /// </summary>
        /// <param name="ip">Ip address</param>
        /// <param name="port">tcp/ip port number</param>
        public SynchronousServerBase64(IPAddress ip, int port): base(ip, port)
        {

        }
        #endregion
        #region Functions
        /// <summary>
        /// Waits for client connection
        /// </summary>
        protected override void AcceptConnetion()
        {
            TcpListener l = Listener;
            client = l.AcceptTcpClient();
            stream = client.GetStream();
            BeginDataTransmission(stream);
        }
        /// <summary>
        /// Implements data transminion between client and server
        /// </summary>
        /// <param name="stream">Transminsion stream</param>
        protected override void BeginDataTransmission(NetworkStream stream)
        {
            byte[] buffer = new byte[BufferSize];
            while (true)
            {
                int len = stream.Read(buffer, 0, BufferSize);
                if (buffer[0] != 13 && buffer[1] != 10)
                {
                    Console.WriteLine(len + " " + Encoding.Default.GetString(buffer).Trim());
                    char[] encodedBuffer = new char[BufferSize * 2];
                    _ = Convert.ToBase64CharArray(buffer, 0, len, encodedBuffer, 0);
                    stream.Write(Encoding.UTF8.GetBytes(encodedBuffer), 0, encodedBuffer.Length);
                    Console.WriteLine("Wiadomość w formacie Base64: " +
                        Encoding.Default.GetString(Encoding.UTF8.GetBytes(encodedBuffer)));
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
