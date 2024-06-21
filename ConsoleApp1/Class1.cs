using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Hello, World!");
IPHostEntry iphostinfo = await Dns.GetHostEntryAsync("google.com");
IPAddress ipaddress = iphostinfo.AddressList[0];
Console.WriteLine(ipaddress);
IPEndPoint ipendpoint = new(ipaddress, 11_000);

//Server Socket
using Socket server = new(ipendpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
server.Bind(ipendpoint);
server.Listen(100);
var handler = await server.AcceptAsync();
var msg = "Success";
var msgbytes = Encoding.UTF8.GetBytes(msg);
await handler.SendAsync(msgbytes, 0);
