using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Silo
{
    public class SiloPortConfigurator
    {
        private const int StartPort = 30000; // Начальный порт диапазона
        private static int CurrentPort = StartPort;

        public static (int GatewayPort, int SiloPort) GetAvailablePorts()
        {
            int gatewayPort = GetNextAvailablePort();
            int siloPort = GetNextAvailablePort();

            return (gatewayPort, siloPort);
        }

        private static int GetNextAvailablePort()
        {
            while (CurrentPort < 65535)
            {
                if (IsPortAvailable(CurrentPort))
                {
                    return CurrentPort++;
                }
                else
                {
                    CurrentPort++;
                }
            }

            throw new InvalidOperationException("No available ports found.");
        }

        private static bool IsPortAvailable(int port)
        {
            using (var tcpListener = new TcpListener(IPAddress.Loopback, port))
            {
                try
                {
                    tcpListener.Start();
                    tcpListener.Stop();
                    return true;
                }
                catch (SocketException)
                {
                    return false;
                }
            }
        }
    }
}
