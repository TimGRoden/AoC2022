using System;
using System.Collections.Generic;
using System.IO;
namespace Day11
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            List<Monkey> monkeys = new List<Monkey>();
            MakeMonkeys(ref monkeys, dataIn);
            DoPart1(monkeys, 20);
            MakeMonkeys(ref monkeys, dataIn);
            DoPart2(monkeys, 10000);
            Console.ReadKey();
        }
        static void MakeMonkeys(ref List<Monkey> monkeys, string[] dataIn)
        {
            monkeys = new List<Monkey>();
            string[] monkey = new string[5];
            foreach (string line in dataIn)
            { if (line == "")
                {
                    monkeys.Add(new Monkey(monkey));
                    monkey = new string[5];
                }
                else if (line[2] == 'S') monkey[0] = line;
                else if (line[2] == 'O') monkey[1] = line;
                else if (line[2] == 'T') monkey[2] = line;
                else if (line[7] == 't') monkey[3] = line;
                else monkey[4] = line;
            }
            monkeys.Add(new Monkey(monkey));
        }
        static void DoPart1(List<Monkey> monkeys, int steps)
        {
            for (int i = 0; i < steps; i++)
            { foreach (Monkey monkey in monkeys)
                { while (monkey.items.Count != 0)
                    {
                        ulong[] pass = monkey.passItem();
                        monkeys[(int)pass[1]].items.Enqueue(pass[0]);
                    }
                }
            }
            List<int> monkeyPasses = new List<int>();
            foreach (Monkey monkey in monkeys) monkeyPasses.Add(monkey.inspects);
            int totalPassMax = MaxList(monkeyPasses);
            monkeyPasses.Remove(totalPassMax);
            totalPassMax *= MaxList(monkeyPasses);
            Console.WriteLine($"{steps} rounds, max passes: {totalPassMax}");
        }

        static void DoPart2(List<Monkey> monkeys, int steps)
        {
            int lcm = 1;
            foreach (Monkey monkey in monkeys) lcm *= monkey.divisor;
            for (int i = 0; i < steps; i++)
            { for (int j = 0; j < monkeys.Count; j++)
                { while (monkeys[j].items.Count != 0)
                    {
                        ulong[] pass = monkeys[j].passItem2();
                        monkeys[(int)pass[1]].items.Enqueue(pass[0] % (ulong)lcm);
                    }
                }
            }
            List<int> monkeyPasses = new List<int>();
            foreach (Monkey monkey in monkeys) monkeyPasses.Add(monkey.inspects);
            ulong totalPassMax = (ulong)MaxList(monkeyPasses);
            monkeyPasses.Remove((int)totalPassMax);
            totalPassMax *= (ulong)MaxList(monkeyPasses);
            Console.WriteLine($"{steps} rounds, max passes: {totalPassMax}");
        }
        static int MaxList(List<int> intList)
        {
            int max = 0;
            foreach (int item in intList) if (item > max) max = item;
            return max;
        }
    }
}
