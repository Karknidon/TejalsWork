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
        public static int errorCode;
        public static readonly int[] indexerVals = { 730, 5730, 10730, 15730, 20730, 25730, 30730, 35730, 40730, 45730, 50730, 55730, 60730, 65730, 70730, 75730, 80730, 85730, 90730, 95730 };
        public static int d, s = 0;



        public static void Main(string[] args)
        {


            //Connection
            Console.WriteLine("Server");
            var port = 2321;
            IPAddress ip = IPAddress.Parse("192.168.99.208");
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
               //stream.Write(msgbytes);

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
                sensor1 = Convert.ToByte(sensor1 | (1 << 1));
                sensor1 = Convert.ToByte(sensor1 | (1 << 5));
                //errorCode = 205;
                while (true)
                {


                    stream.Read(buffer, 0, buffer.Length);
                    HB(stream);
                    string done = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Thread c = new Thread(() => Emulator(stream, buffer));
                    c.Start();
                    string input = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    if (input.Contains("W") || input.Contains("DOOR"))
                    {
                        c.Join();
                    }
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


                //if (input.Contains("MOVEIN"))
                //{

                //    encCounts = 5;
                //    int end = input.IndexOf(",", 2);
                //    int start = input.IndexOf("N");
                //    n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                //    int mul = encCounts * n;
                //    int add = 0;
                //    if (encS > 0)
                //    {
                //        while (add < mul)
                //        {
                //            Thread.Sleep(50);
                //            encS -= 10;
                //            add += 10;
                //        }
                //    }
                //    else if (encS < 0)
                //    {

                //        while (add < mul)
                //        {
                //            Thread.Sleep(50);
                //            encS += 10;
                //            add = add + 10;
                //        }
                //    }
                //    if (encS == 0)
                //    {
                //        sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                //    }
                //    else
                //    {
                //        sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));
                //    }

                //}
                //else if (input.Contains("MOVEOUT"))
                //{
                //    encCounts = 5;
                //    int end = input.IndexOf(",", 2);
                //    int start = input.IndexOf("T");
                //    n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                //    int mul = encCounts * n;
                //    int add = 0;
                //    if (encS >= 0)
                //    {
                //        while (add < mul)
                //        {
                //            Thread.Sleep(50);
                //            encS += 10;
                //            add += 10;
                //        }
                //    }
                //    else if (encS < 0)
                //    {

                //        while (add < mul)
                //        {
                //            Thread.Sleep(50);
                //            encS -= 10;
                //            add = add + 10;
                //        }
                //    }
                //    if (encS == 0)
                //    {
                //        sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                //    }
                //    else
                //    {
                //        sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));
                //    }

                //}
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
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));
                }

                else if (input.Contains("ES"))
                {
                    int t = 0;
                    int w = 1;
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    encS = t * w;
                    if (Convert.ToByte(sensor1 & (1 << 1)) != 2)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 1));
                    }
                    if (Convert.ToByte(sensor1 & (1 << 0)) == 1)
                    {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 0));
                    }
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0));
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));
                    sensor2 = Convert.ToByte(sensor2 | (1 << 3));  //indexer calibrated
                    sensor1 = Convert.ToByte(sensor1 | (1 << 2)); //SOURCE home
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));

                    //encS = 0;
                }
                else if (input.Contains("ED"))
                {
                    int t = 0;
                    int w = 1;
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    encD = t * w;
                    if(Convert.ToByte(sensor1 & (1 << 5)) != 32)
                    {
                        sensor1 = Convert.ToByte(sensor1 | (1 << 5));
                    }
                    if (Convert.ToByte(sensor1 & (1 << 0)) == 1)
                    {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 0));
                    }
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0));
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));
                    sensor2 = Convert.ToByte(sensor2 | (1 << 3)); ; //indexer calibrated
                    sensor1 = Convert.ToByte(sensor1 | (1 << 6)); ;//DUMMY home
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));
                    //encD = 0;
                }
                else if (input.Contains("MSF"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress

                    if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && (Convert.ToByte(sensor1 & (1 << 1)) != 2))
                    {
                        errorCode = 209;
                    }
                    else if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && (d > 0))
                    {
                        errorCode = 500;
                    }

                    else
                    {
                        encCounts = 5;
                        int end = input.IndexOf(",", 2);
                        int start = input.IndexOf("F");
                        n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                        int mul = encCounts * n;
                        int add = 0;
                        while (mul != 0)
                        {

                            if (mul >= 500)
                            {
                                Thread.Sleep(50);
                                encS += 100;
                                mul -= 100;
                            }
                            else if (mul >= 100)
                            {
                                Thread.Sleep(50);
                                encS += 50;
                                mul -= 50;
                            }
                            else if (mul >= 50)
                            {
                                Thread.Sleep(50);
                                encS += 10;
                                mul -= 10;
                            }
                            else 
                            {
                                Thread.Sleep(50);
                                encS += 1;
                                mul -= 1;
                            }
                            if (encS != 0)
                            {
                                sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));
                            }
                            if (encS >= 1585)
                            {
                                sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                s++;
                                if (encS == 21240)
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 1));
                                }
                                else if (encS > 21240)
                                {
                                    encS = 21240;
                                    errorCode = 209;
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 1));
                                    break;
                                }
                            }
                        }
                        if (encS == 0)
                        {
                            sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                        }


                    }
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                }

                else if (input.Contains("MSR"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress      
                    if (encS == 0)
                    {
                        errorCode = 208;
                    }
                    else if (encS > 0)
                    {
                        encCounts = 5;
                        int end = input.IndexOf(",", 2);
                        int start = input.IndexOf("R");
                        n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                        int mul = encCounts * n;


                        while (mul != 0)
                        {
                            if (mul >= 500)
                            {
                                Thread.Sleep(50);
                                encS -= 100;
                                mul -= 100;
                            }
                            else if (mul >= 100)
                            {
                                Thread.Sleep(50);
                                encS -= 50;
                                mul -= 50;
                            }
                            else if (mul >= 50)
                            {
                                Thread.Sleep(50);
                                encS -= 10;
                                mul -= 10;
                            }
                           
                            else
                            {
                                Thread.Sleep(50);
                                encS -= 1;
                                mul -= 1;
                            }

                            if (encS < 0)
                            {
                                errorCode = 208;
                                sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                                encS = 0;
                                break;
                            }
                            else if (encD < 21420)
                            {
                                sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                if (encD < 1585)
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 0));
                                    s--;
                                }
                            }


                            else
                            {
                                sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));

                            }
                        }
                    }

                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                }

                else if (input.Contains("MDF"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && (Convert.ToByte(sensor1 & (1 << 5)) != 32))
                    {
                        errorCode = 204;
                    }
                    else if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && s > 0)
                    {
                        errorCode = 500;
                    }

                    else
                    {
                        encCounts = 5;
                        int end = input.IndexOf(",", 2);
                        int start = input.IndexOf("F");
                        n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                        int mul = encCounts * n;
                        int add = 0;
                        while (mul != 0)
                        {

                            if (mul >= 500)
                            {
                                Thread.Sleep(50);
                                encD += 100;
                                mul -= 100;
                            }
                            else if (mul >= 100)
                            {
                                Thread.Sleep(50);
                                encD += 50;
                                mul -= 50;
                            }
                            else if (mul >= 50)
                            {
                                Thread.Sleep(50);
                                encD += 10;
                                mul -= 10;
                            }
                            
                            else
                            {
                                Thread.Sleep(50);
                                encD += 1;
                                mul -= 1;
                            }

                            if (encD != 0)
                            {
                                sensor1 = Convert.ToByte(sensor1 & ~(1 << 6));
                            }
                            if (encD >= 1585)
                            {
                                d++;
                                sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                if (encD == 21240)
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                }
                                else if (encD > 21240)
                                {
                                    encD = 21240;
                                    errorCode = 204;
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                    break;
                                }
                            }
                        }
                    }

                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                }

                else if (input.Contains("MDR"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    if (encD == 0)
                    {
                        errorCode = 203;
                    }
                    if (encD > 0)
                    {
                        encCounts = 5;
                        int end = input.IndexOf(",", 2);
                        int start = input.IndexOf("R");
                        n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                        int mul = encCounts * n;
                        while (mul != 0)
                        {
                            if (mul >= 500)
                            {
                                Thread.Sleep(50);
                                encD -= 100;
                                mul -= 100;
                            }
                            else if (mul >= 100)
                            {
                                Thread.Sleep(50);
                                encD -= 50;
                                mul -= 50;
                            }
                            else if (mul >= 50)
                            {
                                Thread.Sleep(50);
                                encD -= 10;
                                mul -= 10;
                            }
                            else
                            {
                                Thread.Sleep(50);
                                encD -= 1;
                                mul -= 1;
                            }
                           
                            if (encD < 0)
                            {
                                errorCode = 203;
                                sensor1 = Convert.ToByte(sensor1 | (1 << 6));
                                encD = 0;
                                break;
                            }
                            else if (encD < 21420)
                            {
                                sensor1 = Convert.ToByte(sensor1 | (1 << 5));
                                if (encD < 1585)
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 0));
                                    d--;
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

                        }

                    }
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                }

                else if (input.Contains("MIF"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    int k = 0;
                    for (int i = 0; i < 20; i++)
                    {
                        if (indexerVals[i] - 5 < encI && encI<indexerVals[i]+5)
                        {
                            k++;
                            break;
                        }
                    }
                    if (k == 0)
                    {
                        int b = 0;
                        encCounts = 1;
                        int end = input.IndexOf(",", 2);
                        int start = input.IndexOf("F");
                        n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                        int mul = encCounts * n;
                        int add = 0;
                        while (add < mul)
                        {
                            Thread.Sleep(50);
                            encI += 5;
                            add = add + 5;
                            for (int i = 0; i < 20; i++)
                            {
                                if (encI == indexerVals[i])
                                {
                                    sensor1 = Convert.ToByte(sensor1 | (1 << 3));
                                    b++;
                                    break;
                                }
                                else
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 3));
                                }
                            }
                            if (b != 0)
                            {
                                break;
                            }
                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 4));
                        }

                    }
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                }

                else if (input.Contains("MIR"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    int k = 0;
                    for (int i = 0; i < 20; i++)
                    {
                        if (encI == indexerVals[i])
                        {
                            k++;
                            break;

                        }
                    }
                    if (k == 0)
                    {
                        int b = 0;
                        encCounts = 1;
                        int end = input.IndexOf(",", 2);
                        int start = input.IndexOf("R");
                        n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                        int mul = encCounts * n;
                        int add = 0;
                        while (add < mul)
                        {
                            Thread.Sleep(50);
                            encI -= 5;
                            add += 5;


                            for (int j = 0; j < 20; j++)
                            {
                                if (encI == indexerVals[j])
                                {
                                    sensor1 = Convert.ToByte(sensor1 | (1 << 3));
                                    b++;
                                    errorCode = 0;
                                    break;
                                }
                                else
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 3));
                                }

                            }
                            if (b != 0)
                            {
                                break;
                            }


                        }


                    }
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                }

                else if (input.Contains("OI"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    while (encI != 0)
                    {
                        if (encI >= 500)
                        {
                            encI -= 100;
                            Thread.Sleep(50);
                        }
                        else if (encI >=100)
                        {
                            encI -= 50;
                            Thread.Sleep(50);
                        }
                        else if (encI >= 50)
                        {
                            encI -= 10;
                            Thread.Sleep(50);
                        }
                        else if (encI >= 10)
                        {
                            encI -= 5;
                            Thread.Sleep(50);
                        }
                        else
                        {
                            encI -= 1;
                            Thread.Sleep(50);
                        }
                        for (int i=0;i<20; i++)
                            {
                                if (!(indexerVals[i]-5 <encI && encI < indexerVals[i] + 5))
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 3));
                                }
                            }
                    }
                    sensor1 = Convert.ToByte(sensor1 | (1 << 4));
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                }
                else if (input.Contains("OS"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    while (encS != 0)
                    {
                        if (encS >= 500)
                        {
                            encS -= 100;
                            Thread.Sleep(50);
                        }
                        else if (encS >= 100)
                        {
                            encS -= 50;
                            Thread.Sleep(50);
                        }
                        else if (encS >= 50)
                        {
                            encS -= 10;
                            Thread.Sleep(50);
                        }
                        else if (encS >= 10)
                        {
                            encS -= 5;
                            Thread.Sleep(50);
                        }
                        else
                        {
                            encS -= 1;
                            Thread.Sleep(50);
                        }
                    }
                    if((Convert.ToByte(sensor1 & (1 << 0)) == 1) && s>0) {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 0));
                    }
                    sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                }
                else if (input.Contains("OD"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    while (encD != 0)
                    {
                        if (encD >= 500)
                        {
                            encD -= 100;
                            Thread.Sleep(50);
                        }
                        else if (encD >= 100)
                        {
                            encD -= 50;
                            Thread.Sleep(50);
                        }
                        else if (encD >= 50)
                        {
                            encD -= 10;
                            Thread.Sleep(50);
                        }
                        else if (encD >= 10)
                        {
                            encD -= 5;
                            Thread.Sleep(50);
                        }
                        else
                        {
                            encD -= 1;
                            Thread.Sleep(50);
                        }
                    }
                    if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && d > 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 0));
                    }
                    sensor1 = Convert.ToByte(sensor1 | (1 << 6));
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                }
                else if (input.Contains("DOOR"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    Console.Write("Imput:");
                    string inp = Console.ReadLine();
                    if (inp == "OPEN")
                    {
                        sensor2 = Convert.ToByte(sensor2 | (1 << 4));
                    }
                    Thread.Sleep(100);
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));
                }
                else if (input.Contains("R"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    errorCode = 0;
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                }
                else if (input.Contains("P"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    int start = input.IndexOf("P");
                    int end = input.IndexOf(",", 2);
                    input = input.Substring(start + 1, end - start - 1);
                    n = Convert.ToInt32(input);
                    int mul = encI - n;
                    int add =Math.Abs(mul);
                    if (mul < 0)
                    {
                        while (add != 0)
                        {
                            if (add >= 10)
                            {
                                encI += 5;
                                add -= 5;
                                Thread.Sleep(50);
                            }
                            else if (add < 10)
                            {
                                encI += 1;
                                add -= 1;
                                Thread.Sleep(50);
                            }
                        }

                    }
                    
                    else
                    {
                        while (add != 0)
                        {
                            if (add >= 10)
                            {
                                encI -= 5;
                                add -= 5;
                                Thread.Sleep(50);
                            }
                            else if (add < 10)
                            {
                                encI -= 1;
                                add -= 1;
                                Thread.Sleep(50);
                            }
                        }
                    }
                    for (int i = 0; i < 20; i++)
                            {
                                if (encI > indexerVals[i] -5 && encI < indexerVals[i]+5)
                                {
                                    sensor1 = Convert.ToByte(sensor1 | (1 << 3));
                                    break;
                                   
                                }
                                else
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 3));
                                }
                            }
                            
                    if (encI != 0)
                    {
                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 4));
                    }
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                }
                else if (input.Contains("S"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    int start = input.IndexOf("S");
                    int end = input.IndexOf(",", 2);
                    input = input.Substring(start + 1, end - start - 1);
                    Console.WriteLine(input);
                    n = Convert.ToInt32(input);
                    int add = 0;
                    int mul = encS - n;

                    if (mul < 0)
                    {
                        while (add > mul)
                        {
                            encS += 5;
                            add -= 5;
                            if (encS != 0)
                            {
                                sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));
                            }
                            if (encS >= 1585)
                            {
                                sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                if (encS == 21225)
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                }
                                else if (encS > 21225)
                                {
                                    encS = 21225;
                                    errorCode = 204;
                                    break;
                                }
                            }
                            Thread.Sleep(50);
                        }
                    }
                    else
                    {
                        while (add < mul)
                        {
                            encS -= 5;
                            add += 5;
                            if (encS != 0)
                            {
                                sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));
                            }
                            if (encS >= 1585)
                            {
                                sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                if (encS == 21225)
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                }
                                else if (encS > 21225)
                                {
                                    encS = 21225;
                                    errorCode = 204;
                                    break;
                                }
                            }
                            Thread.Sleep(50);
                        }
                    }
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                }
                else if (input.Contains("D"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    int start = input.IndexOf("D");
                    int end = input.IndexOf(",", 2);
                    input = input.Substring(start + 1, end - start - 1);
                    Console.WriteLine(input);
                    n = Convert.ToInt32(input);
                    int add = 0;
                    int mul = encD - n;
                    if (mul < 0)
                    {
                        while (add > mul)
                        {
                            encD += 5;
                            add -= 5;
                            if (encD != 0)
                            {
                                sensor1 = Convert.ToByte(sensor1 & ~(1 << 6));
                            }
                            if (encD >= 1585)
                            {
                                sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                if (encD == 2125)
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                }
                                else if (encD > 2125)
                                {
                                    encD = 2125;
                                    errorCode = 204;
                                    break;
                                }
                            }
                            Thread.Sleep(50);
                        }

                    }
                    else
                    {
                        while (add < mul)
                        {
                            encD -= 5;
                            add += 5;
                            if (encD != 0)
                            {
                                sensor1 = Convert.ToByte(sensor1 & ~(1 << 6));
                            }
                            if (encD >= 1585)
                            {
                                sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                if (encD == 2125)
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                }
                                else if (encD > 2125)
                                {
                                    encD = 2125;
                                    errorCode = 204;
                                    break;
                                }
                            }
                            Thread.Sleep(50);
                        }
                    }
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                }
                else if (input.Contains("W"))
                {
                    sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                    int start = input.IndexOf("W");
                    int end = input.IndexOf(",", 2);
                    input = input.Substring(start + 1, end - start - 1);
                    Console.Write("Input2:");
                    string input2=Console.ReadLine();
                    int value = Convert.ToInt32(input);
                    if (input2.Contains("DOOR"))
                    {
                        sensor2 = Convert.ToByte(sensor2 | (1 << 4));
                        errorCode = 501;
                    }
                    else if (input2.Contains("LMO"))
                    {
                        sensor2 = Convert.ToByte(sensor2 | (1 << 5));
                        errorCode = 501;
                    }
                    else if (input2.Contains("EMG"))
                    {
                        sensor2 = Convert.ToByte(sensor2 | (1 << 6));
                        errorCode = 501;
                    }
                    else
                    {
                        Thread.Sleep(value);
                    }
                   
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                    sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                    
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
        }
    }



            //public static int Binary_Search(int[] a,int start,int end,int key)
            //{
            //while (start <= end)
            //{
            //    int mid=(start+end)/2;
            //    if (mid > key)
            //    {
            //        Binary_Search(a, start, mid - 1, key);
            //        return 0;
            //    }
            //    else if(mid < key)
            //    {
            //        Binary_Search(a, mid + 1, end, key);
            //        return 0;
            //    }
            //    else if (mid == key)
            //    {
            //        return 1;
            //    }
            //    else
            //    {
            //        return -1;
            //    }
            //}
            //}


        

        
    










