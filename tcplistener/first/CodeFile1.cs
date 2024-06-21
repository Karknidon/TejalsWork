using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

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



// See https://aka.ms/new-console-template for more information
using System.IO;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace Machine
{
    class Program
    {
        public static byte[] sensor1 = new byte[8];
        public static byte[] sensor2 = new byte[8];
        public static int encI;
        public static int encS;
        public static int encD;
        public static byte dwellTime;
        public static byte totalTime;
        public static byte errorCode;



        public static void Main(string[] args)
        {


            //Connection
            Console.WriteLine("Server");
            var port = 11;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint end = new(ip, port);
            using TcpListener listener = new(end);

            while (true)
            {

                listener.Start();
                using TcpClient handler = listener.AcceptTcpClient();
                using NetworkStream stream = handler.GetStream();
                byte[] buffer = new byte[1024];
                string msg = "Successfully Connected";
                var msgbytes = Encoding.UTF8.GetBytes(msg);
                stream.Write(msgbytes);

                while (true)
                {
                    stream.Read(buffer, 0, buffer.Length);
                    string done = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    if (done.Substring(0, 4) == "Done")
                    {

                        Console.WriteLine("Connection terminated...");
                        listener.Stop();
                        break;
                    }

                    else
                    {
                        Thread c = new Thread(() => Emulator(stream, buffer));
                        c.Start();
                        c.Join();
                    }
                }
                Thread.Sleep(20);

            }
        }
        static void Emulator(NetworkStream stream, byte[] buffer)
        {
            string input = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            Console.WriteLine(input); // Debug command
            int size = input.Length;
            // Console.WriteLine(size);
            string str = @"#,+,?";
            Regex re = new Regex(str);
            if (re.IsMatch(input))
            {
                if (input.Contains("HB"))
                {
                    Thread hb = new Thread(() => HB(stream, buffer, input));
                    hb.Start();
                    hb.Join();
                }
                else if (input.Contains("EI"))
                {
                    Thread ei = new Thread(() => EI(stream, buffer, input));
                    ei.Start();
                    ei.Join();
                }
                else if (input.Contains("I"))
                {
                    Thread i = new Thread(() => I(stream, buffer, input));
                    i.Start();
                    i.Join();
                }
                else if (input.Contains("ES"))
                {
                    Thread es = new Thread(() => ES(stream, buffer, input));
                    es.Start();
                    es.Join();

                }
                else if (input.Contains("ED"))
                {
                    Thread ed = new Thread(() => ED(stream, buffer, input));
                    ed.Start();
                    ed.Join();
                }


            }
        }

        public static void HB(NetworkStream stream, byte[] buffer, string input)
        {
            if (sensor2[6] != 1 && input.Contains("HB") || sensor2[6] == 1 && input.Contains("I"))
            {
                for (int i = 0; i < sensor1.Length; i++)
                {
                    sensor1[i] = 0;
                }
                for (int i = 0; i < sensor2.Length; i++)
                {
                    sensor2[i] = 0;
                }
                encI = 0;
                encS = 0;
                encD = 0;
                dwellTime = 0;
                totalTime = 0;
                errorCode = 0;
                string Data2Send = "#," + string.Join("", sensor1) + "," + string.Join("", sensor2) + "," + Convert.ToString(encI) + "," + Convert.ToString(encS) + "," + Convert.ToString(encD) + "," + Convert.ToString(dwellTime) + "," + Convert.ToString(totalTime) + "," + Convert.ToString(errorCode);
                var Data2Sendbytes = Encoding.UTF8.GetBytes(Data2Send);
                stream.Write(Data2Sendbytes);
            }
            else
            {
                string invalid = "Invalid command";
                var invalidbytes = Encoding.UTF8.GetBytes(invalid);
                stream.Write(invalidbytes);

                Thread.Sleep(100);
                return;
            }

        }

        public static void EI(NetworkStream stream, byte[] buffer, string input)
        {
            if (sensor2[6] != 1 && input.Contains("EI"))
            {
                int w = 1;
                int t = 0;
                sensor2[7] = 1; //moving
                sensor2[6] = 1; //command in progress
                encI = t * w;
                sensor2[6] = 0;
                sensor2[7] = 0;
                sensor2[4] = 1; //indexer calibrated
                sensor1[3] = 1; //indexer home
                string sens1 = string.Join("", sensor1);
                string sens2 = string.Join("", sensor2);
                string Data2Send = "#" + "," + sens1 + "," + sens2 + "," + Convert.ToString(encI) + "," + Convert.ToString(encS) + "," + Convert.ToString(encD) + "," + Convert.ToString(dwellTime) + "," + Convert.ToString(totalTime) + "," + Convert.ToString(errorCode);
                var Data2Sendbytes = Encoding.UTF8.GetBytes(Data2Send);
                stream.Write(Data2Sendbytes);
                encI = 0;
            }
            else
            {
                string invalid = "Invalid command";
                var invalidbytes = Encoding.UTF8.GetBytes(invalid);
                stream.Write(invalidbytes);
                return;
            }
        }
        public static void ES(NetworkStream stream, byte[] buffer, string input)
        {
            if (sensor2[6] != 1 && input.Contains("ES"))
            {
                int t = 0;
                int w = 1;
                sensor2[7] = 1; //moving
                sensor2[6] = 1; //command in progress
                encS = t * w;
                sensor2[6] = 0;
                sensor2[7] = 0;
                sensor2[4] = 1; //indexer calibrated
                sensor1[5] = 1; //SOURCE home
                string sens1 = string.Join("", sensor1);
                string sens2 = string.Join("", sensor2);
                string Data2Send = "#" + "," + sens1 + "," + sens2 + "," + Convert.ToString(encI) + "," + Convert.ToString(encS) + "," + Convert.ToString(encD) + "," + Convert.ToString(dwellTime) + "," + Convert.ToString(totalTime) + "," + Convert.ToString(errorCode);
                var Data2Sendbytes = Encoding.UTF8.GetBytes(Data2Send);
                stream.Write(Data2Sendbytes);
                encS = 0;
            }
            else
            {
                string invalid = "Invalid command";
                var invalidbytes = Encoding.UTF8.GetBytes(invalid);
                stream.Write(invalidbytes);
                return;

            }
        }
        public static void ED(NetworkStream stream, byte[] buffer, string input)
        {
            if (sensor2[6] != 1 && input.Contains("ED"))
            {
                int t = 0;
                int w = 1;
                sensor2[7] = 1; //moving
                sensor2[6] = 1; //command in progress
                encS = t * w;
                sensor2[6] = 0;
                sensor2[7] = 0;
                sensor2[4] = 1; //indexer calibrated
                sensor1[1] = 1; //DUMMY home
                string sens1 = string.Join("", sensor1);
                string sens2 = string.Join("", sensor2);
                string Data2Send = "#" + "," + sens1 + "," + sens2 + "," + Convert.ToString(encI) + "," + Convert.ToString(encS) + "," + Convert.ToString(encD) + "," + Convert.ToString(dwellTime) + "," + Convert.ToString(totalTime) + "," + Convert.ToString(errorCode);
                var Data2Sendbytes = Encoding.UTF8.GetBytes(Data2Send);
                stream.Write(Data2Sendbytes);
                encS = 0;
            }
            else
            {
                string invalid = "Invalid command";
                var invalidbytes = Encoding.UTF8.GetBytes(invalid);
                stream.Write(invalidbytes);
                return;
            }

        }
        public static void I(NetworkStream stream, byte[] buffer, string input)
        {
            if (sensor2[6] != 1 && input.Contains("I"))
            {
                int w = 1;
                int t = 0;
                sensor2[7] = 1; //moving
                sensor2[6] = 1; //command in progress
                int encEnd = 1000;

                while (encI != encEnd)
                {
                    encI = t * w;
                    //var clock = System.Diagnostics.Stopwatch.StartNew();
                    Thread.Sleep(500);
                    //clock.Stop();
                    t += 500;// Convert.ToInt32(clock.ElapsedMilliseconds);
                }
                sensor2[6] = 0;
                sensor2[7] = 0;
                sensor2[4] = 1; //indexer calibrated
                sensor1[3] = 1; //indexer home
                string sens1 = string.Join("", sensor1);
                string sens2 = string.Join("", sensor2);
                string Data2Send = "#" + "," + sens1 + "," + sens2 + "," + Convert.ToString(encI) + "," + Convert.ToString(encS) + "," + Convert.ToString(encD) + "," + Convert.ToString(dwellTime) + "," + Convert.ToString(totalTime) + "," + Convert.ToString(errorCode);
                var Data2Sendbytes = Encoding.UTF8.GetBytes(Data2Send);
                Thread.Sleep(100);
                stream.Write(Data2Sendbytes);
                encI = 0;
            }
            else
            {
                string invalid = "Invalid command";
                var invalidbytes = Encoding.UTF8.GetBytes(invalid);
                stream.Write(invalidbytes);
                return;
            }
        }

    }
}




