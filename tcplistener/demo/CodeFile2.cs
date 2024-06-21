using System;
using System.Net;
using System.Diagnostics;


namespace HelloWorld
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            int t = 0;
            var clock = System.Diagnostics.Stopwatch.StartNew();
            Thread.Sleep(50);
            clock.Stop();
            t += Convert.ToInt16(clock.ElapsedMilliseconds);
            Console.WriteLine(t);
            int myByte = 0;

            // Let's set the third bit
            int bitNumber = 3;
            myByte = myByte | 1 << bitNumber;
            Console.WriteLine(myByte);
        }
    }
}