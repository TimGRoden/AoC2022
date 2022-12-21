using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day14
{
    internal class Day14
    {
        static List<Point> MakeLine(int x1, int y1, int x2, int y2)
        {
            List<Point> line = new List<Point>();
            if (x1 > x2)
            {
                for (int x = x2; x <= x1; x++) line.Add(new Point(x, y1, '#'));
            }
            else if (x1 < x2)
            {
                for (int x = x1; x <= x2; x++) line.Add(new Point(x, y1, '#'));
            }
            else if (y1 > y2)
            {
                for (int y = y2; y <= y1; y++) line.Add(new Point(x1, y, '#'));
            }
            else
            {
                for (int y = y1; y <= y2; y++) line.Add(new Point(x1, y, '#'));
            }
            return line;
        }
        static bool ListContains(List<Point> Points, Point p)
        {
            foreach (Point p2 in Points)
            {
                if (p2.x == p.x && p2.y == p.y) return true;
            }
            return false;
        }
        static bool ListContains(List<Point> Points, int x, int y)
        {
            foreach (Point p2 in Points)
            {
                if (p2.x == x && p2.y == y) return true;
            }
            return false;
        }
        static int[] SizeXY(List<Point> points)
        {
            int minX = int.MaxValue, maxX = 0, minY = int.MaxValue, maxY = 0;
            foreach (Point p in points)
            {
                if (p.x > maxX) maxX = p.x;
                if (p.x < minX) minX = p.x;
                if (p.y > maxY) maxY = p.y;
                if (p.y < minY) minY = p.y;
            }
            return new int[] { 2+maxX - minX, 2+maxY - minY , minX, minY};
        }
        static bool AddSand(ref List<Point> AllPoints, int maxY)
        {
            int start = AllPoints.Count;
            int sandX = 500, sandY = 0;
            bool falling = !ListContains(AllPoints,sandX,sandY);
            while (falling)
            {
                if (sandY > maxY) break; //Off screen!
                if (!ListContains(AllPoints, sandX, sandY + 1)) { sandY++; continue; } //Free air!
                if (ListContains(AllPoints, sandX - 1, sandY + 1)) { sandX--; sandY++; }
                else if (ListContains(AllPoints, sandX + 1, sandY + 1)) { sandX++; sandY++; }
                else { falling = false; AllPoints.Add(new Point(sandX, sandY, 'o')); }
            }
            return start < AllPoints.Count;
        }
        static void DrawPoints(List<Point> points)
        {
            int[] sizes = SizeXY(points);
            foreach (Point p in points) p.Draw(sizes[2], sizes[3]);
        }
        static int DoPart1(List<Point> points, bool visual, int maxY)
        {
            int sandCount = 0;
            bool sanding = true;
            while (sanding)
            {
                sanding = AddSand(ref points, maxY);
                if (sanding) sandCount++;
                DrawPoints(points);
            }
            return sandCount;
        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            List<Point> AllPoints = new List<Point>();
            foreach (string line in dataIn)
            {
                string[] coords = line.Split(' ');
                for (int i = 0; i < coords.Length - 1; i += 2)
                { //Start at 0, go up in 2's to find new points.
                    int x1 = int.Parse(coords[i].Split(',')[0]);
                    int y1 = int.Parse(coords[i].Split(',')[1]);
                    int x2 = int.Parse(coords[i+2].Split(',')[0]);
                    int y2 = int.Parse(coords[i + 2].Split(',')[1]);
                    foreach (Point p in MakeLine(x1, y1, x2, y2)) AllPoints.Add(p);
                }
            }
            int[] sizes = SizeXY(AllPoints);
            Console.SetWindowSize(sizes[0], sizes[1]);
            foreach (Point p in AllPoints) p.Draw(sizes[2], sizes[3]);

            int sol1 = DoPart1(AllPoints, true, sizes[1]);
            Console.SetCursorPosition(0, sizes[1] - 1);
            Console.Write($"Placed {sol1} sand.");
            Console.ReadKey();
        }
    }
}
