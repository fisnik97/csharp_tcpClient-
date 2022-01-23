using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using static Client_.Delegates;

namespace Client_
{
   public class Delegates
   {
      public delegate void OnMessageReceive(string message);
      public delegate void ClientDisconnected();
   }

   public class Client
   {
      private readonly TcpClient _tcpClient;
      private readonly Action<string> OnMessageReceive;

      public Client(string ipAddress, int port, Action<string> onMessageReceive)
      {
         _tcpClient = new TcpClient(ipAddress, port);
         OnMessageReceive = onMessageReceive;
      }

      public void SendMessage(string message)
      {
         var byteData = Encoding.ASCII.GetBytes(message);
         var stream = _tcpClient.GetStream();
         stream.Write(byteData, 0, byteData.Length);
         Console.ForegroundColor = ConsoleColor.DarkGreen;
         Console.WriteLine($"Me: {message}");
         Console.ForegroundColor = ConsoleColor.DarkRed ;
      }

      public void CheckForMessages()
      {
         try
         {
            var bytes = new Byte[256];
            var stream = this._tcpClient.GetStream();
            int i;
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
               var data = Encoding.ASCII.GetString(bytes, 0, i);
               OnMessageReceive?.Invoke(data);
            }
         }
         catch (Exception)
         {
            
         }
      }

   }


}
