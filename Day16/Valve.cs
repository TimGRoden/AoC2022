using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    internal class Valve
    {
        public string name;
        public List<Valve> valves;
        public List<string> valveNames;
        public int flowRate;
        public bool isOpen;
        public Valve(string line)
        { //EG "Valve AA has flow rate=0; tunnels lead to valves DD, II, BB"
            string[] data = line.Split(' ');
            //Now ["Valve", "AA", "has", "flow", "rate=0;", "tunnels", "lead", "to", "valves",  "DD,", "II,", "BB"]
            name = data[1];
            flowRate = int.Parse(data[4].Split('=')[1].Trim(';'));
            string[] alt = line.Split(';')[1].Trim().Split(' '); //["tunnels", "lead", "to", "valves", "DD,", "II,", "BB"]
            valveNames = new List<string>();
            valves = new List<Valve>();
            for(int i = 4;i<alt.Length;i++)
            {
                valveNames.Add(alt[i].Trim(','));
            }
            isOpen = false;
        }
        public Valve()
        {
            name = "";
            valves = new List<Valve>();
            valveNames = new List<string>();
            flowRate = 0;
            isOpen = false;
        }
        public void ConnectValves(ref List<Valve> valvesIn)
        {
            foreach (Valve v in valvesIn)
            {
                foreach (string vName in v.valveNames)
                {
                    v.valves.Add(ValveSearch(valvesIn, vName));
                }
            }
            PrintValve();
        }
        public int OpenValve(int time)
        {
            if (isOpen) return 0;
            isOpen = true;
            return time * flowRate;
        }
        public void PrintValve()
        {
            Console.WriteLine($"Valve {name}:");
            Console.WriteLine($"> Flow rate {flowRate}.");
            Console.Write($"> Connected to: ");
            for (int i = 0; i < valveNames.Count; i++)
            {
                Console.Write($"{valveNames[i]}{((i == valveNames.Count - 1) ? "." : ", ")}");
            } Console.WriteLine();
        }
        private Valve ValveSearch(List<Valve> valvesIn, string name)
        {
            foreach (Valve v in valvesIn)
            {
                if (v.name == name) return v;
            }
            return null;
        }
    }
}
