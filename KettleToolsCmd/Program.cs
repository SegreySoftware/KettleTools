//
// Segrey Software licenses this file to you under the Apache License, Version 2.0
// (the "License"); you may not use this file except in compliance with 
// the License. You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
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
