using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    internal class Rucksack
    {
        public string contents;
        private string comp1, comp2;
        public char prioItem;
        
        public Rucksack(string input)
        {
            contents = input;
            int mid = input.Length / 2;
            comp1 = input.Substring(0,mid);
            comp2 = input.Substring(mid);
            prioItem = FindMatch();
        }
        private char FindMatch()
        {
            char c = ' ';
            foreach (char letter in comp1)
            {
                if (comp2.Contains(letter)) { c = letter; break; }
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
