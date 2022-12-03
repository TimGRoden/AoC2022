using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day3
{
    internal class Day3
    {
        static void Main(string[] Args) 
        {
            List<Rucksack> rucksacks = new List<Rucksack>();
            using (StreamReader sr = new StreamReader(new FileStream("input.txt", FileMode.OpenOrCreate)))
            {
                while (!sr.EndOfStream)
                {
                    rucksacks.Add(new Rucksack(sr.ReadLine()));
                }
            }
            int total = 0;
            int total2 = 0;
            Rucksack[] rG = new Rucksack[3];
            List<rGroup> groups = new List<rGroup>();
            int count = 0;
            foreach (Rucksack rucksack in rucksacks)
            {
                rG[count] = rucksack;
                count++;
                if (count == 3)
                {
                    count = 0;
                    rGroup set = new rGroup(rG);
                    groups.Add(set);
                    total2 += set.getPrio();
                }
                total += rucksack.getPrio();
            }
            Console.WriteLine($"Total Part 1 priority is {total}.");
            Console.WriteLine($"Total Pert 2 priority is {total2}.");
            

            Console.ReadKey();
        }
    }
}
