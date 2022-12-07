using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    internal class Day7
    {
        static Directory currFolder;
        static void Main(string[] args)
        {
            string[] filesystem = File.ReadAllLines("input.txt");
            currFolder = new Directory("/");
            for (int i=1;i< filesystem.Length; i++) //Start with the first directory in place.
            { 
                switch (filesystem[i].Substring(0, 3))
                {
                    case "$ c": //Change directory now.
                        if (filesystem[i].Split(' ')[2] == "..") //Up one, find root.
                        {
                            currFolder = currFolder.root;
                        } else //Go deeper.
                        {
                            currFolder = currFolder.directories[currFolder.folderNames.IndexOf(filesystem[i].Split(' ')[2])];
                        }
                        break;
                    case "$ l": //list things.
                        Console.WriteLine("======================");
                        currFolder.PrintOut();
                        break;
                    default: //Dealing with a file.
                        currFolder.AddFile(filesystem[i]);
                        break;
                }
            }
            while (currFolder.root != null) { currFolder = currFolder.root; }//Find the root node.
            int totalCount = currFolder.countBelow(100000);
            Console.WriteLine("======================");
            Console.WriteLine($" / is: {totalCount}");
            Console.WriteLine("======================");
            int freeSpace = 70000000 - currFolder.fullSize;
            int toFree = 30000000 - freeSpace;
            List<int> directsToDelete = currFolder.listBelow(toFree);
            Console.WriteLine($"Smallest directory that would work is size {directsToDelete.Min()}.");
            Console.ReadKey();
        }
    }
}
