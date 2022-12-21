using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day21
{
    internal class Day21
    {
        static Dictionary<string, string[]> monkeys;
        static string getMonkey(string monkey, int n)
        {
            return monkeys[monkey][n];
        }
        static double MonkeyCall(string monkey, bool p2)
        {
            if (monkeys[monkey].Length == 1)
            {
                //Console.WriteLine($"{monkey}: {monkeys[monkey][0]}");
                return double.Parse(monkeys[monkey][0]);
            }
            if (p2 && monkey == "root")
            {
                return MonkeyCall(getMonkey(monkey, 0), p2) - MonkeyCall(getMonkey(monkey, 2), p2);
            }
            double sol = 0;
            switch (monkeys[monkey][1])
            {
                case "+":
                    sol =  MonkeyCall(getMonkey(monkey, 0), p2) + MonkeyCall(getMonkey(monkey, 2), p2);
                    break;
                case "-":
                    sol =  MonkeyCall(getMonkey(monkey, 0), p2) - MonkeyCall(getMonkey(monkey, 2), p2);
                    break;
                case "*":
                    sol = MonkeyCall(getMonkey(monkey, 0), p2) * MonkeyCall(getMonkey(monkey, 2), p2);
                    break;
                default:
                    sol = MonkeyCall(getMonkey(monkey, 0), p2) / MonkeyCall(getMonkey(monkey, 2), p2);
                    break;
            }
            //Console.WriteLine($"{monkey}: {sol}");
            return sol;
        }
        static void DoPart2()
        {
            int humn = -1000;
            while (true)
            {
                monkeys["humn"] = new string[] { humn.ToString() };
                if (humn%10000==0) Console.WriteLine($"Reached {humn}");
                if (humn==int.MaxValue || MonkeyCall("root", true) == 0) break;
                humn++;
            }
            if (humn != int.MaxValue) Console.WriteLine($"Root passed when I called {monkeys["humn"][0]}");
            else Console.WriteLine("Failed. Reached int max value");
        }
        static void DoPart1(string[] dataIn)
        {
            monkeys = new Dictionary<string, string[]>();
            foreach (string line in dataIn)
            {
                string name = line.Split(':')[0];
                monkeys.Add(name, line.Split(' ').Skip(1).ToArray());
            }
            double sol1 = MonkeyCall("root", false);
            Console.WriteLine($"root: {sol1}");
        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            DoPart1(dataIn);
            DoPart2();
            Console.ReadKey();
        }
    }
}
