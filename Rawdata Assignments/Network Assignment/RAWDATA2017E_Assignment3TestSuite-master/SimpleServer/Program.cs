using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SimpleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the TcpListener on port 5000.
            Int32 port = 5000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            // TcpListener server = new TcpListener(port);
            var server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();
            Console.WriteLine("Started");

            while (true)
            {
                var client = server.AcceptTcpClient();

                Console.WriteLine("Client Connected");

                var thread = new Thread(HandleClient);
                thread.Start(client);
                
            }


        }

        static void HandleClient(object clientObj)
        {
            if (!(clientObj is TcpClient client)) return;
            var stream = client.GetStream();

            byte[] buffer = new byte[client.ReceiveBufferSize];
            stream.Read(buffer,0,buffer.Length);

            var request = Encoding.UTF8.GetString(buffer);
            Console.WriteLine(request);

            //var response = Encoding.UTF8.GetString(request);
            //Console.WriteLine(response);
        }
    }
}
