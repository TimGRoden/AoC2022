using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Schema;

namespace Day15
{
    internal class Day15
    {
        static bool noOverlap(List<int[]> ranges)
        {
            for (int i = 0; i < ranges.Count; i++)
            {
                for (int j = i + 1; j < ranges.Count; j++)
                {
                    int r1a = ranges[i][0], r1b = ranges[i][1];
                    int r2a = ranges[j][0], r2b = ranges[j][1];
                    if (within(r2a, ranges[i]) || within(r2b, ranges[i]) || within(r1a, ranges[j]) || within(r1b, ranges[j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        static bool within(int a, int[] range)
        {
            return a >= range[0] && a <= range[1];
        }
        static bool anyOverlap(int[] range1, int[] range2)
        {
            return within(range1[0], range2) || within(range1[1], range2) || within(range2[0], range1) || within(range2[1], range1);
        }
        static List<int[]> reduceRanges(List<int[]> ranges)
        {
            //Console.WriteLine("Recieved new list:");
            //PrintList(ranges);
            if (noOverlap(ranges)) return ranges; //All done.
            List<int[]> shorter = new List<int[]>();
            for (int i=0;i<ranges.Count;i++)
            {
                int[] thisRange = ranges[i];
                bool overlaps = false;
                for (int j = 0; j < shorter.Count; j++) 
                { //Check for overlaps in shorter.
                    if (!anyOverlap(thisRange, shorter[j])) continue; //Doesn't overlap.
                    overlaps = true;
                    shorter[j] = new int[] { Math.Min(thisRange[0], shorter[j][0]), Math.Max(thisRange[1], shorter[j][1]) };
                    break; //Find one overlap, solve. Rince and repeat later.
                }
                if (!overlaps) shorter.Add(thisRange); //No overlaps, must be new.
            }
            return reduceRanges(shorter);
        }
        static void PrintList(List<int[]> list)
        {
            foreach (int[] item in list)
            {
                Console.WriteLine($"[{item[0]}, {item[1]}]");
            }
        }
        static void DoPart1(string[] dataIn, int row)
        {
            List<Sensor> sensors = new List<Sensor>();
            List<int[]> ranges = new List<int[]>();
            List<int[]> beacons = new List<int[]>();
            List<int> nopes = new List<int>();
            foreach (string line in dataIn)
            {
                Sensor sensor = new Sensor(line);
                sensors.Add(sensor);
                int[] range = sensor.crossOver(row);
                if (range[0] < range[1]) ranges.Add(range);
                beacons.Add(new int[] { sensor.bx, sensor.by });
                if (sensor.y == row && !nopes.Contains(sensor.x)) nopes.Add(sensor.x);
                if (sensor.by == row && !nopes.Contains(sensor.bx)) nopes.Add(sensor.bx);
            }
            int covered = totalRange(ranges);
            int beaconing = nopes.Count;
            Console.WriteLine($"I've detected {covered} places in range.");
            Console.WriteLine($"{beaconing} beacons are on row {row}.");
            Console.WriteLine($"Total non-beacons are {covered - beaconing}.");
        }
        static int totalRange(List<int[]> ranges)
        {
            int total = 0;
            List<int[]> compRanges = reduceRanges(ranges);
            foreach (int[] range in compRanges)
            {
                total += range[1] - range[0] + 1; //1:1 should still be 1 point. 3:5 is 3 points.
            }
            //if (compRanges.Count == 1) { Console.WriteLine($"Single range detected. {compRanges[0][0]}, {compRanges[0][1]}."); }
            return total;
        }
        static bool fullContain(List<int[]> ranges,int maxVal)
        {
            foreach (int[] range in ranges)
            { //There's a single range that contains everything.
                if (within(0, range) && within(maxVal, range)) return true;
            }
            return false;
        }
        static int[] shorter(int[] range, int maxVal)
        {
            int[] newRange = new int[2];
            newRange[0] = range[0];
            newRange[1] = range[1];
            if (within(0, range)) newRange[0] = 0;
            if (within(maxVal, range)) newRange[1] = maxVal;
            //Console.WriteLine($"Shortened {range[0]},{range[1]} into {newRange[0]},{newRange[1]}");
            return newRange;
        }
        static ulong findBreak(List<int[]> ranges)
        {
            List<int> maxes = new List<int>();
            foreach (int[] range in ranges)
            {
                maxes.Add(range.Max());
            }
            return (ulong)maxes.Min() + 1;
        }
        static ulong DoPart2(string[] dataIn, int maxVal)
        {
            ulong score = 0;
            List<Sensor> sensors = new List<Sensor>();
            foreach (string line in dataIn)
            {
                Sensor sensor = new Sensor(line);
                sensors.Add(sensor);
            }
            for (int row = 0;row < maxVal; row++)
            {
                List<int[]> crosses = new List<int[]>();
                foreach (Sensor sensor in sensors)
                { //Store all crossovers.
                    int[] crossover = sensor.crossOver(row);
                    if (crossover[0] < crossover[1])
                    {
                        crosses.Add(shorter(crossover, maxVal));
                    }
                }
                //Console.WriteLine($"Crossovers on row {row}");
                //PrintList(reduceRanges(crosses));
                if (fullContain(reduceRanges(crosses),maxVal)) continue; //Complete row.
                //Console.WriteLine("Found suspected break.");
                //PrintList(reduceRanges(crosses));
                ulong x = findBreak(reduceRanges(crosses));
                score = 4000000 * x + (ulong) row;
                Console.WriteLine($"Beacon at {x}, {row}. Score: {score}");
                break;
            }
            return score;
        }
        public static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            DoPart1(dataIn, 2000000);
            DoPart2(dataIn, 4000000);
            Console.ReadKey();
        }
    }
}
