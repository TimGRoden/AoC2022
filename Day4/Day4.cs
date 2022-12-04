using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using static System.Collections.Specialized.BitVector32;

namespace Day4
{
    internal class Day4
    {
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            int[] overlaps = { 0, 0 };
            foreach (string line in dataIn)
            {
                Section section = new Section(line);
                if (section.fullOverlap()) overlaps[0]++;
                if (section.anyOverlap()) overlaps[1]++;
            }

            Console.WriteLine($"Part 1: {overlaps[0]}.");
            Console.WriteLine($"Part 2: {overlaps[1]}.");

            Console.ReadKey();
        }
    }
}
