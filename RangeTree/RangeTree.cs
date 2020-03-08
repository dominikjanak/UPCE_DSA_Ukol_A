using RangeTree.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RangeTree
{
    public partial class RangeTree<TValue>
        where TValue : IValue
    {
        private Node _root;
        private bool _builded;
        LeafNode _lastLeaf;

        public RangeTree()
        {
            _root = null;
            _builded = false;
            _lastLeaf = null;
        }

        public bool IsEmpty()
        {
            return _root == null;
        }

        public void Rebuild(List<TValue> data)
        {
            _builded = false;
            _root = null;
            _lastLeaf = null;
            Build(data);
        }   

        public void Build(List<TValue> values)
        {
            if (values == null)
            {
                // TODO: message
                throw new ArgumentNullException();
            }

            if (!IsEmpty())
            {
                // TODO: message
                throw new TreeIsAleadyBuilded("");
            }

            if (values.Count == 0)
            {
                _builded = false;
                _root = null;
                return;
            }

            _lastLeaf = null;
            _root = Build(values, null, true);
        }

        /// <summary>
        /// Build range tree
        /// </summary>
        /// <param name="values">List ordered by X</param>
        /// <param name="orderedY">List ordered by Y</param>
        /// <param name="parent">Reference to previous node (parent)</param>
        /// <param name="level">Deep in tree</param>
        /// <param name="odd">Build X == true or Y == false</param>
        /// <returns></returns>
        private Node Build(List<TValue> values, NodeBlock parent, bool odd)
        {
            Node newItem = null;

            if (values.Count > 1)
            {
                newItem = CreateNode(values, parent, odd);
            }
            else
            {
                newItem = CreateLeaf(values[0], parent, _lastLeaf);
                _lastLeaf = (LeafNode)newItem;
            }

            if (odd == true && !newItem.IsLeaf())
            {
                LeafNode leaf = _lastLeaf;
                _lastLeaf = null;
                ((NodeBlock)newItem).SecondTree = Build(values, null, false);
                if (newItem.IsLeaf())
                {
                    _lastLeaf = (LeafNode)newItem;
                }
                ((NodeBlock)newItem).Parent = (NodeBlock)newItem;
                _lastLeaf = leaf;
            }

            return newItem;
        }

        private NodeBlock CreateNode(List<TValue> values, NodeBlock parent, bool odd)
        {
            values = values.OrderBy(i => odd ? i.X : i.Y).ToList();

            TValue minItem = values[0];
            TValue maxItem = values[values.Count - 1];

            float minValue = (float)(odd ? minItem.X : minItem.Y);
            float maxValue = (float)(odd ? maxItem.X : maxItem.Y);
            float median = GetMedian(values, odd);

            NodeBlock newItem = new NodeBlock(median, minValue, maxValue, parent);

            var splited = SplitList(values);

            newItem.Left = Build(splited.First, newItem, odd);
            newItem.Right = Build(splited.Second, newItem, odd);

            return newItem;
        }

        public TValue Find(float x, float y) 
        {
            var node = FindNode(x, y);

            if(node != null && node.IsLeaf()) 
            {
                return ((LeafNode)node).Data;
            }

            return default(TValue);
        }

        /*public Node FindNode(float x, float y)
        {
            if (_root.IsLeaf())
            {
                LeafNode leaf = (LeafNode)_root;
                if(leaf.Data.X == x && leaf.Data.Y == y)
                {
                    return leaf;
                }
                return null;
            }

            NodeBlock node = (NodeBlock)_root;

            while (node != null)
            {
                // Left descendant
                if(node.Left.IsLeaf() && ((LeafNode)node.Left).Data.X == x && ((LeafNode)node.Left).Data.Y == y)
                {
                    return ((LeafNode)node.Left).Data;
                }

                // Right descendant
                if (node.Right.IsLeaf() && ((LeafNode)node.Right).Data.X == x && ((LeafNode)node.Right).Data.Y == y)
                {
                    return ((LeafNode)node.Left).Data;
                }

                // traversing a tree
                if (node.Left != null && !node.Left.IsLeaf() && ((NodeBlock)node.Left).Min <= x && ((NodeBlock)node.Left).Max > x)
                {
                    node = (NodeBlock)node.Left;
                }
                else
                {
                    if(node.Right != null && (node.Right.IsLeaf() || node.Min > x || node.Max <= x))
                    {
                        return default(TValue);
                    }
                    if (node.Right != null && !node.Right.IsLeaf())
                    {
                        node = (NodeBlock)node.Right;
                    }
                }
            }

            return default(TValue);
        }*/

        
        private Node FindNode(float x, float y)
        {
            if (_root != null && _root.IsLeaf())
            {
                LeafNode leafRoot = (LeafNode)_root; 
                if (leafRoot.Data.X == x && leafRoot.Data.Y == y)
                {
                    return leafRoot;
                }
                return null;
            }

            NodeBlock node = (NodeBlock)_root;
            LeafNode leaf = null;
            while (node != null)
            {
                if (IsNodeNotNullAndLeaf(node.Left))
                {
                    leaf = (LeafNode)node.Left;
                    Console.WriteLine("LLeaf: " + leaf.Data.X + "-" + leaf.Data.Y);
                    if (leaf.Data.X == x)
                    {
                        if (leaf.Data.Y == y)
                        {
                            return leaf;
                        }
                        else
                        {
                            return FindInLeaves(leaf, x, y);
                        }
                    }
                }

                if (IsNodeNotNullAndLeaf(node.Right))
                {
                    leaf = (LeafNode)node.Right;
                    Console.WriteLine("RLeaf: " + leaf.Data.X + "-" + leaf.Data.Y);
                    if (leaf.Data.X == x)
                    {
                        if (leaf.Data.Y == y)
                        {
                            return leaf;
                        }
                        else
                        {
                            return FindInLeaves(leaf, x, y);
                        }
                    }
                }


                if (IsNodeNotNullAndNotLeaf(node.Left) && ((NodeBlock)node.Left).From <= x && x <= ((NodeBlock)node.Left).To)
                {

                    Console.WriteLine("Node: " + ((NodeBlock)node.Left).From + "-" + ((NodeBlock)node.Left).To);
                    node = (NodeBlock)node.Left;
                    continue;
                }
                else
                {

                    if (!IsNodeNotNullAndNotLeaf(node.Right) || ((NodeBlock)node.Right).From <= x || x <= ((NodeBlock)node.Right).To)
                    {

                        return null;
                    }
                    node = (NodeBlock)node.Right;
                }
            }

            return null;
        }

        private LeafNode FindInLeaves(LeafNode leaf, float x, float y)
        {


            return null;
        }

        private bool IsNodeNotNullAndNotLeaf(Node node)
        {
            if (node == null) return false;
            if (node.IsLeaf()) return false;
            return true;
        }

        private bool IsNodeNotNullAndLeaf(Node node)
        {
            if (node == null) return false;
            if (!node.IsLeaf()) return false;
            return true;
        }




        public List<TValue> RangeFind(RectangleF rectangle)
        {


            return new List<TValue>();
        }





















        private (List<TValue> First, List<TValue> Second) SplitList(List<TValue> values)
        {
            int cnt = values.Count();
            int firstCount = cnt - (cnt / 2);

            List<TValue> first = new List<TValue>(values.Take(firstCount));
            List<TValue> second = new List<TValue>(values.Skip(firstCount));

            return (first, second);
        }

        private LeafNode CreateLeaf(TValue value, NodeBlock parent, LeafNode lastLeaf = null)
        {
            return new LeafNode(value, parent, lastLeaf);
        }

        private float GetMedian(List<TValue> values, bool odd)
        {
            int count = values.Count();
            int half = count / 2;
            float median;

            if(count % 2 == 0)
            {
                var val1 = values.ElementAt(half);
                var val2 = values.ElementAt(half-1);

                median = (float)((odd ? val1.X : val1.Y) + (odd ? val2.X : val2.Y)) / 2;
            }
            else
            {
                var val = values.ElementAt(half);
                median = (float)(odd ? val.X : val.Y); 
            }
            return (float)Math.Ceiling(median);
        }

        public void Print(bool printSecond = false, bool showLeafRef = false)
        {
            Console.WriteLine("Vysvětlika:");
            Console.WriteLine("  - Bíle:  hlavní strom");
            Console.WriteLine("  - Modře: sekundární strom");
            Console.WriteLine("\nStom:");
            PrintTree(_root, "", true, false, printSecond, showLeafRef);
        }

        private void PrintTree(Node node, String indent, bool last, bool second, bool printSecond, bool showLeafRef)
        {
            if (!node.IsLeaf())
            {
                NodeBlock n = (NodeBlock)node;

                if (second)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }                    

                Console.WriteLine(indent + "+- " + n.Key + " <" + n.From + " ; " +n.To + ">");

                if (second)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (n.SecondTree != null && printSecond)
                    PrintTree(n.SecondTree, indent + "   ", true, true, false, showLeafRef);
            }
            else
            {
                LeafNode n = (LeafNode)node;

                if (second)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                if (showLeafRef)
                {
                    Console.WriteLine(indent + "+- X[" + n.Data.X + " ; " + n.Data.Y + "]");
                    Console.WriteLine(indent + "      PrevRef(" + (n.Prev == null ? "nil" : "[" + n.Prev.Data.X + "-" + n.Prev.Data.Y + "]") + ")");
                    Console.WriteLine(indent + "      NextRef(" + (n.Next == null ? "nil" : "[" + n.Next.Data.X + "-" + n.Next.Data.Y + "]") + ")");
                }
                else
                {
                    Console.WriteLine(indent + "+- X[" + n.Data.X + " ; " + n.Data.Y + "]" );
                }

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
                    PrintTree(n.Left, indent, false, second, printSecond, showLeafRef);
                if (n.Right != null)
                    PrintTree(n.Right, indent, true, second, printSecond, showLeafRef);
            }
        }
    }
}
