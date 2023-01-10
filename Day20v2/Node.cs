using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Day20v2
{
    internal class Node
    {
        public Node next, prev;
        public long val;
        public Node(long val)
        {
            this.val = val;
        }

        public void PrintNodes()
        {
            Node pointer = this;
            Console.Write('[');
            do
            {
                Console.Write(pointer.val);
                if (pointer.next != this) Console.Write(',');
                if (pointer.next != null) pointer = pointer.next;
                else break; //Just incase of a broken list.
            } while (pointer != this);
            Console.WriteLine(']');
        }
        public void Move()
        {
            if (val == 0) return; // SKIP. Not going to move.
            //Extract, then seal the gap.
            prev.next = next;
            next.prev = prev;
            if (val > 0)
            { // Going forwards.
                for (long i = 0; i < val; i++)
                { //Move forwards "val" number of times.
                    prev = prev.next;
                }
            } else
            { //Going backwards.
                for (long i=0;i>val;i--) prev = prev.prev;
            }
            next = prev.next; //Now I've got the right values.
            prev.next = this;
            next.prev = this; //Now they are looking at me too.
        }
        public void Move(int len)
        {
            if (val == 0) return; // SKIP. Not going to move.
            //Extract, then seal the gap.
            prev.next = next;
            next.prev = prev;
            //Go only a distance mod len.
            long dist = val % (len-1);
            if (dist > 0)
            { // Going forwards.
                for (long i = 0; i < dist; i++)
                { //Move forwards "val" number of times.
                    prev = prev.next;
                }
            }
            else
            { //Going backwards.
                for (long i = 0; i > dist; i--) prev = prev.prev;
            }
            next = prev.next; //Now I've got the right values.
            prev.next = this;
            next.prev = this; //Now they are looking at me too.
        }
        public Node Find(int dist)
        {
            Node pointer = this;
            for (int i = 0; i < dist; i++)
            {
                pointer = pointer.next;
            }
            return pointer;
        }
    }
}
