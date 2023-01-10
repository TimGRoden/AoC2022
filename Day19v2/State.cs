using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19v2
{
    internal class State
    {
        public int min, oreG, ore, clayG, clay, obsG, obs, geodeG, geode;
        public bool affordOre, affordClay, affordObs, affordGeode;
        public Blueprint bp;
        public State prev;

        public State(string line)
        {
            bp = new Blueprint(line);
            ore = 0; clay = 0; obs = 0; geode = 0;
            oreG = 1; clayG = 0; obsG = 0; geodeG = 0;
            min = 0;
            affordOre = false; affordClay = false; affordObs = false; affordGeode = false;
        }

        public State (State last)
        {
            prev = last; bp = last.bp; min = last.min + 1;
            ore = last.ore; clay = last.clay; obs = last.obs; geode = last.geode;
            oreG = last.oreG; clayG = last.clay; obsG = last.obsG; geodeG = last.geodeG;
            affordWhat();
        }
        private void affordWhat()
        {   //Can I afford a thing? Do I have the max necessary?
            affordOre = (ore >= bp.ore) && (oreG < bp.maxOreG);
            affordClay = (ore >= bp.clay) && (clayG < bp.maxClayG);
            affordObs = (ore >= bp.obsOre) && (clay >= bp.obsClay) && (obsG < bp.maxObsG);
            affordGeode = (ore >= bp.geodeOre) && (obs >= bp.geodeObs);
        }

        public void mine()
        {
            ore += oreG;
            clay += clayG;
            obs += obsG;
            geode += geodeG;
        }
        public void buy(int i)
        {
            mine();
            switch (i)
            {
                case 0: // Buy ore golem.
                    oreG++;
                    ore -= bp.ore;
                    break;
                case 1: // Buy clay golem
                    clayG++;
                    ore -= bp.clay;
                    break;
                case 2: // Buy obs golem
                    obsG++;
                    ore -= bp.obsOre;
                    clay -= bp.obsClay;
                    break;
                default: // Buy geode golem
                    geodeG++;
                    ore -= bp.geodeOre;
                    obs -= bp.geodeObs;
                    break;
            }
        }
        public void CheckBP() { bp.PrintBP(); }

        public void FullPrint()
        {
            if (min == 0) return; //No moves made, exit.
            //Print the previous state, then your own.
            prev.FullPrint();
            Console.WriteLine($"=== Minute {min} ===");
            // Did I build a new golem?
            Console.Write($"Previous stuff: [{prev.ore},{prev.clay},{prev.obs},{prev.geode}]. ");
            Console.Write("I can afford: [");
            if (affordOre) { Console.Write("1,"); } else { Console.Write("0,"); }
            if (affordClay) { Console.Write("1,"); } else { Console.Write("0,"); }
            if (affordObs) { Console.Write("1,"); } else { Console.Write("0,"); }
            if (affordGeode) { Console.WriteLine("1]"); } else { Console.WriteLine("0]"); }
            if (oreG > prev.oreG) Console.WriteLine("Building a new Ore Golem.");
            else if (clayG > prev.clayG) Console.WriteLine("Building a new Clay Golem.");
            else if (obsG > prev.obsG) Console.WriteLine("Building a new Obsidian Golem.");
            else if (geodeG > prev.geodeG) Console.WriteLine("Building a new Geode Golem.");
            // Mining?
            Console.WriteLine($"{oreG} ore golems mine. Now have {ore} ore.");
            if (clayG > 0) Console.WriteLine($"{clayG} clay golems mine. Now have {clay} clay.");
            if (obsG > 0) Console.WriteLine($"{obsG} obsidian golems mine. Now have {obs} obsidian.");
            if (geodeG > 0) Console.WriteLine($"{geodeG} geode golems mine. Now have {geode} geodes.");
            // Finish building.
            if (oreG > prev.oreG) Console.WriteLine($"Finished building a new Ore Golem. Now have {oreG}.");
            else if (clayG > prev.clayG) Console.WriteLine($"Finished building a new Clay Golem. Now have {clayG}.");
            else if (obsG > prev.obsG) Console.WriteLine($"Finished building a new Obsidian Golem. Now have {obsG}.");
            else if (geodeG > prev.geodeG) Console.WriteLine($"Finished building a new Geode Golem. Now have {geodeG}.");
            //What resoures do I have?
            Console.WriteLine($"Golems: [{oreG},{clayG},{obsG},{geodeG}]   Stuff: [{ore},{clay},{obs},{geode}]");
            Console.WriteLine();

        }

    }
}
