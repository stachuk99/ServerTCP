using ServerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ServerBase64 server = new ServerBase64();
            server.Start();
        }
    }
}
