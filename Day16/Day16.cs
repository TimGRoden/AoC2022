using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day16
{
    internal class Day16
    {
        static List<Valve> valves;
        static int valveMove(Valve currValve, int timer)
        {
            int maxOut = 0;
            //Two options: Open a valve, or move on.
            List<int> options = new List<int>();
            if (!currValve.isOpen) 
            return maxOut;
        }
        static bool AllOpen(List<Valve> valves)
        {
            foreach (Valve valve in valves) if (!valve.isOpen) return false;
            return true;
        }
        static void doPart1(ref string[] dataIn)
        {
            valves = new List<Valve>();
            //Make valves, no connections yet.
            foreach (string line in dataIn) valves.Add(new Valve(line));
            //Connect them now that the list is complete.
            foreach (Valve valve in valves) valve.ConnectValves(ref valves);
            //Find "AA" to start with.
            Valve currValve = new Valve();
            foreach (Valve v in valves) if (v.name == "AA") currValve = v;
            int sol = valveMove(currValve, 30); //Let's go!
        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            doPart1(ref dataIn);


            Console.ReadKey();
        }
    }
}
