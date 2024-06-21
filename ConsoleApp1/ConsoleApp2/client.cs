using System.Net.Sockets;
using System.Net;
using System.Text;

Console.WriteLine("Client");
//IPHostEntry iphostinfo = await Dns.GetHostEntryAsync("google.com");
IPAddress ipaddress = IPAddress.Any;
Console.WriteLine(ipaddress);
IPEndPoint ipEndPoint = new(ipaddress,11000);

using Socket client = new(
    ipEndPoint.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp);

await client.ConnectAsync(ipEndPoint);
//while (true)
//{
    var buffer = new byte[1024];
    var received = await client.ReceiveAsync(buffer, SocketFlags.None);
    var response = Encoding.UTF8.GetString(buffer, 0, received);
    Console.WriteLine(response);

 //}
client.Shutdown(SocketShutdown.Both);
