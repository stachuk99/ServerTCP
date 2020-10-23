using ServerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Tworzy obiekt servera i go uruchamia
/// </summary>
namespace ServerTCP
{
    class Program
    {
        static void Main(string[] args)
        {
            SynchronousServerBase64 server = new SynchronousServerBase64(IPAddress.Parse("127.0.0.1"), 30000);
            server.Start();
        }
    }
}
