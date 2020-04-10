using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;

namespace RangeTree.Tests
{
    [TestClass()]
    public class RangeTreeTests
    {
        private RangeTree<RangeTreeData> InitRangeTree()
        {
            var tree = new RangeTree<RangeTreeData>();
            List<RangeTreeData> data = new List<RangeTreeData>()
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

        [TestMethod()]
        public void RangeScanTestLeft()
        {
            var tree = InitRangeTree();

            List<RangeTreeData> expected = new List<RangeTreeData>()
            {
                new RangeTreeData(22, 60),
                new RangeTreeData(28, 50)
            };

            List<RangeTreeData> result = tree.RangeFind(new PointF(20, 50), new PointF(30, 60));

            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(ListContainSameValues(expected, result));
        }

        [TestMethod()]
        public void RangeScanTestRight()
        {
            var tree = InitRangeTree();

            List<RangeTreeData> expected = new List<RangeTreeData>()
            {
                new RangeTreeData(38, 52),
                new RangeTreeData(42, 65)
            };

            List<RangeTreeData> result = tree.RangeFind(new PointF(35, 45), new PointF(50, 70));

            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(ListContainSameValues(expected, result));
        }

        [TestMethod()]
        public void RangeScanTestThree()
        {
            var tree = InitRangeTree();

            List<RangeTreeData> expected = new List<RangeTreeData>()
            {
                new RangeTreeData(22, 60),
                new RangeTreeData(28, 50),
                new RangeTreeData(38, 52)
            };

            List<RangeTreeData> result = tree.RangeFind(new PointF(19, 61), new PointF(40, 49));

            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(ListContainSameValues(expected, result));
        }

        [TestMethod()]
        public void RangeScanTestFour()
        {
            var tree = InitRangeTree();

            List<RangeTreeData> expected = new List<RangeTreeData>()
            {
                new RangeTreeData(38, 52)
            };

            List<RangeTreeData> result = tree.RangeFind(new PointF(31, 61), new PointF(40, 49));

            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(ListContainSameValues(expected, result));
        }

        [TestMethod()]
        public void RangeScanTestFive()
        {
            var tree = InitRangeTree();

            List<RangeTreeData> expected = new List<RangeTreeData>()
            {
                new RangeTreeData(47, 35)
            };

            List<RangeTreeData> result = tree.RangeFind(new PointF(40, 43), new PointF(54, 26));

            Assert.AreEqual(expected.Count, result.Count);
            Assert.IsTrue(ListContainSameValues(expected, result));
        }

        /////////////////////////////////////////////////////////////////
        private bool ListContainSameValues(List<RangeTreeData> expected, List<RangeTreeData> actual)
        {
            if (expected.Count != actual.Count)
            {
                return false;
            }
            bool cont = false;

            foreach (var e in expected)
            {

                foreach (var a in actual)
                {

                    if (e.X == a.X && e.Y == a.Y)
                    {
                        cont = true;
                        break;
                    }
                }

                if (cont)
                {
                    cont = false;
                    continue;
                }
                return false;
            }

            return true;
        }
    }
}