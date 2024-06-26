using System;



namespace First
{
    class Program
    {
        static void Main(string[] args)
        {
            /* int b, l;
             double h;
             string bas=Console.ReadLine();
            string height=Console.ReadLine();
             b = Convert.ToInt32(bas);
             l = Convert.ToInt32(height);
             h =Math.Sqrt(b* b + l  *l);
             Console.Write(h);*/
            //int num = 5;
            //num = 5/3;
            //Console.WriteLine(num);
            Random random = new Random();
            int rant=random.Next(1, 7);
            int n = 0;
            while (n != rant)
            {
                n = Convert.ToInt16(Console.ReadLine());
                if (n > rant)
                {
                    Console.WriteLine("go lower");
                }
                else if (n < rant)
                {
                    Console.WriteLine("go higher");
                }
                else
                {
                    Console.WriteLine("yay!you guessed it right");
                }
            }
            //Console.WriteLine(rant);

        }
    }
}