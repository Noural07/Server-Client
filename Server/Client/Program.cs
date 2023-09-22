using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static Socket sck;

        static void Main(string[] args)
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

            try
            {
                sck.Connect(serverEndPoint);
            }
            catch
            {
                Console.Write("Unable to connect to the server!\r\n");
                return;
            }

            // Receive and display product information or error message from the server
            byte[] buffer = new byte[1024];
            int bytesRead = sck.Receive(buffer);
            string productInfo = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine(productInfo);

            Console.Write("Press any key to continue...");
            Console.Read();
            sck.Close();
        }
    }
}