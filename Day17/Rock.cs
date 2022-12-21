using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    internal class Rock
    {
        public char[,] shape;
        public int[] size, pos;
        public int type;
        public bool finished { get; }
        
        public Rock(int type, int height)
        {
            this.type = type;
            switch (type)
            {
                case 0:
                    shape = new char[,] { { '#', '#', '#', '#' } };
                    break;
                case 1:
                    shape = new char[,] { { '.', '#', '.' }, { '#', '#', '#' }, { '.', '#', '.' } };
                    break;
                case 2:
                    shape = new char[,] { { '.', '.', '#' }, { '.', '.', '#' }, { '#', '#', '#' } };
                    break;
                case 3:
                    shape = new char[,] { { '#' }, { '#' }, { '#' }, { '#' } };
                    break;
                default:
                    shape = new char[,] { { '#', '#' }, { '#', '#' } };
                    break;
            }
            size = new int[] { shape.GetLength(1), shape.GetLength(0) };
            pos = new int[] { 2, height + 3 };
            finished = false;
        }
        public void move(char dir, ref Dictionary<int, char[]> currTower)
        {
            if (dir == '>' && pos[0] + size[0] < 7) pos[0]++;
            else if (pos[0] > 0) pos[0]--;
            moveDown(ref currTower);
        }
        public void moveDown(ref Dictionary<int, char[]> currTower)
        {
            int topRow = currTower.Keys.Max();
            //check that each row of the rock *would* be safe to move down, then move.
            //Otherwise, set finished to true;
        }
    }
}
