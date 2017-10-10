using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace EchoServer
{
    internal class EchoServ
    {
        private static void Main(string[] args)
        {
            const int port = 5000;
            var localAddr = IPAddress.Parse("127.0.0.1");

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

        private static void HandleClient(object clientObj)
        {
            if (!(clientObj is TcpClient client)) return;

            var stream = client.GetStream();

            var buffer = new byte[client.ReceiveBufferSize];
            var bytesRead = stream.Read(buffer, 0, buffer.Length);
            var response = new Response();

            var request = Encoding.UTF8.GetString(buffer);
            //Console.WriteLine("buffer: "+bytesRead+"Req: |"+request.Trim('\0') + "|");
            //if (request.Trim('\0') == "{}"){ response.Status = "missing method";}
            //else
            //{
            //Console.ReadKey();
                var requestObj = JsonConvert.DeserializeObject<RequestObj>(request.Trim('\0'));
                switch (requestObj.Method)
                {
                    case "{}":
                        response.Status = "missing method";
                        break;
                    default:
                        response.Status = "illegal method";
                        break;

                }
                Console.WriteLine("The method: "+requestObj.Method);
            //}
            //Console.WriteLine(response.Status + " | " + response.Body);
            //var reqObjFromJson = JsonConvert.DeserializeObject(request);
            //string response;


            //Console.WriteLine(JsonConvert.SerializeObject(response).Length);

            var jsonResponse = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
            //Console.ReadKey();
            stream.Write(jsonResponse, 0, jsonResponse.Length);

            stream.Close();

            client.Dispose();
        }

        public class RequestObj
        {
            public string Method, Path, Date, Body;

            public RequestObj(string method, string path, string date, string body)
            {
                Method = method;
                Path = path;
                Date = date;
                Body = body;
            }

            public RequestObj()
            {
                Method = "{}";
            }
        }

        public class Response
        {
            public string Status { get; set; }
            public string Body { get; set; }
        }
    }
}
