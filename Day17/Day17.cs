using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Day17
{
    internal class Day17
    {
        static ulong rockCounter;
        static List<(int, int)> tower;
        
        static void Part1(string dataIn, bool visible, ulong rockCount)
        {
            int i = 0;
            int iMod = dataIn.Length;
            tower = new List<(int,int)>();
            rockCounter = 0;
            int type = 0;
            Rock currRock = new Rock(type, 0);
            type = (type + 1) % 5;
            if (visible) { DrawTower(tower, currRock); Console.ReadKey(); }
            while (rockCounter < rockCount)
            {
                while (!currRock.finished)
                {
                    currRock.move(dataIn[i], tower);
                    if (visible) { DrawTower(tower, currRock); Console.ReadKey(); }
                    i = (i + 1) % iMod;
                }
                rockCounter++;
                int maxY = 0;
                foreach ((_, int y) in tower) if (y > maxY) maxY = y;
                currRock = new Rock(type, maxY);
                type = (type + 1) % 5;
            }
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Tower completed. Storing {rockCounter} rocks.");
            int height = 0;
            foreach ((_, int y) in tower) if (y > height) height = y;
            Console.WriteLine($"Total height: {height}.");
        }
        static void DrawTower(List<(int,int)> tower, Rock rock)
        {
            int maxY = 0;
            foreach ((_, int y) in tower) if (y > maxY) maxY = y;
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < maxY+8; i++) Console.WriteLine("|.......|");
            Console.WriteLine("+-------+");
            foreach ((int x, int y) in tower)
            {
                Console.SetCursorPosition(x + 1, maxY + 8 - y);
                Console.Write('#');
            }
            foreach ((int x, int y) in rock.shape)
            {
                Console.SetCursorPosition(x + 1, maxY + 8 - y);
                Console.Write('@');
            }
            Console.SetCursorPosition(9, 0);
            Console.Write($"Total rocks: {rockCounter}");
            Console.SetCursorPosition(9, 1);
            Console.Write($"Total Height: {maxY}.");
        }
        static void Main(string[] args)
        {
            string dataIn = File.ReadAllLines("input.txt")[0];
            Part1(dataIn, false, 2022);
            Console.ReadKey();
            Part1(dataIn, false, 1000000000000);


            Console.ReadKey();
        }
    }
}
