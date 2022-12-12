using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    internal class Node
    {
        public int height, minDist;
        public List<Node> connected;
        public List<Node> from;
        public string name;
        public bool visited;
        public Node(char c, int x, int y)
        {
            connected = new List<Node>();
            height = c;
            name = $"{x},{y}";
            visited = false;
            if (c == 'S') minDist = 0;
            else minDist = int.MaxValue;
            from = new List<Node>();
        }
        public Node()
        {
            height = int.MaxValue;
            minDist = int.MaxValue;
        }
        public Node(Node n)
        {
            height = n.height;
            name = n.name;
            visited = false;
            connected = new List<Node>();
            foreach (Node node in n.connected)
            {
                connected.Add(node);
            }
            minDist = int.MaxValue;
            from = new List<Node>();
        }
        public void addNode(Node n)
        {
            if (height == 'S')
            {
                connected.Add(n);
                n.from.Add(this);
            }
            else if (!(n.height == 'E') && n.height - height <= 1)
            {
                connected.Add(n);
                n.from.Add(this);
            }
            else if (n.height == 'E' && height == 'z') 
            { 
                connected.Add(n);
                n.from.Add(this);
            }
        }
        public void printNode()
        {
            Console.SetCursorPosition(int.Parse(name.Split(',')[0]), int.Parse(name.Split(',')[1]));
            Console.Write((char)height);
        }
    }
}
