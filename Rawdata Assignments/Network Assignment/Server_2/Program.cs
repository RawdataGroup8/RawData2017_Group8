using System;
using System.IO;
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
            int numberofconnections = 1;

            try
            {
                
                var server = new TcpListener(localAddr, port);

                server.Start();
                

                Console.WriteLine("Server Started");

                while (true)
                {
                    Console.WriteLine("Waiting for a client to connect...");

                    var client = server.AcceptTcpClient();

                    Console.WriteLine("A new Client is connected----" + numberofconnections++);

                    var thread = new Thread(HandleClient);

                    thread.Start(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
           

            
        }
        static void HandleClient(object clientObj)
        {
            var client = (TcpClient) clientObj;

            if (client == null) return;

            try
            {
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());

                string massagefromclient = "";
                

                 //massagefromclient = reader.ReadLine();


                
                while(massagefromclient != null && massagefromclient.ToLower() != "exite")
                {
                    massagefromclient = reader.ReadLine();

                    Console.WriteLine("MasageFromClient" + massagefromclient);
                    writer.WriteLine("massage from server Hi ..." + massagefromclient);
                    writer.Flush();
                }

                reader.Close();
                writer.Close();
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Problem with Client"+ e);

                
            }

           



        }
    }
}
