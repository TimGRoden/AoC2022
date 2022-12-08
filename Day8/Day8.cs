using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Day8
{
    internal class Day8
    {
        static void PrintGrid(Tree[,] grid,bool printAll)
        {
            for (int i=0;i<grid.GetLength(0);i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j].visible || printAll) Console.Write(grid[i, j].height);
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static int OuterVis(Tree[,] grid)
        {
            int count = 0;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (x == 0 || y == 0 || x == grid.GetLength(0) - 1 || y == grid.GetLength(1) - 1)
                    { //It's on the edge.
                        grid[x, y].visible = true;
                        count++;
                    }
                    else //Check internals now.
                    { //check all above:
                        bool aboveVis = true;
                        for (int y2 = 0; y2 < y; y2++)
                        {
                            if (grid[x, y2].height >= grid[x, y].height) aboveVis = false;
                        }
                        if (aboveVis) { grid[x, y].visible = true; count++; }
                        else //Not above visible. Check upwards
                        {
                            bool belowVis = true;
                            for (int y2 = grid.GetLength(1) - 1; y2 > y; y2--)
                            {
                                if (grid[x, y2].height >= grid[x, y].height) belowVis = false;
                            }
                            if (belowVis) { grid[x, y].visible = true; count++; }
                            else
                            { //Neither. Check left vis
                                bool leftVis = true;
                                for (int x2 = 0; x2 < x; x2++)
                                {
                                    if (grid[x2, y].height >= grid[x, y].height) leftVis = false;
                                }
                                if (leftVis) { grid[x, y].visible = true; count++; }
                                else
                                { //Not up or down or left vis. Check right
                                    bool rightVis = true;
                                    for (int x2 = grid.GetLength(0) - 1; x2 > x; x2--)
                                    {
                                        if (grid[x2, y].height >= grid[x, y].height) rightVis = false;
                                    }
                                    if (rightVis) { grid[x, y].visible = true; count++; }
                                }
                            }
                        }
                    }

                }
            }
            return count;
        }
        static int TreeVis(Tree[,] grid)
        {
            int maxVis = 0;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y=0; y < grid.GetLength(1); y++)
                {
                    //Ignore boundaries. Default is 0.
                    if (x != 0)
                    { //Look left
                        bool blocked = false;
                        int pos = 1;
                        while (!blocked && x-pos>=0) //No tree, and not off the grid.
                        {
                            grid[x, y].viewDist.Left++; //I see a tree!
                            if (grid[x, y].height <= grid[x-pos,y].height) blocked = true;
                            pos++;
                        }
                    }
                    if (y != 0)
                    { //Look up
                        bool blocked = false;
                        int pos = 1;
                        while (!blocked && y - pos >= 0) //No tree, and not off the grid.
                        {
                            grid[x, y].viewDist.Up++; //I see a tree!
                            if (grid[x, y].height <= grid[x, y-pos].height) blocked = true;
                            pos++;
                        }
                    }
                    if (x != grid.GetLength(0) - 1) 
                    { //Look down
                        bool blocked = false;
                        int pos = 1;
                        while (!blocked && x + pos < grid.GetLength(0)) //No tree, and not off the grid.
                        {
                            grid[x, y].viewDist.Down++; //I see a tree!
                            if (grid[x, y].height <= grid[x + pos, y].height) blocked = true;
                            pos++;
                        }
                    }
                    if (y!=grid.GetLength(1)-1) 
                    { //Look right
                        bool blocked = false;
                        int pos = 1;
                        while (!blocked && y + pos <grid.GetLength(1)) //No tree, and not off the grid.
                        {
                            grid[x, y].viewDist.Right++; //I see a tree!
                            if (grid[x, y].height <= grid[x , y+pos].height) blocked = true;
                            pos++;
                        }
                    }
                    //Now what's the score?
                    int viewDist = grid[x, y].viewDist.scenicScore();
                    if (maxVis<viewDist) maxVis = viewDist; //Update if necessary.
                }
            }


            return maxVis;
        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            Tree[,] grid = new Tree[dataIn[0].Length,dataIn.Length]; //Across, then down
            for (int x = 0; x < dataIn[0].Length; x++)
            {
                for (int y = 0; y < dataIn.Length; y++)
                {
                    grid[x,y] = new Tree(int.Parse(dataIn[x][y].ToString()));
                }
            }
            //Check all visibilities.
            
            Console.WriteLine($"I've found {OuterVis(grid)} trees.");
            Console.WriteLine($"Best scenic Score I've found is {TreeVis(grid)}.");

            Console.ReadKey();
        }
    }
}
