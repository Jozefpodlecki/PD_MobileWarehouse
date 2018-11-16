using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace TcpServer
{
    class Program
    {
        static IEnumerable<NetworkInterface> GetAvailableNetworkInterfaces()
        {
            foreach(var nic in NetworkInterface
                .GetAllNetworkInterfaces())
            {
                if(nic.OperationalStatus != OperationalStatus.Up)
                {
                    continue;
                }

                if(nic.NetworkInterfaceType == NetworkInterfaceType.Loopback || 
                    nic.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
                {
                    continue;
                }

                if ((nic.Description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0) ||
                (nic.Name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0))
                    continue;

                if (nic.Description.Equals("Microsoft Loopback Adapter", StringComparison.OrdinalIgnoreCase))
                    continue;

                yield return nic;
            }
        }

        static void Main(string[] args)
        {
            var nic = GetAvailableNetworkInterfaces()
                .FirstOrDefault();

            var address = nic.GetIPProperties()
                .UnicastAddresses
                .FirstOrDefault(adr => adr.Address.AddressFamily == AddressFamily.InterNetwork)
                .Address;

            var port = 44324;

            var endPoint = new IPEndPoint(address, port);
            var tcpServer = new TcpListener(endPoint);

            //var thread = new Thread()

            //tcpServer.AcceptSocketAsync
        }
    }
}
