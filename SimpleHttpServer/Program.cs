using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Threading;

namespace SimpleHttpServer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string server_addr="http://localhost:8080/test/";
            MyServer myServer = new MyServer(server_addr);
        }
    }
}
