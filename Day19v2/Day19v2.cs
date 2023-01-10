using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day19v2
{
    internal class Day19v2
    {
        static (State, int) SolveBluePrint(State state)
        {
            if (state.min == 24) return (state, state.geode + state.geodeG);
            //TODO: Should I stop early?
            if (state.min == state.bp.maxWait && (state.clayG + state.oreG) == 1) return (state, 0);
            if (state.affordGeode)
            {
                State next = new State(state);
                next.buy(3);
                return SolveBluePrint(next);
            }
            List<State> options =StateOptions(state);
            int[] scores = new int[options.Count];
            State[] states = new State[options.Count];
            for (int i=0;i<options.Count;i++)
            {
                (states[i], scores[i]) = SolveBluePrint(options[i]);
            }
            int pos = Array.IndexOf(scores, scores.Max());
            return (states[pos], scores[pos]);
        }
        static List<State> StateOptions(State s)
        {
            List<State> opt = new List<State>();
            if (s.affordOre)
            {
                State next = new State(s);
                next.buy(0);
                opt.Add(next);
            }
            if (s.affordClay)
            {
                State next = new State(s);
                next.buy(1);
                opt.Add(next);
            }
            if (s.affordObs)
            {
                State next = new State(s);
                next.buy(2);
                opt.Add(next);
            }
            State doNothing = new State(s);
            doNothing.mine();
            opt.Add(doNothing);
            return opt;
        }
        static void DoPart1(string[] dataIn)
        {
            List<State> states = new List<State>();
            foreach (string s in dataIn) states.Add(new State(s));
            foreach (State state in states) state.CheckBP();
            int totalQuality = 0;
            foreach (State state in states)
            {
                //blueprint.CheckPrint();
                Console.Write($"Blueprint {state.bp.ID}: ");
                (State solved, int geodes) = SolveBluePrint(state);
                int score = solved.bp.ID * geodes;
                totalQuality += score;
                Console.WriteLine($"quality level {score}");
                solved.FullPrint();
            }
            Console.WriteLine($"Total quality: {totalQuality}.");
        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            DoPart1(dataIn);
            Console.ReadKey();
        }
    }
}
