using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day1
{
    internal class Day1
    {
        static void Main(string[] args)
        {
            List<int> Elves = new List<int>();
            using (StreamReader sr = new StreamReader(new FileStream("input.txt", FileMode.OpenOrCreate)))
            {
                int elf = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line != "")
                    {
                        elf += int.Parse(line);
                    }
                    else
                    {
                        Elves.Add(elf);
                        elf = 0;
                    }
                }
            }
            Console.WriteLine($"An Elf has {Elves.Max()}.");
            int MaxBerries = Elves.Max();
            Elves.Remove(Elves.Max());
            MaxBerries += Elves.Max();
            Elves.Remove(Elves.Max());
            MaxBerries += Elves.Max();
            Console.WriteLine($"Max Berry: {MaxBerries}");

            Console.ReadKey();
        }
    }
}
