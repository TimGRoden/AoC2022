using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day20
{
    internal class Day20
    {
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            CircList allValues = new CircList(dataIn);
            allValues.moveAll();
            Console.WriteLine($"Final score: {allValues.FinalScore()}.");



            Console.ReadKey();
        }
    }
}
