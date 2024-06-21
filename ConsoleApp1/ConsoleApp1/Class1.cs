using System.Net;
using System.Net.Sockets;
using System.Text;


Console.WriteLine("Server");
//IPHostEntry iphostinfo = await Dns.GetHostEntryAsync("google.com");
IPAddress ipaddress = IPAddress.Any;
Console.WriteLine(ipaddress);
IPEndPoint ipendpoint = new(ipaddress,11000);
//Server Socket
using Socket server = new(ipendpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
server.Bind(ipendpoint);
server.Listen(10);
var handler = await server.AcceptAsync();
while (true)
{
    var msg = "Success";
    var msgbytes = Encoding.UTF8.GetBytes(msg);
    await handler.SendAsync(msgbytes, 0);
    break;
}
   

