using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19v2
{
    internal class Blueprint
    {
        public int ID, ore, clay, obsOre, obsClay, geodeOre, geodeObs, maxWait;
        public int maxOreG, maxClayG, maxObsG;
        public Blueprint(string line)
        {
            string[] parts = line.Split(' ');
            ID = int.Parse(parts[1].Trim(':'));
            ore = int.Parse(parts[6]);
            clay = int.Parse(parts[12]);
            obsOre = int.Parse(parts[18]); obsClay = int.Parse(parts[21]);
            geodeOre = int.Parse(parts[27]); geodeObs = int.Parse(parts[30]);
            maxWait = Math.Max(ore, clay) + 2; //When to kill early.
            maxOreG = Math.Max(ore, Math.Max(clay, Math.Max(obsOre, geodeOre))); // Max necessary ore golems.
            maxClayG = obsClay;
            maxObsG = geodeObs;
        }
        public void PrintBP()
        {
            Console.WriteLine($"Blueprint {ID}:");
            Console.WriteLine($"An Ore Golem costs {ore} ore.");
            Console.WriteLine($"A Clay Golem costs {clay} ore.");
            Console.WriteLine($"An Obsidian Golem costs {obsOre} ore and {obsClay} clay.");
            Console.WriteLine($"A Geode Golem costs {geodeOre} ore and {geodeObs} obsidian.");
            Console.WriteLine($"MaxWait: {maxWait}, MaxGolems: [{maxOreG},{maxClayG},{maxObsG}]");
        }
    }
}
