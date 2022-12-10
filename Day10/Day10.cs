using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day10
{
    internal class Day10
    {
        static void DoPart1(string[] dataIn)
        {
            int regX = 1, cycle = 1, total = 0;
            foreach (string inst in dataIn)
            {
                switch (inst[0])
                {
                    case 'n':
                        cycle++;
                        break;
                    case 'a':
                        cycle++;
                        total += checkScore(regX, cycle);
                        regX += int.Parse(inst.Split(' ')[1]);
                        cycle++;
                        break;
                }
                total += checkScore(regX, cycle);
            }
            Console.SetCursorPosition(0, 10);
            Console.WriteLine($"Part 1: Final score: {total}.");
        }
        static bool checkLight(int regX, int pos)
        {
            return Math.Abs(regX - pos%40) <= 1;
        }
        static void DoPart2(string[] dataIn)
        {
            bool[,] grid = new bool[40, 6];
            int pos = 0, regX = 1;
            foreach (string inst in dataIn) 
            {
                grid[pos % 40, pos / 40] = checkLight(regX, pos);
                pos++;
                if (inst[0] == 'a')
                {
                    grid[pos % 40, pos / 40] = checkLight(regX, pos);
                    pos++;
                    regX += int.Parse(inst.Split(' ')[1]);
                }
            }
            drawScreen(grid);
        }
        static int checkScore(int regX, int cycle)
        {
            if ((cycle - 20) % 40 == 0) return regX * cycle;
            return 0;
        }
        static void drawScreen(bool[,] screen)
        {
            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int j = 0; j < screen.GetLength(1); j++)
                {
                    Console.SetCursorPosition(i, j);
                    if (screen[i, j]) Console.Write('█');
                    else Console.Write('.');
                }
            }
        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            DoPart1(dataIn);
            DoPart2(dataIn);
            Console.ReadKey();
        }
    }
}
