using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using log4net.Config;
using Nancy.Hosting.Self;

namespace KettleToolsCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = GetFreePort();
            var url = "http://localhost:" + port;
            BasicConfigurator.Configure();
            using (var host = new NancyHost(new Uri(url)))
            {
                host.Start();
                Process.Start(url);
                Console.WriteLine("Press ENTER key to stop the program...");
                Console.ReadLine();
            }
        }

        static int GetFreePort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }
    }
}
