using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UdemyUDPBroadcastSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sockBroadcaster = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sockBroadcaster.EnableBroadcast = true;

            IPEndPoint BroadcastEP = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 23000);

            byte[] buff = new byte[512];
            try
            {
                while(true)
                {
                    Console.Write("Input broadcast message: ");
                    string sendString = Console.ReadLine();
                    if (sendString.Length > 0)
                    {
                        buff = Encoding.ASCII.GetBytes(sendString);
                        sockBroadcaster.SendTo(buff, BroadcastEP);
                        if (sendString == "<exit>")
                        {
                            break;
                        }
                        else
                        {
                            Array.Clear(buff, 0, buff.Length);
                        }
                    }
                }
                sockBroadcaster.Shutdown(SocketShutdown.Both);
                sockBroadcaster.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
