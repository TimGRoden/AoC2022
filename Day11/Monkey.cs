using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    internal class Monkey
    {
        public Queue<ulong> items;
        public int MonkA, MonkB, divisor, inspects;
        public string[] Operation;
        public Monkey(string[] data)
        {
            inspects = 0;
            items = new Queue<ulong>();
            foreach (string itemstr in data[0].Split(':')[1].Split(',')) items.Enqueue(ulong.Parse(itemstr.Trim()));
            string[] TempOp = data[1].Split('=')[1].Split(' ');
            Operation = new string[] { TempOp[1].Trim(), TempOp[2].Trim(), TempOp[3].Trim() };
            divisor = int.Parse(data[2].Split(' ')[5]);
            MonkA = int.Parse(data[3].Split('y')[1].Trim());
            MonkB = int.Parse(data[4].Split('y')[1].Trim());
        }
        public ulong[] passItem()
        {
            ulong[] pass = new ulong[2];
            ulong worry = items.Dequeue();
            if (Operation[2][0] == 'o')
            {
                if (Operation[1] == "*") worry *= worry;
                else worry += worry;
            }
            else
            {
                if (Operation[1] == "*") worry *= ulong.Parse(Operation[2]);
                else worry += ulong.Parse(Operation[2]);
            }
            pass[0] = worry / 3;
            if (pass[0] % (ulong)divisor == 0) pass[1] = (ulong)MonkA;
            else pass[1] = (ulong)MonkB;
            inspects++;
            return pass;
        }

        public ulong[] passItem2()
        {
            ulong[] pass = new ulong[2];
            ulong worry = items.Dequeue();
            if (Operation[2][0] == 'o')
            {
                if (Operation[1] == "*") pass[0] = worry * worry;
                else pass[0] = worry + worry;
            }
            else
            {
                if (Operation[1] == "*") pass[0] = worry * ulong.Parse(Operation[2]);
                else pass[0] = worry + ulong.Parse(Operation[2]);
            }
            if (pass[0] % (ulong)divisor == 0) pass[1] = (ulong)MonkA;
            else pass[1] = (ulong)MonkB;
            inspects++;
            return pass;
        }
    }
}
