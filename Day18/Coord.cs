using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    internal class Coord
    {
        public int x, y, z;
        public Coord(string line)
        {
            string[] data = line.Split(',');
            x = int.Parse(data[0]);
            y = int.Parse(data[1]);
            z = int.Parse(data[2]);
        }
        public bool Adjacent(Coord next)
        {
            if (Math.Abs(x - next.x) + Math.Abs(y - next.y) + Math.Abs(z - next.z) == 1) return true;
            return false;
        }
        public int FreeSurface(List<Coord> points)
        {
            int area = 6;
            foreach (Coord point in points)
            {
                if (Adjacent(point)) area--;
            }
            return area;
        }
    }
}
