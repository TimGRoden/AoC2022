using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day13
{
    internal class Day13
    {
        static void DoPart1(string[] dataIn)
        {
            List<Signal> signals = new List<Signal>();
            for (int i = 0; i < dataIn.Length; i++)
            { //Identify each signal one at a time.
                if (dataIn[i] == "") signals.Add(new Signal(dataIn[i - 2], dataIn[i - 1]));
                if (i == dataIn.Length - 1) signals.Add(new Signal(dataIn[i - 1], dataIn[i]));
            }
            int orderedSum = 0;
            for (int i = 0; i < signals.Count; i++)
            {

                if (signals[i].ordered) { orderedSum += i + 1; }
            }
            Console.WriteLine($"Total sum: {orderedSum}");
        }
        static void DoPart2(string[] dataIn)
        {
            string divider1 = "[[2]]", divider2 = "[[6]]";
            List<Signal> reg0 = new List<Signal>();
            List<Signal> reg1 = new List<Signal>();
            foreach (string line in dataIn)
            {   if (line == "") continue;
                Signal sig1 = new Signal(line, divider1);
                Signal sig2 = new Signal(line, divider2);
                if (sig1.ordered)
                { //Before [[2]]
                    reg0.Add(sig1);
                } else if (sig2.ordered)
                { //After [[2]], but Before [[6]]
                    reg1.Add(sig2);
                } //If afterwards, don't bother.
            }
            int ind1 = reg0.Count + 1, ind2 = reg0.Count + reg1.Count + 2;
            Console.WriteLine($"[[2]] is at index {ind1}.");
            Console.WriteLine($"[[6]] is at index {ind2}.");
            int sol = ind1 * ind2;
            Console.WriteLine($"Multiplying gives {sol}");
        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            DoPart1(dataIn);
            DoPart2(dataIn);

            Console.ReadKey();
        }
    }
}
