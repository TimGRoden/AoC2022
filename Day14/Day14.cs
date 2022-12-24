using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Common;

namespace Day14
{
    internal class Day14
    {
        static List<(int, int)> stable;
        static List<(int,int)> MakeLine(int x1, int y1, int x2, int y2)
        {
            List<(int,int)> line = new List<(int,int)>();
            if (x1 > x2)
            {
                for (int x = x2; x <= x1; x++) line.Add((x,y1));
            }
            else if (x1 < x2)
            {
                for (int x = x1; x <= x2; x++) line.Add((x,y1));
            }
            else if (y1 > y2)
            {
                for (int y = y2; y <= y1; y++) line.Add((x1,y));
            }
            else
            {
                for (int y = y1; y <= y2; y++) line.Add((x1,y));
            }
            return line;
        }
        static bool AddSand(int maxY, bool part2)
        {
            int orig = stable.Count;
            int sandX = 500, sandY = 0;
            bool falling = !stable.Contains((sandX, sandY)); //This will stop if the top iece is full.
            while (falling)
            {
                if (sandY == maxY)
                {
                    //Part 2. Just add it there. break regardless.
                    if (part2) stable.Add((sandX, sandY));
                    break;
                }
                if (!stable.Contains((sandX, sandY + 1))) { sandY++; continue; } //Free air.
                if (!stable.Contains((sandX-1,sandY+1))) { sandX--; sandY++;continue; } //Fall left.
                if (!stable.Contains((sandX+1,sandY+1))) { sandX++; sandY++;continue; } //Fall right.
                stable.Add((sandX, sandY)); //Cannot fall.
                falling = false;
            }
            return orig < stable.Count; //true if there's now more points.
        }
        static void PlotAllPoints()
        {
            int maxX = 0, maxY = 0, minX = int.MaxValue, minY = int.MaxValue;
            foreach ((int x, int y) in stable)
            {
                if (y>maxY) maxY = y;
                if (x>maxX) maxX = x;
                if (y<minY) minY = y;
                if (x<minX) minX = x;
            }
            if (maxY<Console.LargestWindowHeight) minY = 0; //I can display it all, go for it :D
            if (maxX < Console.LargestWindowWidth) minX = 0;
            Console.SetWindowSize(maxX - minX, maxY - minY);
            Console.CursorVisible = false;
            foreach ((int x, int y) in stable)
            {
                Console.SetCursorPosition(x-minX, y);
                Console.Write('#');
            }

        }
        static int Sandfall(bool visual, bool part2)
        {
            int sandCount = 0;
            int maxY = 0; //Find lowest point. Can ignore anything that goes there.
            foreach ((int x, int y) in stable) if (y > maxY) maxY = y;
            maxY++; //Allow to "look" one further.
            bool sandFall = true;
            while (sandFall)
            {
                sandFall = AddSand(maxY, part2);
                if (sandFall) sandCount++;
                if (visual)
                {
                    PlotAllPoints();
                    Console.ReadKey();
                }
            }
            return sandCount;
        }
        static void MakeGrid(string[] dataIn)
        {
            stable = new List<(int, int)>();
            foreach (string line in dataIn)
            {
                string[] coords = line.Split(' ');
                for (int i = 0; i < coords.Length - 1; i += 2)
                { //Start at 0, go up in 2's to find new points.
                    int x1 = int.Parse(coords[i].Split(',')[0]);
                    int y1 = int.Parse(coords[i].Split(',')[1]);
                    int x2 = int.Parse(coords[i + 2].Split(',')[0]);
                    int y2 = int.Parse(coords[i + 2].Split(',')[1]);
                    foreach ((int x, int y) in MakeLine(x1, y1, x2, y2)) stable.Add((x, y));
                }
            }
        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            MakeGrid(dataIn);

            int sol1 = Sandfall(false, false);
            Console.SetCursorPosition(0, 0);
            Console.Write($"Part 1, placed {sol1} sand.");
            MakeGrid(dataIn);
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"Part 2, placed {Sandfall(false, true)} sand.");
            Console.ReadKey();
        }
    }
}
