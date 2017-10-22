using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Bai3
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress[] ippool = new IPAddress[5];
            int r;
            Random rd = new Random();
            for (int i = 0; i < ippool.Length; i++)
            {
                ippool[i] = IPAddress.Parse("192.168.1." + (i + 10).ToString());
            }
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 9050);
            UdpClient udps = new UdpClient(ip);

            Console.WriteLine("Waiting for a client...");

            Byte[] data = new Byte[1024];
            data = udps.Receive(ref ip);
            if (Encoding.ASCII.GetString(data) == "req")
            {
                r = rd.Next(0, ippool.Length);
                udps.Send(Encoding.ASCII.GetBytes(ippool[r].ToString()), ippool[r].ToString().Length, ip);
                Console.WriteLine("Da gui IP cho client");
            }
            data = new Byte[1024];
            data = udps.Receive(ref ip);
            if (Encoding.ASCII.GetString(data) == "ack")
            {
                udps.Send(Encoding.ASCII.GetBytes("fin"), 3, ip);
                Console.WriteLine("Server ket thuc DHCP cho client");
            }
            Console.ReadLine();
        }
    }
}
