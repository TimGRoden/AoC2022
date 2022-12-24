using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    internal class Elf
    {
        public (int, int) pos;
        public (int, int) intent;
        public int firstChoice;
        public Elf (int x, int y)
        {
            pos = (x, y);
            intent = (int.MinValue, int.MinValue);
            firstChoice = 0; //First choice, North.
        }
        private bool[] look(bool[] locals)
        {
            bool[] dir = new bool[4];
            dir[0] = !(locals[0] || locals[1] || locals[2]); //North
            dir[1] = !(locals[5] || locals[6] || locals[7]); //South
            dir[2] = !(locals[0] || locals[3] || locals[5]); //West
            dir[3] = !(locals[2] || locals[4] || locals[7]); //East
            return dir;
        }
        public (int, int) intend(ref List<Elf> Elves)
        {
            (int x, int y) = pos;
            bool[] nearby = new bool[8];
            for (int i = 0; i < nearby.Length; i++) nearby[i] = false;
            //  0   1   2
            //  3       4
            //  5   6   7
            foreach (Elf elf in Elves)
            { //Look in all directions.
                if (elf.pos == (x - 1, y - 1)) nearby[0] = true;
                else if (elf.pos == (x, y - 1)) nearby[1] = true;
                else if (elf.pos == (x + 1, y - 1)) nearby[2] = true;
                else if (elf.pos == (x - 1, y)) nearby[3] = true;
                else if (elf.pos == (x + 1, y)) nearby[4] = true;
                else if (elf.pos == (x - 1, y + 1)) nearby[5] = true;
                else if (elf.pos == (x, y + 1)) nearby[6] = true;
                else if (elf.pos == (x + 1, y + 1)) nearby[7] = true;
            }
            bool[] possibles = look(nearby);
            if (!possibles.Contains(false) ||!possibles.Contains(true)) { intent = (int.MinValue, int.MinValue); return intent; } //No elves or No moves nearby.
            switch (firstChoice)
            { // {North, South, West, East}
                case 0: // North first.
                    if (possibles[0]) { intent = (x, y - 1); return intent; }
                    if (possibles[1]) { intent = (x, y + 1); return intent; }
                    if (possibles[2]) { intent = (x - 1, y); return intent; }
                    intent = (x + 1, y); return intent; 
                case 1: //South first.
                    if (possibles[1]) { intent = (x, y + 1); return intent; }
                    if (possibles[2]) { intent = (x - 1, y); return intent; }
                    if (possibles[3]) { intent = (x + 1, y); return intent; }
                    intent = (x, y - 1); return intent;
                case 2: //West first.
                    if (possibles[2]) { intent = (x - 1, y); return intent; }
                    if (possibles[3]) { intent = (x + 1, y); return intent; }
                    if (possibles[0]) { intent = (x, y - 1); return intent; }
                    intent = (x, y + 1); return intent;
                default: //East first.
                    if (possibles[3]) { intent = (x + 1, y); return intent; }
                    if (possibles[0]) { intent = (x, y - 1); return intent; }
                    if (possibles[1]) { intent = (x, y + 1); return intent; }
                    intent = (x - 1, y); return intent;
            }
        }
        public void move(List<(int,int)> Intentions)
        { //If I'm allowed to go, great!
            if (intent!=(int.MinValue, int.MinValue) && Intentions.Contains(intent)) pos = intent;
            firstChoice++; firstChoice = firstChoice % 4; //Next decision option.
        }
    }
}
