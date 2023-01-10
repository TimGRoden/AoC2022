using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Day19
{
    internal class Day19
    {
        static void DoPart1(string[] dataIn)
        {
            List<Blueprint> blueprints = new List<Blueprint>();
            foreach (string line in dataIn)
            {
                blueprints.Add(new Blueprint(line));
            }
            //foreach (Blueprint bp in blueprints) bp.CheckPrint();
            int totalQuality = 0;
            foreach (Blueprint blueprint in blueprints)
            {
                //blueprint.CheckPrint();
                Console.Write($"Blueprint {blueprint.ID}: ");
                (Blueprint solved, int geodes) = SolveBluePrint(blueprint);
                int score = blueprint.ID * geodes;
                totalQuality += score;
                //solved.PrintFull();
                Console.WriteLine($"quality level {score}");
            }
            Console.WriteLine($"Total quality: {totalQuality}.");
        }
        static (Blueprint,int) SolveBluePrint(Blueprint bp)
        {
            if (bp.day == 24) {
                bp.geode += bp.geodeG;
                return (bp, bp.geode); } //Final day, just mine up.
            if (bp.day == bp.maxWait && (bp.clayG + bp.oreG) == 1) return (bp,0); //Hasn't built, won't be most efficient.
            if (bp.affordGeode)
            { //If you can build a geode golem, always do that. Don't check anything else.
                Blueprint geode = new Blueprint(bp);
                geode.buy(3);
                return SolveBluePrint(geode);
            } //Otherwise, check the other options.
            List<Blueprint> options = new List<Blueprint>();
            Blueprint noBuild = new Blueprint(bp);
            noBuild.mineStuff();
            options.Add(noBuild); //always one option: do nothing. Just mine.
            if (bp.affordOre)
            {
                Blueprint next = new Blueprint(bp);
                next.buy(0);
                options.Add(next);
            }
            if (bp.affordClay)
            {
                Blueprint next = new Blueprint(bp);
                next.buy(1);
                options.Add(next);
            }
            if (bp.affordObs)
            {
                Blueprint next = new Blueprint(bp);
                next.buy(2);
                options.Add(next);
            }
            int[] potentials = new int[options.Count];
            Blueprint[] states = new Blueprint[options.Count];
            for (int i = 0; i < options.Count; i++)
            {
                (states[i],potentials[i]) = SolveBluePrint(options[i]);
            }
            int pos = Array.IndexOf(potentials, potentials.Max());

            return (states[pos],potentials[pos]);
        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            DoPart1(dataIn);



            Console.ReadKey();
        }
    }
}
