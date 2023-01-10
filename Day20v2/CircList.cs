using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Day20v2
{
    internal class CircList
    {
        List<Node> nodes;
        public CircList(string[] dataIn)
        {
            nodes = new List<Node>();
            foreach (string s in dataIn)
            { //Add them all in.
                nodes.Add(new Node(long.Parse(s)));
            }
            for (int i = 0; i < nodes.Count; i++)
            {
                if (i > 0) nodes[i].prev = nodes[i - 1];
                if (i < nodes.Count - 1) nodes[i].next = nodes[i + 1];
            }
            nodes[0].prev = nodes[nodes.Count - 1];
            nodes[nodes.Count - 1].next = nodes[0];
        }
        public CircList()
        {
            nodes = new List<Node>();
        }
        public void Encode(long key)
        {
            foreach (Node node in nodes) node.val *= key;
        }
        public void Shuffle()
        { //For every node, move things!
            foreach (Node node in nodes) node.Move(nodes.Count);
        }
        public void moveN(int n)
        { //Do the shuffle n times.
            for (int i = 0; i < n; i++) Shuffle();
        }
        public long finalScore()
        {
            long total = 0;
            foreach (Node node in nodes)
            {
                if (node.val != 0) continue;
                Node temp = node;
                for (int i = 0; i < 3; i++)
                { //1000 on, keep the score.
                    temp = temp.Find(1000);
                    total += temp.val;
                }
                break;
            }
            return total;
        }
        
    }
}
