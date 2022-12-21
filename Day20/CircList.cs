using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    internal class CircList
    {
        public List<int> points;
        public CircList(string[] dataIn)
        {
            points = new List<int>();
            for (int i = 0; i < dataIn.Length; i++)
            {
                points.Add(int.Parse(dataIn[i]));
            }
        }
        public void printList()
        {
            for (int i = 0; i < points.Count; i++)
            {
                Console.Write($"{points[i]}{((i == points.Count - 1) ? "." : ", ")}");
            }
            Console.WriteLine();
        }
        public void moveNum(int n)
        {
            if (n == 0) return;
            if (n > 0)
            {
                int pos = (points.IndexOf(n) + n);
                if (pos >= points.Count - 1) pos = (pos + 1) % points.Count;
                points.Remove(n);
                points.Insert(pos, n);
            }
            else
            {
                int pos = (points.IndexOf(n) + n - 1) % points.Count;
                if (pos < 0) pos += points.Count;
                points.Remove(n);
                points.Insert(pos, n);
            }
        }
        public void moveAll()
        {
            Queue<int> moves = new Queue<int>();
            //this.printList();
            foreach (int i in points)
            {
                moves.Enqueue(i);
            }
            while (moves.Count > 0)
            {
                moveNum(moves.Dequeue());
                //this.printList();
            }
        }
        public int FinalScore()
        {
            Console.WriteLine($"1000th: {points[(points.IndexOf(0) + 1000) % points.Count]}");
            Console.WriteLine($"2000th: {points[(points.IndexOf(0) + 2000) % points.Count]}");
            Console.WriteLine($"3000th: {points[(points.IndexOf(0) + 3000) % points.Count]}");
            return points[(points.IndexOf(0) + 1000) % points.Count] + points[(points.IndexOf(0) + 2000) % points.Count] + points[(points.IndexOf(0) + 3000) % points.Count];
        }
    }
}