// public delegate void workCallBack(byte[] buffer);

/*     public static byte[] send(byte[] buffer)
{
          return buffer;
}*/
/*static async void Emulator(TcpClient handler)
{
    // Thread.Sleep(20);
    byte sensor1 = 0;
    byte sensor2 = 0;
    byte encI = 0;
    byte encS = 0;
    byte encD = 0;
    byte dwellTime = 0;
    byte totalTime = 0;
    byte errorCode = 0;

    byte[] buffer = new byte[1024];
    using NetworkStream stream = handler.GetStream();
    string input = Encoding.UTF8.GetString(buffer,0,buffer.Length);
    int size = input.Length;
    string str = @"{#}+{,}+.*[A-Z]+{,}+{?}";
    Regex re = new Regex(str);
    if (re.IsMatch(input))
    {
        string command = input.Substring(2, size - 4);
        if (command == "HB")
        {
            string Data2Send = "#," + Convert.ToString(sensor1) + "," + Convert.ToString(sensor2) + "," + Convert.ToString(encI) + "," + Convert.ToString(encS) + "," + Convert.ToString(encD) + "," + Convert.ToString(dwellTime) + "," + Convert.ToString(totalTime) + "," + Convert.ToString(errorCode);
            var Data2Sendbytes = Encoding.UTF8.GetBytes(Data2Send);
            await stream.WriteAsync(Data2Sendbytes);
            //callback(Data2Sendbytes);
            Thread.Sleep(100);
        }
    }
}*/

/*else 
                    { 
                    byte sensor1 = 0;
                    byte sensor2 = 0;
                    byte encI = 0;
                    byte encS = 0;
                    byte encD = 0;
                    byte dwellTime = 0;
                    byte totalTime = 0;
                    byte errorCode = 0;

                    string input = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    //Console.WriteLine(input); // Debug command
                    int size = input.Length;
                   // Console.WriteLine(size);
                    string str = @"#,+,?";  
                    Regex re = new Regex(str);
                    if (re.IsMatch(input))
                    {
                        string command = input.Substring(2, 2);
                       // Console.WriteLine(command);
                            if (command == "HB")
                            {
                                string Data2Send = "#," + Convert.ToString(sensor1) + "," + Convert.ToString(sensor2) + "," + Convert.ToString(encI) + "," + Convert.ToString(encS) + "," + Convert.ToString(encD) + "," + Convert.ToString(dwellTime) + "," + Convert.ToString(totalTime) + "," + Convert.ToString(errorCode);
                                var Data2Sendbytes = Encoding.UTF8.GetBytes(Data2Send);
                                stream.Write(Data2Sendbytes);*/







