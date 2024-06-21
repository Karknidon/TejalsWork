using System.ComponentModel.Design;
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Client");

var port = 11;     //2321;
IPAddress ip = IPAddress.Parse("127.0.0.1");        //("192.168.99.208");
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
        /* Thread inp = new Thread(() => Input(done, stream, buffer));
         inp.Start();
         inp.Join();*/
        var stopbytes = Encoding.UTF8.GetBytes(done);
        stream.Write(stopbytes);
        /* while (true && done.Substring(2,1)=="I")
         {
             Console.Write("Input2:");
             string input = Console.ReadLine();
             var inputbytes = Encoding.UTF8.GetBytes(input);
             stream.Write(inputbytes);

             if (input.Substring(0, 4) == "Done") 
             {
                 break;
             }
             else if (input.Contains("HB"))
         {

             Array.Clear(buffer, 0, buffer.Length);
             stream.Read(buffer, 0, buffer.Length);
             msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
             Console.WriteLine(msg);
             break;
         }
             else
             {
                 Array.Clear(buffer, 0, buffer.Length);
                 stream.Read(buffer, 0, buffer.Length);
                 msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                 Console.WriteLine(msg);


             }
         }*/

        //Thread.Sleep(20);

        if (done.Substring(0, 4) == "Done")
        {
            break;
        }
        else
        {
            Array.Clear(buffer, 0, buffer.Length);
            stream.Read(buffer, 0, buffer.Length);
            msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            Console.WriteLine(msg);

        }
    }

    static void Input(string done, NetworkStream stream, byte[] buffer)
    {
        var stopbytes = Encoding.UTF8.GetBytes(done);
        stream.Write(stopbytes);
        if (done.Substring(0, 4) == "Done")
        {
            return;
        }
        else
        {
            Array.Clear(buffer, 0, buffer.Length);
            stream.Read(buffer, 0, buffer.Length);
            string msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
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
