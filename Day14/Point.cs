using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    internal class Point
    {
        public int x, y;
        public char c;
        public Point (string coord)
        {
            x = int.Parse(coord.Split(',')[0]);
            y = int.Parse(coord.Split(',')[1]);
            c = '#';
        }
        public Point (int x, int y, char c)
        {
            this.x = x; this.y = y; this.c = c;
        }
        public void Draw(int xMin, int yMin)
        {
            Console.SetCursorPosition(x-xMin, y-yMin);
            Console.Write(c);
        }
    }
}
