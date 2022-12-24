using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Instrumentation;
using System.ComponentModel;

namespace Day22
{
    internal class Day22
    {
        enum Direct
        {
            Up, 
            Down, 
            Left, 
            Right
        }
        static Direct Turn(Direct dir, char c)
        {
            if (c == 'R')
            {
                switch (dir)
                {
                    case Direct.Up: return Direct.Right;
                    case Direct.Right: return Direct.Down;
                    case Direct.Down: return Direct.Left;
                    default: return Direct.Up;
                }
            } //Not right ==> left.
            switch (dir)
            {
                case Direct.Up: return Direct.Left;
                case Direct.Right: return Direct.Up;
                case Direct.Down: return Direct.Right;
                default: return Direct.Down;
            }
        }
        static void DoPart1(string[] dataIn)
        {
            string instr = dataIn[dataIn.Length - 1];
            string[] map = dataIn.Take(dataIn.Length - 2).ToArray();
            Direct dir = Direct.Right;
            int[] pos = { map[0].IndexOf('.'), 0 }; //First cell.
            Queue<char> inst = new Queue<char>();
            foreach (char c in instr) inst.Enqueue(c);

        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            DoPart1(dataIn);



            Console.ReadKey();
        }
    }
}
