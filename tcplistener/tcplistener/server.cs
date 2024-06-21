// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Hello");

//public class TcpListener
//{
   // private TcpListener listener;
   /* public TcpServer()
    {
        StartServer();
    }
    private StartServer()
    {*/
var port = 13;
IPAddress ip = IPAddress.Parse("127.0.0.1");
IPEndPoint end = new(ip, port);
using TcpListener listener = new(end);
listener.Start();
//while (true)
//{
    using TcpClient handler = listener.AcceptTcpClient();
    await using NetworkStream stream = handler.GetStream();
    string msg = "Successfully Connected";
    var msgbytes = Encoding.UTF8.GetBytes(msg);
    await stream.WriteAsync(msgbytes);


   // if()
//}
listener.Stop();
       

    

