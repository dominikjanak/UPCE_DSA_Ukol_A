using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTree.Tests
{
    [TestClass()]
    public class RangeTreeTests
    {
        private RangeTree<RangeTreeData> InitRangeTree()
        {
            var tree = new RangeTree<RangeTreeData>();
            var data = new List<RangeTreeData>()
            {
                new RangeTreeData(47, 35),
                new RangeTreeData(42, 65),
                new RangeTreeData(28, 50),
                new RangeTreeData(33, 63),
                new RangeTreeData(5, 5),
                new RangeTreeData(38, 52),
                new RangeTreeData(22, 60),
                new RangeTreeData(15, 45),
            };
            tree.Build(data);

            return tree;
        }

        [TestMethod()]
        public void FindTest()
        {
            var tree = InitRangeTree();

            List<(int X, int Y, bool valid)> data = new List<(int X, int Y, bool valid)>
            {
                (47, 35, true),
                (42, 65, true),
                (28, 50, true),
                (33, 63, true),
                (5, 5, true),
                (38, 52, true),
                (22, 60, true),
                (15, 45, true),
                (25, 17, false),
                (33, 50, false),
                (38, 51, false),
                (38, 53, false)
            };

            RangeTreeData item = null;
            foreach (var i in data)
            {
                item = tree.Find(i.X, i.Y);
                if (i.valid)
                {
                    Assert.IsNotNull(item, "Expected Item but get Null! Searched params: [" + i.X + ";" + i.Y + "]");
                    Assert.AreEqual(i.X, item.X, 0, "Expected " + i.X + " but get " + item.X + "!");
                    Assert.AreEqual(i.Y, item.Y, 0, "Expected " + i.Y + " but get " + item.Y + "!");
                }
                else
                {
                    Assert.IsNull(item, "Expected Null but get item! Searched params: [" + i.X + ";" + i.Y + "]");
                }
            }

        }
    }
}