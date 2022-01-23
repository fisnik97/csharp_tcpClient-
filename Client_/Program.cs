using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client_
{
   class Program
   {
      static Client client;
      static void Main(string[] args)
      {
         client = new Client("127.0.0.1", 81, OnMessageReceive);
         Console.WriteLine("Write a message to server: ");
         Task.Run(client.CheckForMessages);

         while (true)
         {
            var message = Console.ReadLine();
            if (message.ToLower() == "exit")
               break;
            if (!string.IsNullOrEmpty(message))
            {
               client.SendMessage(message);
            }
         }
         Console.Read();
      }

      static void OnMessageReceive(string message)
      {
         Console.ForegroundColor = ConsoleColor.DarkMagenta;
         Console.WriteLine($"Server: {message}");
         Console.ForegroundColor = ConsoleColor.DarkRed;
      }

   }
}

