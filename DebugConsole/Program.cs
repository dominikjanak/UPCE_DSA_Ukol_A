using RangeTree;
using System;
using System.Collections.Generic;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /*List<RangeTreeData> data = new List<RangeTreeData>()
            {
                new RangeTreeData(50, 5),
                new RangeTreeData(50, 45),
                new RangeTreeData(50, 60),
                new RangeTreeData(50, 50),
                new RangeTreeData(50, 63),
                new RangeTreeData(50, 52),
                new RangeTreeData(50, 65),
                new RangeTreeData(50, 35)
            };*/

            List<RangeTreeData> data = new List<RangeTreeData>()
            {
                new RangeTreeData(5, 5),
                new RangeTreeData(15, 45),
                new RangeTreeData(22, 60),
                new RangeTreeData(30, 50),
                new RangeTreeData(30, 63),
                new RangeTreeData(38, 52),
                new RangeTreeData(42, 65),
                new RangeTreeData(47, 35)
            };

            RangeTree<RangeTreeData> tree = new RangeTree<RangeTreeData>();

            tree.Build(data);

            tree.Print(false, true);

            bool res = (tree.Find(5, 5) != null);
            res &= (tree.Find(15, 45) != null);
            res &= (tree.Find(22, 60) != null);
            res &= (tree.Find(30, 50) != null);
            res &= (tree.Find(30, 63) != null);
            res &= (tree.Find(38, 52) != null);
            res &= (tree.Find(42, 65) != null);
            res &= (tree.Find(47, 35) != null);

            if (res)
            {
                Console.WriteLine("All item found!");
            }
            else
            {
                Console.WriteLine("Any item not found!");
            }

            Console.ReadLine();
        }
    }
}
