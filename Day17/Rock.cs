using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    internal class Rock
    {
        public (int, int) posBL;
        public List<(int, int)> shape;
        public int type;
        public bool finished;
        
        public Rock(int type, int height)
        {
            this.type = type;
            (int x, int y) = (2, height + 4);
            posBL = (x, y);
            shape = new List<(int, int)>();
            switch (type)
            {
                case 0:                         // ####
                    shape.Add(posBL);
                    shape.Add((x + 1, y));
                    shape.Add((x+2,y));
                    shape.Add((x + 3, y));
                    break;
                case 1:
                    shape.Add((x + 1, y));      //  .#.
                    shape.Add((x, y + 1));      //  ###
                    shape.Add((x + 1, y + 1));  //  .#.
                    shape.Add((x + 2, y + 1));
                    shape.Add((x + 1, y + 2));
                    break;
                case 2:                         //  ..#
                    shape.Add(posBL);           //  ..#
                    shape.Add((x + 1, y));      //  ###
                    shape.Add((x + 2, y));
                    shape.Add((x + 2, y + 1));
                    shape.Add((x + 2, y + 2));
                    break;
                case 3:
                    shape.Add(posBL);           //  #
                    shape.Add((x, y + 1));      //  #
                    shape.Add((x, y + 2));      //  #
                    shape.Add((x, y + 3));      //  #
                    break;
                default:                        // ##
                    shape.Add(posBL);           // ##
                    shape.Add((x+1,y));
                    shape.Add((x, y + 1));
                    shape.Add((x + 1, y + 1));
                    break;
            }
            finished = false;
        }
        public void move(char dir, List<(int,int)> currTower)
        {
            bool canmove = true;
            switch (dir)
            {
                case '>': //Will there be a collision with the tower, or x=7?
                    foreach ((int x, int y) in shape)
                    {
                        if (canmove&&(currTower.Contains((x + 1, y)) || x+1 > 6)) canmove = false;
                    }
                    if (canmove) moveRight();
                    break;
                default: //Going left, tower or x=-1?
                    foreach ((int x, int y) in shape)
                    {
                        if (canmove && (currTower.Contains((x - 1, y)) || x - 1 < 0)) canmove = false;
                    }
                    if (canmove) moveLeft();
                    break;
            }
            moveDown(currTower);
        }
        private void moveRight()
        {
            for (int i = 0; i < shape.Count; i++)
            {
                (int x, int y) = shape[i];
                shape[i] = (x + 1, y);
            }
        }
        private void moveLeft()
        {
            for (int i = 0; i < shape.Count; i++)
            {
                (int x, int y) = shape[i];
                shape[i] = (x - 1, y);
            }
        }
        private void CheckTower(List<(int,int)> tower)
        {
            int minY = int.MaxValue, maxY = int.MinValue;
            foreach ((_,int y) in shape)
            {
                if (y>maxY) maxY = y;
                if (y<minY) minY = y;
            }
            for (int y = minY; y <= maxY; y++)
            { // Check the tower for these completed rows.
                if (tower.Contains((0,y)) && tower.Contains((1, y)) && tower.Contains((2, y))){
                    if (tower.Contains((3, y)) && tower.Contains((4, y)) && tower.Contains((5, y)) && tower.Contains((6, y)))
                    { //Tower has this whole row! Delete all below it.
                        ClearTower(tower, y);
                    }
                }
            }
        }
        private void ClearTower(List<(int,int)> tower, int h)
        {
            List<(int, int)> toRemove = new List<(int, int)> ();
            foreach ((int x, int y) in tower) 
            {
                if (y < h) toRemove.Add((x, y));
            }
            foreach ((int, int) point in toRemove)
            {
                tower.Remove(point);
            }
        }
        public void moveDown(List<(int,int)> currTower)
        {
            foreach ((int x, int y) in shape)
            {
                if (y-1==0 || currTower.Contains((x, y - 1)))
                { //Cannot go down.
                    finished = true;
                    break;
                }
            }
            if (finished) //If done falling, add to the tower.
            {
                foreach ((int, int) point in shape)
                { //If I can't go down, it goes on the tower!
                    currTower.Add(point);
                }
                CheckTower(currTower);
            } else //Otherwise, it falls.
            {
                for (int i = 0; i < shape.Count; i++)
                {
                    (int x, int y) = shape[i];
                    shape[i] = (x, y - 1);
                }
            }
        }
    }
}
