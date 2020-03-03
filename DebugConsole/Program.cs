using RangeTree;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<RangeTreeData> data = new List<RangeTreeData>()
            {
                new RangeTreeData("K1", 5, 10),
                new RangeTreeData("K2", 4, 10),
                new RangeTreeData("K3", 8, 10),
                new RangeTreeData("K4", 3, 10),
                new RangeTreeData("K5", 9, 10)
            };

            RangeTree<RangeTreeData> tree = new RangeTree<RangeTreeData>();
            tree.Build(data);

            Console.ReadLine();
        }
    }
}
