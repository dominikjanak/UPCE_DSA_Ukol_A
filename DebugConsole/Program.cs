using RangeTree;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            List<RangeTreeData> data = new List<RangeTreeData>()
            {
                new RangeTreeData(5, 5), // A
                new RangeTreeData(15, 45), // B
                new RangeTreeData(22, 60),  // C
                new RangeTreeData(28, 50), // D
                new RangeTreeData(33, 63), // E
                new RangeTreeData(38, 52), // F
                new RangeTreeData(42, 65), // G
                new RangeTreeData(47, 35) // H

            };

            RangeTree<RangeTreeData> tree = new RangeTree<RangeTreeData>();

            tree.Build(data);

            PointF start = new PointF(21, 61);
            PointF stop = new PointF(40, 47);
            //PointF start = new PointF(35, 50);
            //PointF stop = new PointF(45, 70);
            List<RangeTreeData> find = tree.RangeFind(start, stop);

            if(find.Count == 0)
            {
                Console.WriteLine("NIC NENALEZENO!");
            }
            else
            {
                Console.WriteLine("Nalezeny " + (find.Count) + " body!");

                foreach (var bod in find)
                {
                    Console.WriteLine("  Bod [" + bod.X + " ; " + bod.Y + "]"); ;
                }
            }

            



            //tree.Print(false, false);



            /*bool res = (tree.Find(5, 5) != null);
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
            }*/

            Console.ReadLine();
        }
    }
}
