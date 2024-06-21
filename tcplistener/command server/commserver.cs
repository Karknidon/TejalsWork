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
                // using TcpClient handler2 = listener.AcceptTcpClient();
                // using NetworkStream stream2 = handler2.GetStream();
                byte[] buffer = new byte[1024];
                string msg = "Successfully Connected";
                var msgbytes = Encoding.UTF8.GetBytes(msg);
                stream.Write(msgbytes);
                //stream2.Write(msgbytes);

                while (true)
                {
                    stream.Read(buffer, 0, buffer.Length);
                    HB(stream);
                    string done = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Thread c = new Thread(() => Emulator(stream, buffer));
                    c.Start();
                    Thread.Sleep(20);
                        // c.Join();
                    
                }
            }
           

        }

        static void Emulator(NetworkStream stream, byte[] buffer)
        {
            string input = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            Array.Clear(buffer, 0, buffer.Length);
            Console.WriteLine(input); // Debug command
            int size = input.Length;
            Console.WriteLine(size);
            int encCounts;
            int n;
            string str = @"#,+,?";
            Regex re = new Regex(str);
            if (re.IsMatch(input))
            {



                if (input.Contains("EI"))
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
                    encS = 1000 - t * w;
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0));
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));
                    sensor2 = Convert.ToByte(sensor2 | (1 << 3)); ; //indexer calibrated
                    sensor1 = Convert.ToByte(sensor1 | (1 << 2)); ;//SOURCE home
                    encS = 0;
                }
                else if (input.Contains("ED"))
                {
                    int t = 0;
                    int w = 1;
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    encD = 1000 - t * w;
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0));
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));
                    sensor2 = Convert.ToByte(sensor2 | (1 << 3)); ; //indexer calibrated
                    sensor1 = Convert.ToByte(sensor1 | (1 << 6)); ;//DUMMY home
                    encD = 0;
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
            //Array.Clear(buffer, 0, buffer.Length);
            stream.Write(Data2Sendbytes);

        }

       /* public static void EI(NetworkStream stream, byte[] buffer, string input)
        {
            if (sensor2.SetBitTo1(6) != 1 && input.Contains("EI"))
            {
                int w = 1;
                int t = 0;
                sensor2 = sensor2 | 1 <<; //moving
                sensor2[6] = 1; //command in progress
                encI = t * w;
                sensor2[6] = 0;
                sensor2[7] = 0;
                sensor2[4] = 1; //indexer calibrated
                sensor1[3] = 1;//indexer home
                string sens1 = string.Join("", sensor1);
                string sens2 = string.Join("", sensor2);
                string Data2Send = "#" + "," + sens1 + "," + sens2 + "," + Convert.ToString(encI) + "," + Convert.ToString(encS) + "," + Convert.ToString(encD) + "," + Convert.ToString(dwellTime) + "," + Convert.ToString(totalTime) + "," + Convert.ToString(errorCode);
                var Data2Sendbytes = Encoding.UTF8.GetBytes(Data2Send);
                Console.WriteLine(Data2Send);
                stream.Write(Data2Sendbytes);

                encI = 0;
            }

        }*/
       /* public static void ES(NetworkStream stream, byte[] buffer, string input)
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
                Console.WriteLine(Data2Send);
                Array.Clear(buffer, 0, buffer.Length);
                stream.Write(Data2Sendbytes);
                encS = 0;
            }

        }*/
       /* public static void ED(NetworkStream stream, byte[] buffer, string input)
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
                string Data2Send = "#" + "," + Convert.ToDecimal(Convert.ToInt16((sens1))) + "," + Convert.ToDecimal(sens2) + "," + Convert.ToString(encI) + "," + Convert.ToString(encS) + "," + Convert.ToString(encD) + "," + Convert.ToString(dwellTime) + "," + Convert.ToString(totalTime) + "," + Convert.ToString(errorCode);
                var Data2Sendbytes = Encoding.UTF8.GetBytes(Data2Send);
                Array.Clear(buffer, 0, buffer.Length);
                Console.WriteLine(Data2Send);
                stream.Write(Data2Sendbytes);

                encS = 0;
            }


        }*/
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










