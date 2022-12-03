using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    internal class rGroup
    {
        public Rucksack r1, r2, r3;
        private char prioItem;

        public rGroup(Rucksack[] rA)
        {
            r1 = rA[0];
            r2 = rA[1];
            r3 = rA[2];
            prioItem = FindPrio();
        }
        private char FindPrio() 
        {
            char c = ' ';
            foreach (char letter in r1.contents)
            {
                if (r2.contents.Contains(letter) && r3.contents.Contains(letter))
                {
                    c = letter; break;
                }
            }
            return c;
        }



        public int getPrio()
        {
            if (prioItem == prioItem.ToString().ToUpper().ToCharArray()[0]) return ((int)prioItem) - 38;
            else return ((int)prioItem - 96);
        }
    }
}
