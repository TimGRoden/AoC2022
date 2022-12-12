using System;
using System.Collections.Generic;
using System.IO;

namespace Day12
{
    internal class Day12
    {
        static List<Node> MakeNodes(string[] dataIn)
        {
            List<Node> Nodes = new List<Node>();
            for (int y = 0; y < dataIn.Length; y++)
            {
                for (int x = 0; x < dataIn[0].Length; x++)
                {
                    Node node = new Node(dataIn[y][x], x, y);
                    Nodes.Add(node);
                }
            }
            //Now nodes exsist, check all connections.
            for (int y = 0; y < dataIn.Length; y++)
            {
                for (int x = 0; x < dataIn[0].Length; x++)
                { //Current node stored at Nodes[x+y*dataIn[0].Length], since it's contiguous now.
                    int row = y * dataIn[0].Length; //x+row now
                    if (x > 0)
                    {
                        Nodes[x + row].addNode(Nodes[x + row - 1]); //Look left.
                    }
                    if (x < dataIn[0].Length - 1)
                    {
                        Nodes[x + row].addNode(Nodes[x + row + 1]); //Look Right
                    }
                    if (y > 0)
                    {
                        Nodes[x + row].addNode(Nodes[x + row - dataIn[0].Length]); //Look up
                    }
                    if (y < dataIn.Length - 1)
                    {
                        Nodes[x + row].addNode(Nodes[x + row + dataIn[0].Length]); //Look down.
                    }
                }
            }
            return Nodes;
        }
        static void PrintAll(List<Node> nodes, bool visited)
        {
            foreach (Node node in nodes)
            {
                if (!visited || visited && node.visited)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    node.printNode();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    node.printNode();
                }
            }
            Console.WriteLine();
        }
        static void AnalyseAll(List<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                Console.Write($"Node {(char)node.height}. Connected to: ");
                foreach (Node n in node.connected) Console.Write($"[{(char)n.height}, ({n.name})]");
                Console.WriteLine();
            }
        }
        static Node nextNode(List<Node> nodes)
        {
            Node n = new Node();
            foreach (Node n1 in nodes)
            {
                if (!n1.visited && n1.minDist <= n.minDist) n = n1;
            }
            return n;
        }
        static int FindE(List<Node> nodes, bool visual)
        {
            Node currNode = nextNode(nodes);
            //first node found, distace 0.
            while (nodes.Count > 0)
            {
                if (visual) { currNode.printNode(); System.Threading.Thread.Sleep(1); }
                //Console.WriteLine($"Checking {(char)currNode.height} ({currNode.name})");
                if (currNode.height == 'E') break; //If you're looking at E as currNode, you're done.
                foreach (Node neighbor in currNode.connected)
                { //Look at neighbors.
                    if (!neighbor.visited) //Not if they've been visited.
                    {
                        neighbor.minDist = currNode.minDist + 1; //Set a new minDist.
                        //Console.WriteLine($"  New distance for {(char)neighbor.height} ({neighbor.name}): {neighbor.minDist}");
                    }
                }
                currNode.visited = true;
                currNode = nextNode(nodes);
            }
            foreach (Node n in nodes) if (n.height == 'E') return n.minDist;
            return -1;
        }
        static int FindA(List<Node> nodes, bool visual)
        {
            Node currNode = new Node();
            foreach (Node n in nodes)
            {
                if (n.height == 'S') n.minDist = int.MaxValue;
                else if (n.height=='E') currNode = n;
            }
            currNode.minDist = 0;
            while (nodes.Count > 0)
            {
                if (visual) { currNode.printNode(); System.Threading.Thread.Sleep(1); }
                //Console.WriteLine($"Checking {(char)currNode.height} ({currNode.name})");
                if (currNode.height == 'a') break; //If you're looking at a as currNode, you're done.
                foreach (Node neighbor in currNode.from)
                { //Look at neighbors.
                    if (!neighbor.visited) //Not if they've been visited.
                    {
                        neighbor.minDist = currNode.minDist + 1; //Set a new minDist.
                        //Console.WriteLine($"  New distance for {(char)neighbor.height} ({neighbor.name}): {neighbor.minDist}");
                    }
                }
                currNode.visited = true;
                currNode = nextNode(nodes);
            }
            List<Node> allA = new List<Node>();
            foreach (Node n in nodes) if (n.height == 'a') allA.Add(n);
            int steps = int.MaxValue;
            foreach (Node n in allA) if (n.minDist < steps) steps = n.minDist;
            return steps;
        }
        static void Main(string[] args)
        {
            string[] dataIn = File.ReadAllLines("input.txt");
            List<Node> nodes = MakeNodes(dataIn);
            Console.Write("Visualise? (y/n)");
            char choice = Console.ReadKey(true).KeyChar;
            Console.Clear();
            bool visualise = false;
            if (choice == 'y') { visualise = true; Console.SetWindowSize(dataIn[0].Length + 5, dataIn.Length + 5); PrintAll(nodes, false); Console.ForegroundColor = ConsoleColor.Green; }
            int sol1 = FindE(nodes, visualise);
            if (choice == 'y') Console.SetCursorPosition(0, dataIn.Length + 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"S to E took {sol1} steps.");
            if (choice == 'y') { Console.ReadKey(); Console.Clear(); PrintAll(nodes, false); Console.ForegroundColor = ConsoleColor.Green; }
            nodes = MakeNodes(dataIn);
            int sol2 = FindA(nodes, visualise);
            if (choice == 'y') Console.SetCursorPosition(0, dataIn.Length + 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"E to a took {sol2} steps.");
            Console.ReadKey();
        }
    }
}
