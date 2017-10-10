﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace EchoServer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Int32 port = 5000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            var server = new TcpListener(localAddr, port);

            server.Start();

            Console.WriteLine("Started");

            while(true)
            {
                var client = server.AcceptTcpClient();

                Console.WriteLine("Client connected");

                var thread = new Thread(HandleClient);

                thread.Start(client);
            }
        }

        static void HandleClient(object clientObj)
        {
            var client = clientObj as TcpClient;
            if (client == null) return;

            var strm = client.GetStream();

            byte[] buffer = new byte[client.ReceiveBufferSize];
            strm.Read(buffer, 0, buffer.Length);

            var request = Encoding.UTF8.GetString(buffer);

            Console.WriteLine(request);



        }
    }
}
