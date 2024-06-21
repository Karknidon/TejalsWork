using System.IO;
using System ;
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Client");

var port = 13;
IPAddress ip = IPAddress.Parse("127.0.0.1");
IPEndPoint end = new(ip, port);

using TcpClient client = new();
await client.ConnectAsync(end);
var stream = client.GetStream();
int readtot;
string msg;
byte[] buffer = new byte[1024];
while ((readtot = stream.Read(buffer, 0, buffer.Length)) > 0)
{
    msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
    Console.WriteLine(msg);
}
client.Close();




