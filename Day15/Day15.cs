using System;
using System.Collections.Generic;
using System.IO;
namespace Day15
{
    internal class Day15
    {
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
            List<int> points = new List<int>();
            foreach (int[] range in ranges)
            {
                for (int i = range[0]; i <= range[1]; i++)
                {
                    if (!nopes.Contains(i) && !points.Contains(i)) points.Add(i);
                }
            }
            Console.WriteLine($"I've detected {points.Count} places a beacon cannot be.");
        }

        public static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            DoPart1(dataIn, 2000000);
            Console.ReadKey();
        }
    }
}
