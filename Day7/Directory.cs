using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    internal class Directory
    {
        public Directory root;
        public string folderName;
        public List<Directory> directories;
        public List<string> folderNames, fileNames;
        public int size, fullSize;
        public List<int> fileSize;
        public Directory(string folderName)
        {
            this.folderName = folderName;
            size = 0;
            directories = new List<Directory>();
            folderNames = new List<string>();
            fileNames = new List<string>();
            fileSize = new List<int>();
        }
        public void NewFolder(string folderName)
        {
            Directory dir = new Directory(folderName);
            dir.root = this;
            directories.Add(dir);
            folderNames.Add(folderName);
        }
        public void NewFile(string fileInfo)
        {
            string[] data = fileInfo.Split(' ');
            fileNames.Add(data[1]);
            fileSize.Add(int.Parse(data[0]));
            size += int.Parse(data[0]);
        }
        public void PrintOut()
        {
            Console.WriteLine($"- {folderName} (dir)");
            foreach (Directory dir in directories)
            {
                dir.PrintOut("  ");
            }
            for (int i = 0; i < fileNames.Count; i++)
            {
                Console.WriteLine($"  - {fileNames[i]} (file, size={fileSize[i]})");
            }
        }
        public void PrintOut(string spaces)
        {
            Console.WriteLine($"{spaces}- {folderName} (dir)");
            foreach (Directory dir in directories)
            {
                dir.PrintOut(spaces + "  ");
            }
            for (int i = 0; i < fileNames.Count; i++)
            {
                Console.WriteLine($"{spaces}  - {fileNames[i]} (file, size={fileSize[i]})");
            }
        }
        public void AddFile(string instr)
        {
            if (instr[0] == 'd')
            {
                NewFolder(instr.Split(' ')[1]);
            } else
            {
                NewFile(instr);
            }
        }

        public int dirSize()
        {
            int total = size;
            foreach (Directory dir in directories)
            {
                total += dir.dirSize();
            }
            fullSize = total;
            return total;
        }
        public int countBelow(int max)
        {
            dirSize();
            int total = 0;
            foreach (Directory dir in directories)
            {
                dir.dirSize();
                total += dir.countBelow(max);
            }
            if (fullSize <= max) total += fullSize;
            return total;
        }
        public List<int> listBelow(int max)
        {
            List<int> below = new List<int>();
            if (fullSize >= max) below.Add(fullSize); //This file would work.
            foreach (Directory dir in directories)
            { //Now check the lower ones, adding their items back on.
                foreach (int result in dir.listBelow(max))
                {
                    below.Add(result);
                }
            }
            return below;
        }
    }
}
