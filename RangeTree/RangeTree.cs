using RangeTree.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTree
{
    public partial class RangeTree<TValue>
        where TValue : IValue
    {
        private Node _root;
        private bool _builded;

        public RangeTree()
        {
            _root = null;
            _builded = false;
        }

        public bool IsEmpty()
        {
            return _root == null;
        }

        public void Rebuild(List<TValue> data)
        {
            _builded = false;
            _root = null;
            Build(data);
        }

        /*public void Build(List<TValue> data)
        {
            if (data == null)
            {
                // TODO: message
                throw new ArgumentNullException();
            }

            if (!IsEmpty())
            {
                // TODO: message
                throw new TreeIsAleadyBuilded("");
            }

            if (data.Count == 0)
            {
                _builded = false;
                _root = null;
                return;
            }

            _root = BuildTree(data, null, true);
        }


        private Node BuildTree(List<TValue> values, NodeBlock secondDimension, bool odd)
        {
            Node item = BuildSubTree(values, null, null, odd);

            if(secondDimension != null && !item.IsLeaf())
            {
                ((NodeBlock)item).SecondTree = secondDimension;
            }

            return item;
        }

        private Node BuildSubTree(List<TValue> values, Node parent, LeafNode previous, bool odd)
        {
            //Node item = new 

            if(values.Count() >= 2)
            {
                if (odd)
                {
                    values = values.OrderBy(n => n.X).ToList();
                }
            }



            return null;
        }*/


        
        










        public void Build(List<TValue> data)
        {
            if (data == null)
            {
                // TODO: message
                throw new ArgumentNullException();
            }

            if (!IsEmpty())
            {
                // TODO: message
                throw new TreeIsAleadyBuilded("");
            }

            if (data.Count == 0)
            {
                _builded = false;
                _root = null;
                return;
            }

            var orderedX = data.OrderBy(k => k.X).ToList();
            var orderedY = data.OrderBy(k => k.Y).ToList();

            _root = Build(orderedX, orderedY, null, true);
        }

        /// <summary>
        /// Build range tree
        /// </summary>
        /// <param name="orderedX">List ordered by X</param>
        /// <param name="orderedY">List ordered by Y</param>
        /// <param name="parent">Reference to previous node (parent)</param>
        /// <param name="level">Deep in tree</param>
        /// <param name="odd">Build X == true or Y == false</param>
        /// <returns></returns>
        private Node Build(List<TValue> orderedX, List<TValue> orderedY, NodeBlock parent, bool odd, LeafNode lastLeaf = null)
        {
            Node newItem = null;
            List<TValue> values = odd ? orderedX : orderedY;

            if (values.Count > 1)
            {
                newItem = CreateNode(orderedX, orderedY, parent, odd, lastLeaf);
            }
            else
            {
                lastLeaf = CreateLeaf(odd ? orderedX[0] : orderedY[0], lastLeaf);
                newItem = lastLeaf;
            }

            if (odd == true && !newItem.IsLeaf())
            {

                ((NodeBlock)newItem).SecondTree = Build(orderedX, orderedY, null, false);
                ((NodeBlock)newItem).Parent = (NodeBlock)newItem;
            }

            return newItem;
        }

        private NodeBlock CreateNode(List<TValue> orderedX, List<TValue> orderedY, NodeBlock parent, bool odd, LeafNode lastLeaf)
        {
            List<TValue> orderedData = odd ? orderedX : orderedY;

            TValue minItem = orderedData[0];
            TValue maxItem = orderedData[orderedData.Count - 1];

            double minValue = odd ? minItem.X : minItem.Y;
            double maxValue = odd ? maxItem.X : maxItem.Y;
            double median = GetMedian(orderedData, odd);

            NodeBlock newItem = new NodeBlock(median, minValue, maxValue, parent);

            var splited = SplitList(orderedData);

            if(odd == true)
            {
                newItem.Left = Build(splited.First, orderedY, newItem, odd, lastLeaf);
                newItem.Right = Build(splited.Second, orderedY, newItem, odd, lastLeaf);
            }
            else
            {
                newItem.Left = Build(orderedX, splited.First, newItem, odd, lastLeaf);
                newItem.Right = Build(orderedX, splited.Second, newItem, odd, lastLeaf);
            }

            return newItem;
        }























        private (List<TValue> First, List<TValue> Second) SplitList(List<TValue> values)
        {
            int cnt = values.Count();
            int firstCount = cnt - (cnt / 2);

            List<TValue> first = new List<TValue>(values.Take(firstCount));
            List<TValue> second = new List<TValue>(values.Skip(firstCount));

            return (first, second);
        }

        private LeafNode CreateLeaf(TValue value, LeafNode lastLeaf = null)
        {
            return new LeafNode(value, lastLeaf);
        }

        private double GetMedian(List<TValue> values, bool odd)
        {
            int count = values.Count();
            int half = count / 2;
            double median;

            if(count % 2 == 0)
            {
                var val1 = values.ElementAt(half);
                var val2 = values.ElementAt(half-1);

                median = ((odd ? val1.X : val1.Y) + (odd ? val2.X : val2.Y)) / 2;
            }
            else
            {
                var val = values.ElementAt(half);
                median = odd ? val.X : val.Y; 
            }
            return Math.Ceiling(median);
        }

        public void Print()
        {
            PrintTree(_root, "", true, false);
        }

        private void PrintTree(Node node, String indent, bool last, bool second)
        {
            if (!node.IsLeaf())
            {
                NodeBlock n = (NodeBlock)node;

                if (second)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }                    

                Console.WriteLine(indent + "+- " + n.Key + " <" + n.Min + " ; " +n.Max + ">");

                if (second)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (n.SecondTree != null)
                    PrintTree(n.SecondTree, indent + "   ", true, true);
            }
            else
            {
                LeafNode n = (LeafNode)node;

                if (second)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                Console.WriteLine(indent + "+- [" + n.Data.X + " ; " + n.Data.Y + "]");

                if (second)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            indent += last ? "   " : "|  ";

            if (!node.IsLeaf())
            {
                NodeBlock n = (NodeBlock)node;
                if(n.Left != null)
                    PrintTree(n.Left, indent, false, second);
                if (n.Right != null)
                    PrintTree(n.Right, indent, true, second);
            }
        }
    }
}
