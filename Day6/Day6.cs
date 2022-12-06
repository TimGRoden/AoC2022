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
        static void Main(string[] Args)
        {

            string dataIn = File.ReadAllLines("input.txt")[0];
            List<char> marker = new List<char>() { dataIn[0], dataIn[1], dataIn[2] };
            
            for (int i = 3; i < dataIn.Length; i++)
            {
                marker.Add(dataIn[i]);
                if (!Dupes(marker))
                { //It's the key.
                    Console.WriteLine($"Part 1: Found code {marker[0]}{marker[1]}{marker[2]}{dataIn[i]} after {i + 1} characters.");
                    break;
                }
                else
                {
                    marker.RemoveAt(0); //Prep for the next item.
                }
            }
            marker = new List<char>();
            for (int i = 0; i < 13; i++)
            {
                marker.Add(dataIn[i]);
            }
            for (int i = 3; i < dataIn.Length; i++)
            {
                marker.Add(dataIn[i]);
                if (!Dupes(marker))
                { //It's the key.
                    Console.WriteLine($"Part 2: Found code {marker[0]}{marker[1]}{marker[2]}{dataIn[i]} after {i + 1} characters.");
                    break;
                }
                else
                {
                    marker.RemoveAt(0); //Prep for the next item.
                }
            }

            Console.ReadKey();
        }
    }
}
