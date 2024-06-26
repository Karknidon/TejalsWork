using System;



namespace First
{
    class Program
    {
        static void Main(string[] args)
        {
            //int b, l;
            //double h;
            //string bas = Console.ReadLine();
            //string height = Console.ReadLine();
            //b = Convert.ToInt32(bas);
            //l = Convert.ToInt32(height);
            //h = Math.Sqrt(b * b + l * l);
            //Console.Write(h);
            //int num = 5;
            //num = 5/3;
            //Console.WriteLine(num);
            //Random random = new Random();
            //int rant = random.Next(1, 7);
            //int n = 0;
            //while (n != rant)
            //{
            //    n = Convert.ToInt16(Console.ReadLine());
            //    if (n > rant)
            //    {
            //        Console.WriteLine("go lower");
            //    }
            //    else if (n < rant)
            //    {
            //        Console.WriteLine("go higher");
            //    }
            //    else
            //    {
            //        Console.WriteLine("yay!you guessed it right");
            //    }
            //}
            //Console.WriteLine(rant);
            //string name = "Rama Gupta";
            //name=name.Insert(0,"@");
            //Console.WriteLine(name);
            //string phone = "123-123-657";
            //phone = phone.Replace("-", " ");
            //Console.WriteLine(phone);
            Console.WriteLine("ROCK,PAPER,SCISSORS GAME");
            int score_comp = 0, score_player = 0;
            while (true)
            {
                Console.Write("PLAYER:");
                string player = Console.ReadLine();
                player = player.ToUpper();
                string c = "";
                Console.WriteLine("YOUR CHOICE:" + player);
                Console.Write("COMPUTER'S CHOICE:");
                Random rand = new Random();

                int comp = rand.Next(1, 4);
                switch (comp)
                {
                    case 1:
                        Console.WriteLine("ROCK");
                        c = "ROCK";
                        break;

                    case 2:
                        Console.WriteLine("PAPER");
                        c = "PAPER";
                        break;

                    case 3:
                        Console.WriteLine("SCISSORS");
                        c = "SCISSORS";
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        break;


                }

                if (player == "ROCK")
                {
                    if (c == "SCISSORS")
                    {
                        Console.WriteLine("Yay!! You won the point :)");
                        score_player++;
                    }
                    else if (c == "ROCK")
                    {
                        Console.WriteLine("It's a Draw :|");

                    }


                    else if (c == "PAPER")
                    {
                        Console.WriteLine("You Lost TT!Better Luck Next Time...");
                        score_comp++;
                    }
                }

                else if (player == "PAPER")
                {
                    if (c == "SCISSORS")
                    {
                        Console.WriteLine("You Lost TT!Better Luck Next Time...");

                        score_comp++;
                    }
                    else if (c == "PAPER")
                    {
                        Console.WriteLine("It's a Draw :|");
                    }


                    else if (c == "ROCK")
                    {

                        Console.WriteLine("Yay!! You won the point :)");
                        score_player++;
                    }

                }
                else if (player == "SCISSORS")
                {
                    if (c == "SCISSORS")
                    {
                        Console.WriteLine("It's a Draw :|");
                    }
                    else if (c == "ROCK")
                    {

                        Console.WriteLine("You Lost TT!Better Luck Next Time...");

                        score_comp++;
                    }


                    else if (c == "PAPER")
                    {

                        Console.WriteLine("Yay!! You won the point :)");
                        score_player++;
                    }

                }

                Console.Write("Play Again?(Y/N)  ");
                string play = Console.ReadLine();
                play = play.ToUpper();
                if (play == "N")
                {
                    break;
                }
            }
            Console.WriteLine("YOUR POINT:" + score_player);
            Console.WriteLine("COMPUTER'S POINT:" + score_comp);
            int final;
            if (score_comp > score_player)
            {
                final = score_comp - score_player;
                Console.WriteLine("COMPUTER WON BY " + final + " POINTS");
            }
            else if (score_player > score_comp)
            {
                final = score_player - score_comp;
                Console.WriteLine("YOU WON BY " + final + " POINTS");
            }

        }
    }
}

        
    
