using RangeTree;
using System;
using System.Collections.Generic;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<RangeTreeData> data = new List<RangeTreeData>()
            {
                new RangeTreeData(5, 5),
                new RangeTreeData(15, 45),
                new RangeTreeData(22, 60),
                new RangeTreeData(28, 50),
                new RangeTreeData(33, 63),
                new RangeTreeData(38, 52),
                new RangeTreeData(42, 65),
                new RangeTreeData(47, 35)
            };

            RangeTree<RangeTreeData> tree = new RangeTree<RangeTreeData>();

            tree.Build(data);

            tree.Print();

            Console.ReadLine();
        }
    }
}
