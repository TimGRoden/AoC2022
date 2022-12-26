using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day18
{
    internal class Day18
    {
        static int Part1(string[] dataIn)
        {
            int totalArea = 0;
            List<Coord> points = new List<Coord>();
            foreach (string line in dataIn)
            {
                points.Add(new Coord(line));
            }
            foreach (Coord point in points)
            {
                totalArea += point.FreeSurface(points);
            }
            return totalArea;
        }
        static int Part2(string[] dataIn)
        { //Attempt BFS from 0,0,0 to find all droplets in touch with air. Trying tuples for .Contains ease.
            List<(int, int, int)> drops = new List<(int, int, int)>();
            Queue<(int, int, int)> BFS = new Queue<(int, int, int)>();
            List<(int, int, int)> visited = new List<(int, int, int)>();
            List<(int, int, int)> exterior = new List<(int, int, int)>();
            int sol = 0;
            BFS.Enqueue((-1, -1, -1));
            foreach (string line in dataIn)
            {
                string[] parts = line.Split(',');
                drops.Add((int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])));
            }
            while (BFS.Count > 0)
            { 
                (int x, int y, int z) = BFS.Dequeue();
                visited.Add((x, y, z));
                if ((x + 1) < 23) //Look x+1
                {
                    if (drops.Contains((x + 1, y, z)))
                    { //Looking at a drop, Found more surface area.
                        sol++;
                    } //Otherwise, enqueue it.
                    else if (!visited.Contains((x + 1, y, z))&&!BFS.Contains((x+1,y,z))) BFS.Enqueue((x + 1, y, z));
                }
                if ((x - 1) >= -1) //Look x-1
                {
                    if (drops.Contains((x - 1, y, z)))
                    { //Looking at a drop, Found more surface area.
                        sol++;
                    } //Otherwise, enqueue it.
                    else if (!visited.Contains((x - 1, y, z)) && !BFS.Contains((x -1, y, z))) BFS.Enqueue((x - 1, y, z));
                }
                if ((y + 1) < 23) //Look y+1
                {
                    if (drops.Contains((x, y+1, z)))
                    { //Looking at a drop, Found more surface area.
                        sol++;
                    } //Otherwise, enqueue it.
                    else if (!visited.Contains((x, y + 1, z)) && !BFS.Contains((x, y+1, z))) BFS.Enqueue((x, y+1, z));
                }
                if ((y - 1) >= -1) //Look y-1
                {
                    if (drops.Contains((x , y-1, z)))
                    { //Looking at a drop, Found more surface area.
                        sol++;
                    } //Otherwise, enqueue it.
                    else if (!visited.Contains((x, y - 1, z)) && !BFS.Contains((x, y-1, z))) BFS.Enqueue((x, y - 1, z));
                }
                if ((z + 1) < 23) //Look z+1
                {
                    if (drops.Contains((x, y, z+1)))
                    { //Looking at a drop, Found more surface area.
                        sol++;
                    } //Otherwise, enqueue it.
                    else if (!visited.Contains((x, y, z+1)) && !BFS.Contains((x , y, z+1))) BFS.Enqueue((x, y, z+1));
                }
                if ((z - 1) >= -1) //Look z-1
                {
                    if (drops.Contains((x , y, z-1)))
                    { //Looking at a drop, Found more surface area.
                        sol++;
                    } //Otherwise, enqueue it.
                    else if (!visited.Contains((x, y, z - 1)) && !BFS.Contains((x , y, z-1))) BFS.Enqueue((x, y, z - 1));
                }
            }
            return sol;
        }
        static void PrintOut(List<(int,int,int)> listy)
        {
            foreach ((int x, int y, int z) in listy)
            {
                Console.WriteLine($"{x},{y},{z}");
            }
        }
        public static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            int sol1 = Part1(dataIn);
            Console.WriteLine($"Total free Surface area = {sol1}.");
            int sol2 = Part2(dataIn);
            Console.WriteLine($"Total exterior Surface area = {sol2}.");
            Console.ReadKey();
        }
    }
}
