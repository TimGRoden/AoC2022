using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    internal class Tree
    {
        public int height;
        public bool visible;
        public view viewDist;
        public Tree(int height)
        {
            this.height = height;
            visible = false;
            viewDist = new view();
        }
    }
}
