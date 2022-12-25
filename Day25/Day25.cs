using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    internal class Day25
    {
        static string IntToBase5(ulong value)
        {
            string result = "";
            Stack<ulong> values = new Stack<ulong>();
            do
            {
                values.Push(value % 5);
                value /= 5;
            } while (value > 0);
            while (values.Count > 0)
            {
                result += values.Pop().ToString();
            }
            return result;
        }
        static ulong readLine(string line)
        {
            ulong total = 0;
            Stack<char> vals = new Stack<char>();
            foreach (char c in line) vals.Push(c);
            ulong col = 1;
            while (vals.Count > 0)
            {
                switch (vals.Pop())
                {
                    case '2':
                        total += 2 * col;
                        break;
                    case '1':
                        total += col;
                        break;
                    case '0': break;
                    case '-':
                        total -= col;
                        break;
                    default:
                        total -= 2 * col;
                        break;
                }
                col *= 5;
            }
            return total;
        }
        static string writeLine(ulong value)
        {
            //Console.WriteLine($"Recieved {value}.");
            string base5 = IntToBase5(value);
            //Console.WriteLine($"In base 5: {base5}.");
            for (int i = 0; i < base5.Length; i++)
            {
                value += 2 * (ulong) Math.Pow(5, i);
            }
            char[] chars = IntToBase5(value).ToArray();
            //Console.Write("Now increased:");
            //foreach (char c in chars) Console.Write(c);
            //Console.WriteLine(".");
            char[] result = new char[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                switch (chars[i])
                {
                    case '4':
                        result[i] = '2';
                        break;
                    case '3':
                        result[i] = '1';
                        break;
                    case '2':
                        result[i] = '0';
                        break;
                    case '1':
                        result[i] = '-';
                        break;
                    default:
                        result[i] = '=';
                        break;
                }
            }
            string ans = "";
            foreach (char c in result) ans += c;
            return ans;
        }
        
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            ulong totalFuel = 0;
            foreach (string line in dataIn)
            {
                totalFuel += readLine(line);
            }
            Console.WriteLine($"Total fuel needed is {totalFuel}.");
            string sol1 = writeLine(totalFuel);
            Console.WriteLine($"In SNAFU: {sol1}.");
            Console.WriteLine($"Back in normal: {readLine(sol1)}.");
            Console.WriteLine($"{totalFuel} in Base 5 is: {IntToBase5(totalFuel)}.");
            Console.WriteLine($"Possible solution: 202201==-==101-0 is {readLine("202201==-==101-0")}.");
            Console.ReadKey();
        }
    }
}
