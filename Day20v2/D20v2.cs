using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day20v2
{
    internal class D20v2
    {
        static void DoPart2(string[] dataIn)
        {
            long key = 811589153;
            List<Node> circlist = new List<Node>();
            foreach (string s in dataIn)
            {
                circlist.Add(new Node(int.Parse(s)*key));
            }
            for (int i = 0; i < circlist.Count; i++)
            {
                if (i > 0) circlist[i].prev = circlist[i - 1];
                if (i < circlist.Count - 1) circlist[i].next = circlist[i + 1];
            }
            circlist[0].prev = circlist[circlist.Count - 1];
            circlist[circlist.Count - 1].next = circlist[0];
            //Console.WriteLine("Initial config:");
            //circlist[0].PrintNodes();
            int len = circlist.Count;
            for (int i = 0; i < 10; i++) { foreach (Node node in circlist) node.Move(len); }
            long s1 = 0, s2 = 0, s3 = 0;
            foreach (Node node in circlist)
            { //Find 0, then find the next items.
                if (node.val != 0) continue;
                Node node1 = node.Find(1000);
                s1 = node1.val;
                Node node2 = node1.Find(1000);
                s2 = node2.val;
                Node node3 = node2.Find(1000);
                s3 = node3.val;
            }
            Console.WriteLine($"Part 2: {s1}, {s2}, {s3} => {s1 + s2 + s3}");
        }
        static void DoPart1(string[] dataIn)
        {
            List<Node> circlist = new List<Node>();
            foreach (string s in dataIn)
            {
                circlist.Add(new Node(int.Parse(s)));
            }
            for (int i = 0; i < circlist.Count; i++)
            {
                if (i > 0) circlist[i].prev = circlist[i-1];
                if (i < circlist.Count - 1) circlist[i].next = circlist[i + 1];
            }
            circlist[0].prev = circlist[circlist.Count - 1];
            circlist[circlist.Count - 1].next = circlist[0];
            //Console.WriteLine("Initial config:");
            //circlist[0].PrintNodes();
            foreach (Node node in circlist)
            {
                //Console.WriteLine($"Moving {node.val}");
                node.Move();
                //node.PrintNodes();
                //Console.WriteLine();
            }
            long s1=0, s2=0, s3=0;
            foreach (Node node in circlist)
            { //Find 0, then find the next items.
                if (node.val != 0) continue;
                Node node1 = node.Find(1000);
                s1 = node1.val;
                Node node2 = node1.Find(1000);
                s2 = node2.val;
                Node node3 = node2.Find(1000);
                s3 = node3.val;
            }
            Console.WriteLine($"Part 1: {s1}, {s2}, {s3} => {s1 + s2 + s3}");
        }
        static void Main(string[] args)
        {
            string[] dataSafe = File.ReadAllLines("input.txt");
            DoPart1(dataSafe);
            DoPart2(dataSafe);
            Console.ReadKey();
        }
    }
}
