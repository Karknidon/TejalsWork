using System;
using System.Threading;
/*
namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi");
            Thread MainThread = Thread.CurrentThread;
            MainThread.Name = "Main Thread";
            Console.WriteLine(MainThread.Name);
            Thread t1 = new Thread(timer1);
            Thread t2 = new Thread(timer2);
            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }
        public static void timer1()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine("timer1 " + i);
                    
                }
                Thread.Sleep(2);
            }
        }
        public static void timer2()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 != 0)
                {
                    Console.WriteLine("timer2 " + i);
                  
                }
                Thread.Sleep(2);
            }
        }
    }
}
*/