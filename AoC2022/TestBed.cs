using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AoC2022
{
    internal class TestBed
    {
        static int getVal(char c)
        {
            if (c == c.ToString().ToUpper().ToCharArray()[0]) return ((int)c) - 38;
            else return ((int)c - 96);
        }
        static char FindMatch(string comp1, string comp2)
        {
            char c = ' ';
            foreach (char letter in comp1)
            {
                if (comp2.Contains(letter)) { c = letter; break; }
            }
            return c;
        }
        static char Find3Match(string r1, string r2, string r3)
        {
            char c = ' ';
            foreach (char letter in r1)
            {
                if (r2.Contains(letter) && r3.Contains(letter))
                {
                    c = letter; break;
                }
            }
            return c;
        }
        static void Main(string[] args)
        {
            string[] bags = File.ReadAllLines("input.txt");
            int total1 = 0, total2 = 0;
            for (int i=0;i<bags.Length; i++)
            {
                string comp2 = bags[i].Substring(bags[i].Length / 2);
                string comp1 = bags[i].Substring(0, bags[i].Length / 2);
                total1 += getVal(FindMatch(comp1, comp2));
            }
            for (int i = 0; i < bags.Length; i += 3)
            {
                total2 += getVal(Find3Match(bags[i], bags[i + 1], bags[i + 2]));
            }
            Console.WriteLine($"Part 1: {total1}.");
            Console.WriteLine($"Part 2: {total2}.");


            Console.ReadKey();
        }
    }
}
