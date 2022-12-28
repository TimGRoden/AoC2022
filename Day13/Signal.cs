using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    internal class Signal
    {
        public string sig1, sig2;
        public bool ordered;
        public List<object> sigA, sigB;
        public Signal(string sig1, string sig2)
        {
            this.sig1 = sig1.Substring(1, sig1.Length - 1);
            this.sig2 = sig2.Substring(1, sig2.Length - 1);
            sigA = MakeList(this.sig1);
            sigB = MakeList(this.sig2);
            checkOrder();
        }
        private List<object> MakeList(string str)
        {
            List<object> goodList = new List<object>();
            while (str.Length > 0 && str[0] != ']')
            {
                if (char.IsNumber(str[0]))
                {
                    int pos = FindPos(str);
                    goodList.Add(int.Parse(str.Substring(0, pos)));
                    if (str[pos] == ',')
                    {
                        str = str.Substring(FindPos(str) + 1);
                    }
                    else break; //It's a close bracket. You're done.
                    
                } else if (str[0] == '[')
                {
                    goodList.Add(MakeList(str.Substring(1)));
                    int nextPos = FindNext(str);
                    if (nextPos < str.Length) str = str.Substring(FindNext(str));
                    else break;
                } else
                {
                    str = str.Substring(1);
                }
            }
            return goodList;
        }
        private int FindNext (string str)
        {
            int count = 1, pos = 1;
            while (count != 0)
            {
                if (str[pos] == '[') count++;
                else if (str[pos] == ']') count--;
                pos++;
            }
            return pos;
        }
        private int FindPos(string str)
        {
            int pos = 0;
            while (str[pos] != ',' && str[pos] != ']') pos++;
            return pos;
        }
        public void PrintSignal()
        {
            PrintList(0);
            PrintList(1);
            if (ordered) Console.WriteLine("Correct order.");
            else Console.WriteLine("Wrong order.");
        }
        public void PrintList(int num)
        {
            if (num == 0) ReadList(sigA);
            else ReadList(sigB);
            Console.WriteLine();
        }
        public void ReadList(List<object> list)
        {
            Console.Write('[');
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is int)
                {
                    Console.Write(list[i]);
                } else
                {
                    ReadList((List<object>)list[i]);
                }
                if (i != list.Count - 1) Console.Write(", ");
            }
            Console.Write(']');
        }
        private void checkOrder()
        {
            ordered = (bool) InOrder(this.sigA, this.sigB);
        }
        private bool? InOrder(List<object> sigA, List<object> sigB)
        {
            bool inOrder = sigA.Count < sigB.Count;
            for (int i=0;i<Math.Min(sigA.Count, sigB.Count); i++)
            {
                if (sigA[i] is int && sigB[i] is int)
                {
                    if ((int) sigA[i] == (int) sigB[i]) continue;
                    return (int) sigA[i] < (int) sigB[i];
                } else if (sigA[i] is List<object> && sigB[i] is List<object>)
                {
                    bool? result = InOrder((List<object>)sigA[i], (List<object>)sigB[i]);
                    if (result != null) return result;
                } else if (sigA[i] is int)
                {
                    List<object> JustVal = new List<object>();
                    JustVal.Add((int)sigA[i]);
                    bool? result = InOrder(JustVal, (List<object>)sigB[i]);
                    if (result != null) return result;
                } else
                {
                    List<object> JustVal = new List<object>();
                    JustVal.Add((int)sigB[i]);
                    bool? result = InOrder((List<object>)sigA[i], JustVal);
                    if (result != null) return result;
                }
            }
            //If you reach here, no decision has been made yet.
            if (sigA.Count == sigB.Count) return null;
            return inOrder;
        }
        
    }
}
