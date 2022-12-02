using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day2
{
    internal class Day2
    {
        class Game
        {
            private string opp, me;
            public Game(string opp, string me)
            {
                this.opp = opp;
                this.me = me;
            }
            public int Score()
            {
                switch (opp)
                {
                    case "A":                   // Rock
                        switch (me)
                        {
                            case "X": return 4; // Rock -Draw
                            case "Y": return 8; // Paper -Win
                            default: return 3; // Sci -Loss
                        }
                    case "B":                   // Paper
                        switch (me)
                        {
                            case "X": return 1; // Rock -Loss
                            case "Y": return 5; // Paper -Draw
                            default: return 9; // Sci -Win
                        }
                    default:                    // Scissors
                        switch (me)
                        {
                            case "X": return 7; // Rock -Win
                            case "Y": return 2; //Paper -Loss
                            default: return 6; //Sci -Draw
                        }
                }
            }
            public int Score2()
            {
                switch (opp)
                {
                    case "A":           //Rock
                        switch (me)
                        {
                            case "X": return 3; //Lose -Sci
                            case "Y": return 4; //Draw -Rock
                            default: return 8; //Win -Paper
                        }
                    case "B":           //Paper
                        switch (me)
                        {
                            case "X": return 1; //Lose -Rock
                            case "Y": return 5; //Draw -Paper
                            default: return 9; //Win -Sci
                        }
                    default:            //Sci
                        switch (me)
                        {
                            case "X": return 2; //Lose -Paper
                            case "Y": return 6; //Draw -Sci
                            default: return 7; //Win -Rock
                        }
                }
            }
        }
        static void Main(string[] args)
        {
            List<Game> allGames = new List<Game>();
            using (StreamReader reader = new StreamReader(new FileStream("input.txt",FileMode.OpenOrCreate)))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(' ');
                    allGames.Add(new Game(line[0], line[1]));
                }
            }
            int total = 0, total2 = 0;
            foreach (Game game in allGames)
            {
                total += game.Score();
                total2 += game.Score2();
            }
            Console.WriteLine($"Final Score: {total}");
            Console.WriteLine($"Final new Score: {total2}");


                Console.ReadKey();
        }
    }
}
