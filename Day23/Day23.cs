using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Day23
{
    internal class Day23
    {
        static List<Elf> Elves;
        static void PrintElves()
        {
            int maxX = 0, maxY = 0, minX = int.MaxValue, minY = int.MaxValue;
            foreach (Elf elf in Elves)
            {
                (int xE, int yE) = elf.pos;
                if (xE > maxX) maxX = xE;
                if (yE > maxY) maxY = yE;
                if (yE<minY) minY = yE;
                if (xE<minX) minX = xE;
            }
            Console.CursorVisible = false;
            for (int y = 0; y <= maxY-minY; y++)
            {
                Console.CursorTop = y;
                Console.CursorLeft = 0;
                for (int x = 0; x <= maxX-minX; x++)
                {
                    Console.Write('.');
                }
            }
            foreach (Elf elf in Elves)
            {
                (int x, int y) = elf.pos;
                Console.SetCursorPosition(x-minX, y-minY);
                Console.Write('#');
            }
        }
        static void RemoveDupeIntentions(List<(int,int)> Intentions)
        {
            Dictionary<(int, int), int> Counts = new Dictionary<(int, int), int>();
            foreach ((int,int) loc in Intentions)
            {
                if (Counts.ContainsKey(loc)) Counts[loc]++;
                else Counts[loc] = 1;
            }
            foreach ((int,int)key in Counts.Keys)
            {
                if (Counts[key] > 1)
                {
                    while (Intentions.Contains(key)) Intentions.Remove(key);
                }
            }
        }
        static void MakeElves(string[] dataIn)
        {
            Elves = new List<Elf>();
            for (int y = 0; y < dataIn.Length; y++)
            {
                for (int x = 0; x < dataIn[0].Length; x++)
                {
                    if (dataIn[y][x] == '#') Elves.Add(new Elf(x, y));
                }
            }
        }
        static void DoPart1(string[] dataIn, bool visual)
        {
            MakeElves(dataIn);
            if (visual) { PrintElves(); Console.ReadKey(); }
            for (int i = 0; i < 10; i++)
            { // Do 10 cycles.
                List<(int, int)> Intentions = new List<(int, int)>();
                foreach (Elf elf in Elves)
                {
                    Intentions.Add(elf.intend(ref Elves));
                }
                RemoveDupeIntentions(Intentions);
                foreach (Elf elf in Elves)
                {
                    elf.move(Intentions);
                }
                if (visual) { PrintElves(); Console.ReadKey(); }
            }
            int maxX = 0, maxY = 0, minX = int.MaxValue, minY = int.MaxValue;
            foreach (Elf elf in Elves)
            {
                (int xE, int yE) = elf.pos;
                if (xE > maxX) maxX = xE;
                if (yE > maxY) maxY = yE;
                if (yE < minY) minY = yE;
                if (xE < minX) minX = xE;
            }
            if (visual) Console.SetCursorPosition(0, maxY - minY + 2);
            else Console.SetCursorPosition(0, 0);
            int TotalSpaces = (maxX - minX) * (maxY - minY);
            Console.WriteLine($"X: {minX} to {maxX}");
            Console.WriteLine($"Y: {minY} to {maxY}");
            Console.WriteLine($"Total Area: {maxX-minX} by {maxY-minY} is {TotalSpaces}.");
            Console.WriteLine($"Without elves, that's {TotalSpaces - Elves.Count}.");
        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            Console.CursorSize = 1;
            DoPart1(dataIn, true);




            Console.ReadKey();
        }
    }
}
