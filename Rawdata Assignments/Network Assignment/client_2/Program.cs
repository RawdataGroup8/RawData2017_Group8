﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            

            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 5000);
                Console.WriteLine("[You are connected to the server...]");

                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());


                String Massagefromclient = "";
               


                while (Massagefromclient != "exit" )
                {
                    Massagefromclient = Console.ReadLine();
                    Console.WriteLine("This is a client.." + Massagefromclient);

                    writer.WriteLine("This is a server..." + Massagefromclient);
                    writer.Flush();
                    Console.WriteLine("-------------- sent-----------");
                }

                reader.Close();
                writer.Close();
                client.Close();



            }
            catch (Exception e)
            {
                Console.WriteLine("There is an Exception" + e);
                
            }
            

       


        }
    }
}
