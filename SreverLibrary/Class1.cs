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
    /// Odsyła komunikaty zakodowane w Base64
    /// </summary>
    public class ServerBase64
    {
        const int BUFFER_SIZE = 1024;
        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 2048);

        public void Start()
        {
            server.Start();
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Połączono z klientem");
            NetworkStream stream = client.GetStream();
            while (true)
            {
                byte[] buffer = new byte[BUFFER_SIZE];
                int len = stream.Read(buffer, 0, BUFFER_SIZE);
                if (buffer[0] != 13 && buffer[1] != 10)
                {
                    Console.WriteLine(len + " " + Encoding.Default.GetString(buffer));
                    char[] encodedBuffer = new char[BUFFER_SIZE * 2];
                    _ = Convert.ToBase64CharArray(buffer, 0, len, encodedBuffer, 0);
                    stream.Write(Encoding.UTF8.GetBytes(encodedBuffer), 0, encodedBuffer.Length);
                    Console.WriteLine("Wiadomość w formacie Base64: " +
                        Encoding.Default.GetString(Encoding.UTF8.GetBytes(encodedBuffer)));
                }
            }
        }

    }
}
