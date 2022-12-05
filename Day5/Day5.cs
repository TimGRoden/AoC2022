using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Collections;

namespace Day5
{
    internal class Day5
    {
        static List<Stack<char>> crates = new List<Stack<char>>();
        static List<string> stacks = new List<string>();
        static void DoMove1(int[]move)
        {
            for (int i = 0; i < move[0]; i++)
            {
                crates[move[1]].Push(crates[move[2]].Pop());
            }
        }
        static void DoMove2(int[] move)
        {
            Stack<char> CrateMover = new Stack<char>();
            for (int i = 0; i < move[0]; i++)
            {
                CrateMover.Push(crates[move[2]].Pop());
            }
            while (CrateMover.Count() > 0)
            {
                crates[move[1]].Push(CrateMover.Pop());
            }
        }
        static int[] MoveRead(string line)
        {
            string[] breakDown = line.Split(' ');
            int[] move = new int[3];
            move[0] = int.Parse(breakDown[1]);
            move[1] = int.Parse(breakDown[5])-1;
            move[2] = int.Parse(breakDown[3])-1;
            return move;
        }
        static void MakeCrates()
        {
            for (int i = 1; i < stacks[stacks.Count() - 1].Length; i += 4)
            {
                Stack<char> ts = new Stack<char>();
                for (int j = 0; j < stacks.Count() - 1; j++)
                {
                    if (stacks[j][i] != ' ') ts.Push(stacks[j][i]);
                }
                crates.Add(new Stack<char>());
                while (ts.Count() > 0)
                {
                    crates[crates.Count - 1].Push(ts.Pop());
                }
            }
        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            List<string> moves = new List<string>();
            bool stacking = true;
            foreach (string line in dataIn)
            {
                if (line == "") stacking = false;
                else if (stacking) stacks.Add(line);
                else moves.Add(line);
            }
            MakeCrates();
            
            foreach (string line in moves)
            {
                DoMove1(MoveRead(line));
            }
            Console.Write("Part 1: ");
            foreach (Stack<char> crate in crates)
            {
                Console.Write(crate.Peek());
            }
            Console.WriteLine();
            //Part 2:
            crates = new List<Stack<char>>();
            MakeCrates();
            foreach (string line in moves)
            {
                DoMove2(MoveRead(line));
            }
            Console.Write("Part 2: ");
            foreach (Stack<char> crate in crates)
            {
                Console.Write(crate.Peek());
            }
            



            Console.ReadKey();
        }
    }
}
