using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20v2
{
    internal class CircList
    {
        private int maxPos;
        private Dictionary<int, int[]> digDic;
        public CircList(string[] dataIn)
        {
            digDic = new Dictionary<int, int[]>();
            foreach (string s in dataIn) 
            { 
                digDic.Add(digDic.Count,new int[] { int.Parse(s),digDic.Count});
            }
            maxPos = digDic.Count -1;

        }

        private int mod(int i, int b)
        {
            if (i > 0) return i % b;
            return ((i % b) + b)%b;
        }

        public void moveDigit(int i)
        {
            int pos = digDic[i][1]; //Current position of that.
            int val = digDic[i][0]; //Value of piece.
            //Console.WriteLine($"Moving {val}");
            if (mod(val,maxPos) == 0) return; //Don't bother with 0's, or full loops.
            int newpos = pos + val;
            if (newpos < 0 || newpos >= maxPos) newpos = mod(newpos, maxPos);
            int dir = (newpos - pos) / Math.Abs(newpos - pos); //1 or -1;
            int min = Math.Min(newpos, pos);
            int max = Math.Max(newpos, pos);
            foreach (int k in digDic.Keys)
            {
                if ((digDic[k][1] >= min) && (digDic[k][1] <= max) && digDic[k][1] != pos)
                { //In the range, isn't the start pos.
                    digDic[k][1] -= dir;
                }
            }
            digDic[i][1] = newpos;
            //PrintOut();
            //Console.WriteLine();
        }
        public void PrintOut()
        {
            Console.Write('[');
            for (int i = 0; i < digDic.Count; i++)
            {
                foreach (int[] item in digDic.Values)
                {
                    if (item[1] == i) Console.Write(item[0]);
                }
                if (i!=digDic.Count-1) Console.Write(',');
            }
            Console.WriteLine(']');
        }
        public void moveAll()
        {
            for (int i = 0; i < digDic.Count; i++) moveDigit(i);
        }

        public int Score()
        {
            int index1 = 0, index2 = 0, index3 = 0, score1 = 0, score2 = 0, score3 = 0;
            foreach (int k in digDic.Keys)
            {
                if (digDic[k][0] == 0)
                {
                    index1 = (k + 1000) % digDic.Count;
                    index2 = (k + 2000) % digDic.Count;
                    index3 = (k + 3000) % digDic.Count;
                    break;
                }
            }
            foreach (int[] dig in digDic.Values)
            {
                if (dig[1] == index1) score1 = dig[0];
                else if (dig[1] == index2) score2 = dig[0];
                else if (dig[1] == index3) score3 = dig[0];
            }
            Console.WriteLine($"{score1}, {score2}, {score3}.");
            return score1 + score2 + score3;
        }
    }
}
