using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Client");

var port = 11;
IPAddress ip = IPAddress.Parse("127.0.0.1");
IPEndPoint end = new(ip, port);

using TcpClient client = new();
client.Connect(end);
var stream = client.GetStream();
int readtot;
string msg;
byte[] buffer = new byte[1024];

while ((readtot = stream.Read(buffer, 0, buffer.Length)) > 0)
{
    msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
    Console.WriteLine(msg);
    while (true)
    {
        Console.Write("Input:");
        string done = Console.ReadLine();
        var stopbytes = Encoding.UTF8.GetBytes(done);
        stream.Write(stopbytes);
        //Thread.Sleep(20);
        if (done.Substring(0, 4) == "Done")
        {
            break;
        }
        else
        {
            stream.Read(buffer, 0, buffer.Length);
            msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            Console.WriteLine(msg);

        }
    }
}
/* else
 {
     string Data2Send = "HB";
     string comm = "#," + Data2Send + ",?";
     var commbytes = Encoding.UTF8.GetBytes(comm);
     stream.Write(commbytes);
     break;

 }*/

//Thread.Sleep(100000);
//while (true)
//{


//client.Shutdown(SocketShutdown.Both);
client.Close();
