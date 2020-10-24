using ServerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerTCP
{
    /// <summary>
    /// Makes instance of server and runs it 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            AbstractServerBase64 server = new AsynchronousServerBase64(IPAddress.Parse("127.0.0.1"), 30000);
            server.Start();
        }
    }
}
