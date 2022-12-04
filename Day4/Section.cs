using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    internal class Section
    {
        private int[] elf1, elf2;
        public Section(string line)
        {
            string[] parts = line.Split(',');
            string[] e1 = parts[0].Split('-');
            string[] e2 = parts[1].Split('-');
            elf1 = new int[] { int.Parse(e1[0]), int.Parse(e1[1])};
            elf2 = new int[] { int.Parse(e2[0]), int.Parse(e2[1])};
        }
        public bool fullOverlap()
        {
            return (elf2[0] >= elf1[0] && elf2[0] <= elf1[1] && elf2[1] <= elf1[1] && elf2[1] >= elf1[0]) || (elf1[0] >= elf2[0] && elf1[0] <= elf2[1] && elf1[1] <= elf2[1] && elf1[1] >= elf2[0]);
        }
        public bool anyOverlap()
        {
            return (elf2[0] >= elf1[0] && elf2[0] <= elf1[0]) || (elf2[1] >= elf1[0] && elf2[1] <= elf1[1]) || (elf1[0] >= elf2[0] && elf1[0] <= elf2[0]) || (elf1[1] >= elf2[0] && elf1[1] <= elf2[1]);
        }
    }
}
