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
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            List<Signal> signals = new List<Signal>();
            for (int i = 0; i < dataIn.Length; i++)
            { //Identify each signal one at a time.
                if (dataIn[i] == "" || i == dataIn.Length - 1) signals.Add(new Signal(dataIn[i - 2], dataIn[i - 1]));
            }


            Console.ReadKey();
        }
    }
}
