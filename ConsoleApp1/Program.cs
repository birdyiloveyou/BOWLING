using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var list = new List<Score>();
            var play = new Play();
            for (var i = 0; i < 10; i++)
            {
                play.Throw(list);
                play.PrintScore(list);
            }

            Console.WriteLine("Press enter to leave");
            Console.ReadLine();
        }

        public class Play
        {
            public void PrintScore(List<Score> list)
            {
                Console.Write("|");
                foreach (var score in list)
                {
                    if (list.IndexOf(score) == 9)
                    {
                        var first = score.FirstBall == 10 ? "X" : score.FirstBall.ToString();
                        var second = score.SecondBall == 10 ? "X" :
                            score.FirstBall + score.SecondBall == 10 ? "/" : score.SecondBall.ToString();
                        var third = score.ThirdBall == 10 ? "X" : score.ThirdBall.ToString();
                        Console.Write($"{first,2}|{second,2}|{third,2}|");
                    }
                    else if (score.isStrike)
                    {
                        Console.Write($"{"",2}|{"X",2}|");
                    }
                    else if (score.isSpare)
                    {
                        Console.Write($"{score.FirstBall,2}|{"/",2}|");
                    }
                    else
                    {
                        Console.Write($"{score.FirstBall,2}|{score.SecondBall,2}|");
                    }
                }

                Console.WriteLine(" SUM |");
                Console.Write("|");
                var sum = 0;
                foreach (var score in list)
                {
                    if (list.IndexOf(score) == 9)
                    {
                        sum += score.Total;
                        Console.Write($"{sum,8}");
                    }
                    else if (score.Total != 0)
                    {
                        sum += score.Total;

                        Console.Write($"{sum,5}");
                    }
                    else
                    {
                        Console.Write($"{"",5}");
                    }

                    Console.Write("|");
                }

                Console.WriteLine($" {sum,3} |");
            }

            public void Throw(List<Score> list)
            {
                var score = new Score();
                Console.WriteLine($"Please throw your {list.Count + 1} ball");
                score.FirstBall = int.Parse(Console.ReadLine());
                if (score.FirstBall == 10)
                {
                    score.isStrike = true;
                }
                else
                {
                    Console.WriteLine("Second Chance");
                    score.SecondBall = int.Parse(Console.ReadLine());
                    if (score.FirstBall + score.SecondBall == 10)
                    {
                        score.isSpare = true;
                    }
                    else
                    {
                        score.Total = score.FirstBall + score.SecondBall;
                    }
                }

                list.Add(score);
                if (list[list.Count - 1].Total != 0 && list.Count >= 2)
                {
                    list[list.Count - 1].Total += list[list.Count - 2].Total;
                }

                this.Check(list);
            }

            private void Check(List<Score> list)
            {
                var count = list.Count;
                if (count == 10)
                {
                    if (list[9].isStrike)
                    {
                        Console.WriteLine("Second Chance");
                        list[9].SecondBall = int.Parse(Console.ReadLine());

                        Console.WriteLine("Third Chance");
                        list[9].ThirdBall = int.Parse(Console.ReadLine());
                        list[9].Total = 10 + list[9].SecondBall + list[9].ThirdBall;
                        if (list[8].isStrike)
                        {
                            list[8].Total = 20 + list[9].SecondBall;
                        }
                    }
                    else if (list[9].isSpare)
                    {
                        Console.WriteLine("Third Chance");
                        var third = int.Parse(Console.ReadLine());
                        list[9].Total = list[9].FirstBall + list[9].SecondBall + third;
                    }
                }

                if (count >= 3 && list[count - 3].isStrike && list[count - 2].isStrike)
                {
                    list[count - 3].Total = 20 + list[count - 1].FirstBall;
                }
                if (count >= 2)
                {
                    if (list[count - 2].isStrike && !list[count - 1].isStrike)
                    {
                        list[count - 2].Total = 10 + list[count - 1].FirstBall + list[count - 1].SecondBall;
                    }
                    else if (list[count - 2].isSpare)
                    {
                        list[count - 2].Total = 10 + list[count - 1].FirstBall;
                    }
                }
            }
        }

        public class Score
        {
            public int FirstBall { get; set; }
            public bool isSpare { get; set; }
            public bool isStrike { get; set; }
            public int SecondBall { get; set; }
            public int ThirdBall { get; set; }

            public int Total { get; set; }
        }
    }
}