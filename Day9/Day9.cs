using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Management.Instrumentation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.ComponentModel;

namespace Day9
{
    internal class Day9
    {
        static string[] dataIn = File.ReadAllLines("input.txt");
        static int[] head = { 0, 0 };
        static int[] tail = { 0, 0 };
        static List<int[]> rope;
        static Dictionary<int, List<int>> Visited;
        static int getMove(int head, int tail)
        {
            if (head - tail > 0) return 1;
            else return -1;
        }
        static void UpdateRopeVisited()
        {
            if (Visited.ContainsKey(rope[rope.Count-1][0]))
            { //Been to row before
                if (!Visited[rope[rope.Count - 1][0]].Contains(rope[rope.Count - 1][1]))
                {
                    Visited[rope[rope.Count - 1][0]].Add(rope[rope.Count - 1][1]);
                }
            }
            else
            {
                List<int> val = new List<int>() { rope[rope.Count - 1][1] };
                Visited.Add(rope[rope.Count - 1][0], val);
            }
        }
        static void makeRopeMove(string instr)
        {
            int dir;
            int pm;
            int move = int.Parse(instr.Split(' ')[1]);
            switch (instr[0])
            {
                case 'R':
                    pm = 1;
                    dir = 0;
                    break;
                case 'L':
                    pm = -1;
                    dir = 0;
                    break;
                case 'U':
                    pm = 1;
                    dir = 1;
                    break;
                default:
                    pm = -1;
                    dir = 1;
                    break;
            }
            for (int i = 0; i < move; i++)
            {
                rope[0][dir] += pm;
                FixRope();
                UpdateRopeVisited();
            }
        }
        static void FixRope()
        {
            for (int i = 1; i < rope.Count; i++)
            {
                if (Math.Abs(rope[i - 1][0] - rope[i][0]) > 1 || Math.Abs(rope[i - 1][1] - rope[i][1]) > 1)
                { //It's too far away.
                    if (rope[i - 1][0] == rope[i][0])
                    { //Same row, move column
                        rope[i][1] += getMove(rope[i - 1][1], rope[i][1]);
                    }
                    else if (rope[i - 1][1] == rope[i][1])
                    { //Same column, move row
                        rope[i][0] += getMove(rope[i - 1][0], rope[i][0]);
                    }
                    else
                    { //Diagonal, move both
                        rope[i][0] += getMove(rope[i - 1][0], rope[i][0]);
                        rope[i][1] += getMove(rope[i - 1][1], rope[i][1]);
                    }
                }
            }
        }
        static void DoRope(int ropeLength)
        {
            List<int> val = new List<int>() { 0 };
            Visited = new Dictionary<int, List<int>>();
            rope = new List<int[]>();
            for (int i = 0; i < ropeLength; i++)
            {
                int[] xl = new int[] { 0, 0 };
                rope.Add(xl);
            }
            foreach (string inst in dataIn) makeRopeMove(inst);
            int visited = 0;
            foreach (int key in Visited.Keys)
            {
                foreach (int coord in Visited[key])
                {
                    visited++;
                }
            }
            Console.WriteLine($"The {ropeLength} length rope has visited {visited} places atleast once.");
        }
        static void Main(string[] args)
        {
            DoRope(2);
            DoRope(10);
            Console.ReadKey();
        }
    }
}
