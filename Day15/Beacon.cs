using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    internal class Sensor
    {
        public int x, y, dist, bx, by;
        public Sensor(string line)
        {
            //Sensor at x=2, y=18: closest beacon is at x=-2, y=15
            string[] halves = line.Split(':');
            //["Sensor at x=2, y=18", " closest beacon is at x=-2, y=15]
            x = int.Parse(halves[0].Split(',')[0].Split('=')[1]);
            y = int.Parse(halves[0].Split('=')[2]);
            bx = int.Parse(halves[1].Split(',')[0].Split('=')[1]);
            by = int.Parse(halves[1].Split('=')[2]);
            dist = Math.Abs(x - bx) + Math.Abs(y - by);
        }

        public int[] crossOver(int line)
        {
            int[] cross = new int[] { 0, -1 }; //"fail condition"
            if ((line < y && y - dist > line) || (line > y && y + dist < line))
            {
                return cross; //line is too low, or too high.
            }
            cross[0] = x - (dist - Math.Abs(y - line));
            cross[1] = x + (dist - Math.Abs(y - line));
            return cross;
        }

    }
}
