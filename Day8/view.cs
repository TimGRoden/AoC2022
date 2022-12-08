using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    internal class view
    {
        public int Up, Down, Left, Right;
        public view()
        {
            Up = 0; Down = 0; Left = 0; Right = 0;
        }
        public int scenicScore()
        {
            return Up * Down * Left * Right;
        }
    }
}
