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
        public static byte sensor1;
        public static byte sensor2;
        public static int encI = 1000;
        public static int encS;
        public static int encD;
        public static byte dwellTime;
        public static byte totalTime;
        public static byte errorCode;



        public static void Main(string[] args)
        {


            //Connection
            Console.WriteLine("Server");
            var port = 11;     // 2321;
            IPAddress ip = IPAddress.Parse("127.0.0.1");                //("192.168.99.208");
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
                    if (encS == 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                    }
                    if (encI == 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 4));

                    }
                    if (encD == 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 6));

                    }


                    stream.Read(buffer, 0, buffer.Length);
                    HB(stream);
                    string done = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Thread c = new Thread(() => Emulator(stream, buffer));
                    c.Start();
                    Thread.Sleep(20);
                       
                    
                }
            }
           

        }

        static void Emulator(NetworkStream stream, byte[] buffer)
        {
            string input = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            Array.Clear(buffer, 0, buffer.Length);
           // Console.WriteLine(input); // Debug command
            int size = input.Length;
           // Console.WriteLine(size);
            int encCounts;
            int n;
            string str = @"#,+,?";
            Regex re = new Regex(str);
            if (re.IsMatch(input))
            {


                if (input.Contains("MOVEIN"))
                {
                    encCounts = 5;
                    int end = input.IndexOf(",", 2);
                    int start = input.IndexOf("N");
                    n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                    int mul = encCounts * n;
                    int add = 0;
                    if (encS > 0)
                    {
                        while (add < mul)
                        {
                            Thread.Sleep(50);
                            encS -= 10;
                            add += 10;
                        }
                    }
                    else if (encS < 0)
                    {

                        while (add < mul)
                        {
                            Thread.Sleep(50);
                            encS += 10;
                            add = add + 10;
                        }
                    }
                    if (encS == 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                    }
                    else
                    {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));
                    }

                }
                else if (input.Contains("MOVEOUT"))
                {
                    encCounts = 5;
                    int end = input.IndexOf(",", 2);
                    int start = input.IndexOf("T");
                    n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                    int mul = encCounts * n;
                    int add = 0;
                    if (encS >= 0)
                    {
                        while (add < mul)
                        {
                            Thread.Sleep(50);
                            encS += 10;
                            add += 10;
                        }
                    }
                    else if (encS < 0)
                    {

                        while (add < mul)
                        {
                            Thread.Sleep(50);
                            encS -= 10;
                            add = add + 10;
                        }
                    }
                    if (encS == 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                    }
                    else
                    {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));
                    }

                }
                else if (input.Contains("EI"))
                {
                    int w = 1;
                    int t = 0;
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    encI = t * w;
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0));
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));
                    sensor2 = Convert.ToByte(sensor2 | (1 << 3)); ; //indexer calibrated
                    sensor1 = Convert.ToByte(sensor1 | (1 << 4)); ; //indexer home
                    encI = 0;

                }
               
                else if (input.Contains("ES"))
                {
                    int t = 0;
                    int w = 1;
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    encS = t * w;
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0));
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));
                    sensor2 = Convert.ToByte(sensor2 | (1 << 3));  //indexer calibrated
                    sensor1 = Convert.ToByte(sensor1 | (1 << 2)); //SOURCE home

                    //encS = 0;
                }
                else if (input.Contains("ED"))
                {
                    int t = 0;
                    int w = 1;
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    encD = t * w;
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0));
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));
                    sensor2 = Convert.ToByte(sensor2 | (1 << 3)); ; //indexer calibrated
                    sensor1 = Convert.ToByte(sensor1 | (1 << 6)); ;//DUMMY home
                    //encD = 0;
                }
                else if (input.Contains("MSF"))
                {
                    encCounts = 5;
                    int end = input.IndexOf(",", 2);
                    int start = input.IndexOf("F");
                    n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                    int mul = encCounts * n;
                    int add = 0;
                    while (add < mul)
                    {
                        Thread.Sleep(50);
                        encS += 10;
                        add += 10;
                    }
                    if (encS == 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                    }
                    else
                    {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));
                        sensor1 = Convert.ToByte(sensor1 | (1 << 0)); ;
                    }
                }
                else if (input.Contains("MSR"))
                {
                    encCounts = 5;
                    int end = input.IndexOf(",", 2);
                    int start = input.IndexOf("R");
                    n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                    int mul = encS - (encCounts * n);
                 
                    while (encS > mul)
                    {
                        Thread.Sleep(50);
                        encS -= 10;
                    }
                    if (encS == 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                    }
                    else
                    {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));
                        sensor1 = Convert.ToByte(sensor1 | (1 << 0)); 
                    }

                }
                else if (input.Contains("MDF"))
                {
                    encCounts = 5;
                    int end = input.IndexOf(",",2);
                    int start=input.IndexOf("F");
                    n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                    int mul = encCounts * n;
                    int add = 0;
                    while (add < mul)
                    {
                        Thread.Sleep(50);
                        encD += 10;
                        add += 10;
                    }
                    if (encD == 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 6));
                    }
                    else
                    {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 6));
                    }

                }
                else if (input.Contains("MDR"))
                {
                    encCounts = 5;
                    int end = input.IndexOf(",", 2);
                    int start = input.IndexOf("R");
                    n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                    int mul = encD - (encCounts * n);
                    while (encD > mul)
                    {
                        Thread.Sleep(50);
                        encD -= 10;
                    }
                    if (encD == 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 6));
                    }
                    else
                    {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 6));
                    }

                }
                else if (input.Contains("MIF"))
                {
                    encCounts = 1;
                    int end = input.IndexOf(",", 2);
                    int start = input.IndexOf("F");
                    n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                    int mul = encCounts * n;
                    int add = 0;
                    while (add < mul)
                    {
                        Thread.Sleep(50);
                        encI += 10;
                        add = add + 10;
                    }
                    if (encI == 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 4));
                    }
                    else
                    {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 4));
                    }
                }

                else if (input.Contains("MIR"))
                {
                    encCounts = 1;
                    int end = input.IndexOf(",", 2);
                    int start = input.IndexOf("R");
                    n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                    int mul = encI - (encCounts * n);
                    
                    while (encI > mul)
                    {
                        Thread.Sleep(50);
                        encI -= 10;
                        
                    }
                    if (encI == 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 4));
                    }
                    else
                    {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 4));
                    }


                }
                else if (input.Contains("OI"))
                {
                    while (encI != 0)
                    {
                        if (encI > 0)
                        {
                            encI -= 10;
                            Thread.Sleep(50);
                        }
                        else
                        {
                            encI += 10;
                            Thread.Sleep(50);
                        }
                    }
                    sensor1 = Convert.ToByte(sensor1 | (1 << 4));

                }
                else if (input.Contains("OS"))
                {
                    while (encS != 0)
                    {
                        if (encS > 0)
                        {
                            encS -= 10;
                            Thread.Sleep(50);
                        }
                        else
                        {
                            encS += 10;
                            Thread.Sleep(50);
                        }
                    }
                    sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                }
                else if (input.Contains("OD"))
                {
                    while (encD != 0)
                    {
                        if (encD > 0)
                        {
                            encD -= 10;
                            Thread.Sleep(50);
                        }
                        else
                        {
                            encD += 10;
                            Thread.Sleep(50);
                        }
                    }
                    sensor1 = Convert.ToByte(sensor1 | (1 << 6));
                }
                else if (input.Contains("P"))
                {
                    int start = input.IndexOf("P");
                    int end = input.IndexOf(",", 2);
                    input = input.Substring(start + 1, end - start - 1);
                    Console.WriteLine(input);
                    n = Convert.ToInt32(input);
                    int add = 0;
                    while (add < n)
                    {
                        encI += 10;
                        add += 10;
                        Thread.Sleep(50);
                    }
                }
                else if (input.Contains("S"))
                {
                    int start = input.IndexOf("S");
                    int end = input.IndexOf(",", 2);
                    input = input.Substring(start + 1, end - start - 1);
                    Console.WriteLine(input);
                    n = Convert.ToInt32(input);
                    int add = 0;
                    while (add < n/5)
                    {
                        encS += 10;
                        add += 10;
                        Thread.Sleep(50);
                    }
                }
                else if (input.Contains("D"))
                {
                    int start = input.IndexOf("D");
                    int end = input.IndexOf(",", 2);
                    input = input.Substring(start + 1, end - start - 1);
                    Console.WriteLine(input);
                    n = Convert.ToInt32(input);
                    int add = 0;
                    while (add < n/5)
                    {
                        encD += 10;
                        add += 10;
                        Thread.Sleep(50);
                    }
                }
                else if (input.Contains("W"))
                {
                    int start = input.IndexOf("W");
                    int end = input.IndexOf(",", 2);
                    input = input.Substring(start+1, end - start - 1);
                    Console.WriteLine(input);
                    int value = Convert.ToInt32(input);
                    Thread.Sleep(value);
                }
                else if (input.Contains("I"))
                {
                    Thread.Sleep(100);
                    int w = 1;
                    int t = 0;
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    int encEnd = 1000;

                    while (encI > 0)
                    {
                        encI = encEnd - t * w;
                        Thread.Sleep(500);

                        t += 100;

                    }
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0));
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));
                    sensor2 = Convert.ToByte(sensor2 | (1 << 3)); ; //indexer calibrated
                    sensor1 = Convert.ToByte(sensor1 | (1 << 4)); ; //indexer home

                }
                

            }
        }
        public static void HB(NetworkStream stream)
        {
            string Data2Send = "#," + Convert.ToString(sensor1) + "," + Convert.ToString(sensor2) + "," + Convert.ToString(encI) + "," + Convert.ToString(encS) + "," + Convert.ToString(encD) + "," + Convert.ToString(dwellTime) + "," + Convert.ToString(totalTime) + "," + Convert.ToString(errorCode);
            var Data2Sendbytes = Encoding.UTF8.GetBytes(Data2Send);
            Console.WriteLine(Data2Send);
            stream.Write(Data2Sendbytes);

        }

       
        public static void I(NetworkStream stream, byte[] buffer, string input)
        {
            // if (sensor2[6] != 1 && input.Contains("I"))
            // {
            Thread.Sleep(100);
            int w = 1;
            int t = 0;
            sensor2 =Convert.ToByte(sensor2 |( 1 << 0)); //moving
            sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
            int encEnd = 1000;

            while (encI > 0)
            {
                encI = encEnd - t * w;

                // HB(stream, buffer, input);
                Thread.Sleep(500);

                t += 100;

            }
            sensor2 = Convert.ToByte(sensor2 & ~(1 << 0));
            sensor2 =Convert.ToByte(sensor2 & ~(1 << 1));
            sensor2 = Convert.ToByte(sensor2 | (1 << 3)); ; //indexer calibrated
            sensor1 = Convert.ToByte(sensor1 | (1 << 4)); ; //indexer home
            encI = 0;

        }
    

       public static void RES(NetworkStream stream, byte[] buffer, string input)
        {

            sensor1 = 0;
            sensor2 = 0;
           encI = 1000;
            encS = 0;
            encD = 0;
             dwellTime = 0;
            totalTime = 0;
            errorCode = 0;
            string Data2Send = "#" + "," +Convert.ToString(sensor1) + "," +Convert.ToString(sensor2) + "," + Convert.ToString(encI) + "," + Convert.ToString(encS) + "," + Convert.ToString(encD) + "," + Convert.ToString(dwellTime) + "," + Convert.ToString(totalTime) + "," + Convert.ToString(errorCode);
            var Data2Sendbytes = Encoding.UTF8.GetBytes(Data2Send);
            Console.WriteLine(Data2Send);
            Array.Clear(buffer, 0, buffer.Length);
            stream.Write(Data2Sendbytes);
        }

        }
}










