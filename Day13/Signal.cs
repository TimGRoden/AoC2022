using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    internal class Signal
    {
        public string sig1, sig2;
        public bool ordered;
        public Signal(string sig1, string sig2)
        {
            this.sig1 = sig1;
            this.sig2 = sig2;
            checkOrder();
        }
        private void checkOrder()
        {
            ordered = true;
        }
        private bool? Compare(string left, string right)
        {
            bool isOrder = true;
            //Outermost layer
            left = left.Substring(1,left.Length-2);
            right = right.Substring(1,right.Length-2);
            while (left.Length>0 && right.Length>0)
            {
                if (Char.IsNumber(left[0]) && Char.IsNumber(right[0]))
                {
                    int leftI = int.Parse(left.Substring(0, left.IndexOf(',')+1));
                    int rightI = int.Parse(right.Substring(0, right.IndexOf(',')+1));
                    if (leftI > rightI)
                    {
                        isOrder = false;
                        break;
                    }
                    else if (leftI < rightI) break; //order good.
                    left = nextString(left);
                    right = nextString(right);

                } else //It's a [. Nested!
                {
                    string innerL = nextString(left);
                    string innerR = nextString(right);
                    bool? nest = Compare(innerL, innerR);
                    if (nest.HasValue) { isOrder = nest.Value; break; } //Came to a conclusion.
                    left = nextString(left);
                    right = nextString(right);
                }
            }
            if (right.Length == 0) return false; //Right ran out of items.
            return isOrder;
        }
        private string nextString(string inpy)
        {
            if (!inpy.Contains('[')) return inpy.Substring(inpy.IndexOf(',') + 1);
            int start = inpy.IndexOf('['), count = 0, pos = 0;
            for (int i = start; i < inpy.Length; i++)
            {
                if (inpy[start + i] == '[') count++;
                else if (inpy[start + i] == ']') count--;
                if (count == 0)
                {
                    pos = i+1;
                    break;
                }
            }
            return inpy.Substring(start, pos);
        }
    }
}
