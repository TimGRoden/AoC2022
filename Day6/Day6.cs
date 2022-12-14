using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day6
{
    internal class Day6
    {
        static bool Dupes(List<char> marker)
        {
            foreach (char c in marker)
            {
                int count = 0;
                foreach (char d in marker)
                {
                    if (d == c) count++;
                }
                if (count > 1) return true;
            }
            return false;
        }
        static int FindDupes(string dataIn, int n)
        {
            List<char> marker = new List<char>();
            for (int i = 0; i < n-1; i++)
            {
                marker.Add(dataIn[i]);
            }
            for (int i = n-1; i < dataIn.Length; i++)
            {
                marker.Add(dataIn[i]);
                if (!Dupes(marker))
                { //It's the key.
                    return i+1;
                }
                else
                {
                    marker.RemoveAt(0); //Prep for the next item.
                }
            }
            return -1;
        }
        static void Main(string[] Args)
        {
            string dataIn = File.ReadAllLines("input.txt")[0];
            Console.WriteLine($"Part 1: Found after {FindDupes(dataIn, 4)}");
            Console.WriteLine($"Part 2: Found after {FindDupes(dataIn, 14)}");
            Console.ReadKey();
        }
    }
}
