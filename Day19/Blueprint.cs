using System;

namespace Day19
{
    internal class Blueprint
    {
        public int day, ID, oreG, ore, clayG, clay, obsG, obs, geodeG, geode, oreCost, clayCost, obsCostOre, obsCostClay, geodeCostOre, geodeCostObs, maxWait;
        public bool affordOre, affordClay, affordObs, affordGeode;
        public Blueprint previous;
        public Blueprint(string line)
        { //Blueprint 1: Each ore robot costs 3 ore. Each clay robot costs 4 ore. Each obsidian robot costs 3 ore and 19 clay. Each geode robot costs 3 ore and 8 obsidian.
            string[] parts = line.Split(' ');
            oreG = 1; clayG = 0; obsG = 0; geodeG = 0;
            ore = 0; clay = 0; obs = 0; geode = 0;
            day = 0;
            ID = int.Parse(parts[1].Trim(':'));
            oreCost = int.Parse(parts[6]);
            clayCost = int.Parse(parts[12]);
            obsCostOre = int.Parse(parts[18]); obsCostClay = int.Parse(parts[21]);
            geodeCostOre = int.Parse(parts[27]); geodeCostObs = int.Parse(parts[30]);
            affords();
            maxWait = Math.Max(oreCost, clayCost) + 2;
        }
        public Blueprint(Blueprint orig)
        {
            oreG = orig.oreG; ore = orig.ore; clayG = orig.clayG; clay = orig.clay;
            obsG = orig.obsG; obs = orig.obs; geodeG = orig.geodeG; geode = orig.geode;
            day = orig.day + 1;
            ID = orig.ID;
            oreCost = orig.oreCost; clayCost = orig.clayCost;
            obsCostOre = orig.obsCostOre; obsCostClay = orig.obsCostClay;
            geodeCostOre = orig.geodeCostOre; geodeCostObs = orig.geodeCostObs;
            maxWait = orig.maxWait;
            affords();
            previous = orig;
        }
        public void PrintFull()
        {
            if (day == 0) return; //The original state.
            previous.PrintFull();
            Console.WriteLine($"== Minute {day} ==");
            if (oreG > previous.oreG) Console.WriteLine($"Spend {oreCost} ore to build an ore golem. Now have {previous.ore - oreCost} ore.");
            else if (clayG > previous.clayG) Console.WriteLine($"Spend {clayCost} ore to build a clay golem. Now have {previous.ore - clayCost} ore.");
            else if (obsG > previous.obsG) Console.WriteLine($"Spend {obsCostOre} ore and {obsCostClay} clay to build an ore golem. Now have {previous.ore - obsCostOre} ore and {previous.clay - obsCostClay} clay.");
            else if (geodeG > previous.geodeG) Console.WriteLine($"Spend {geodeCostOre} ore and {geodeCostObs} obs to build an ore golem. Now have {previous.ore - geodeCostOre} ore and {previous.obs - geodeCostObs} obs.");
            Console.WriteLine($"{previous.oreG} ore Golems mine, now have {ore}.");
            if (previous.clayG > 0) Console.WriteLine($"{previous.clayG} clay Golems mine, now have {clay}.");
            if (previous.obsG > 0) Console.WriteLine($"{previous.oreG} obs Golems mine, now have {obs}.");
            if (previous.geodeG > 0) Console.WriteLine($"{previous.geodeG} geode Golems mine, now have {geode}.");
            if (oreG > previous.oreG) Console.WriteLine($"Finished building Ore Golem. Now have {oreG}.");
            else if (clayG > previous.clayG) Console.WriteLine($"Finished building Clay Golem. Now have {clayG}.");
            else if (obsG > previous.obsG) Console.WriteLine($"Finished building Obs Golem. Now have {obsG}.");
            else if (geodeG > previous.geodeG) Console.WriteLine($"Finished building Geode Golem. Now have {geodeG}.");
            Console.WriteLine($"End resources: Golems: [{oreG}, {clayG}, {obsG}, {geodeG}] Storage: [{ore}, {clay}, {obs}, {geode}]");
            Console.Write("Can afford: [");
            if (affordOre) { Console.Write("1, "); } else { Console.Write("0, "); }
            if (affordClay) { Console.Write("1, "); } else { Console.Write("0, "); }
            if (affordObs) { Console.Write("1, "); } else { Console.Write("0, "); }
            if (affordGeode) { Console.Write("1]"); } else { Console.Write("0]"); }
            Console.WriteLine();
            Console.WriteLine();
        }
        public void affords()
        {
            affordOre = (ore >= oreCost);
            affordClay = (ore >= clayCost);
            affordObs = ((ore >= obsCostOre ) && (clay >= obsCostClay ));
            affordGeode = ((ore >= geodeCostOre) && (obs >= geodeCostObs));
        }
        public void mineStuff()
        { //Increase each resource by the number of that golem you have.
            ore += oreG;
            clay += clayG;
            obs += obsG;
            geode += geodeG;
        }
        public void Print()
        {
            Console.WriteLine($"Blueprint {ID}, Day {day}:");
            Console.WriteLine($"Ore: {oreG} golems, {ore} ore.");
            Console.WriteLine($"Clay: {clayG} golems, {clay} clay");
            Console.WriteLine($"Obs: {obsG} golems, {obs} obsidian");
            Console.WriteLine($"Geodes: {geodeG} golems, {geode} geodes");
        }
        public void CheckPrint()
        {
            Console.WriteLine($"Blueprint {ID}:");
            Console.WriteLine($"Ore golems cost {oreCost} ore.");
            Console.WriteLine($"Clay golems cost {clayCost} ore.");
            Console.WriteLine($"Obsidian golems cost {obsCostOre} ore and {obsCostClay} clay.");
            Console.WriteLine($"Geode golems cost {geodeCostObs} obs and {geodeCostOre} ore.");

        }
        public void buy(int opt)
        {
            mineStuff();
            switch (opt)
            {
                case 0: //buy ore golem
                    oreG++;
                    ore -= oreCost;
                    break;
                case 1: //buy clay golem
                    clayG++;
                    ore -= clayCost;
                    break;
                case 2: //buy obs golem
                    obsG++;
                    ore -= obsCostOre;
                    clay -= obsCostClay;
                    break;
                default: //buy geode golem
                    geodeG++;
                    ore -= geodeCostOre;
                    obs -= geodeCostObs;
                    break;
            }
            affords(); //For display purposes, re-calculate affording.
        }
    }
}
