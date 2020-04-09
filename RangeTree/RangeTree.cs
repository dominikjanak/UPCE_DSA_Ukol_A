using RangeTree.Exceptions;
using RangeTree.Properties;
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
        private LeafNode _lastLeaf;

        public RangeTree()
        {
            _root = null;
            _lastLeaf = null;
        }

        /// <summary>
        /// Is tree builded
        /// </summary>
        /// <returns>true when tree is builded</returns>
        public bool IsBuilded()
        {
            return _root != null;
        }

        /// <summary>
        /// Rebuild RangeTree
        /// </summary>
        /// <param name="values">List of values</param>
        public void Rebuild(List<TValue> values)
        {
            _root = null;
            _lastLeaf = null;
            Build(values);
        }   

        /// <summary>
        /// Build RangeTree
        /// </summary>
        /// <param name="values">List of values</param>
        public void Build(List<TValue> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(Resources.ArgumentNull);
            }

            if(values.Select(x=>x.X).Distinct().Count() != values.Count())
            {
                throw new InvalidDataException(Resources.SameXValue);
            }

            if (IsBuilded())
            {
                throw new TreeIsAleadyBuildedException(Resources.TreeAlreadyBuilded);
            }

            if (values.Count == 0)
            {
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
        /// <param name="parent">Reference to previous node (parent)</param>
        /// <param name="odd">Build X == true or Y == false</param>
        /// <returns></returns>
        private Node Build(List<TValue> values, NodeBlock parent, bool odd)
        {
            Node newItem = null;

            if (values.Count > 1)
            {
                newItem = CreateNodeBlock(values, parent, odd);
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

        /// <summary>
        /// Find specific value
        /// </summary>
        /// <param name="x">Searched value X</param>
        /// <param name="y">Searched value Y</param>
        /// <returns>Return value</returns>
        public TValue Find(float x, float y) 
        {
            var node = FindNode(x, y);

            if(node != null && node.IsLeaf()) 
            {
                return ((LeafNode)node).Data;
            }

            return default(TValue);
        }

        /// <summary>
        /// Find node by specific value
        /// </summary>
        /// <param name="x">Searched value X</param>
        /// <param name="y">Searched value Y</param>
        /// <returns>Value or NULL</returns>
        private Node FindNode(float x, float y)
        {
            if (_root != null && _root.IsLeaf())
            {
                // root is leaf
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
                // Left descendant
                if(node.Left.IsLeaf() && ((LeafNode)node.Left).Data.X == x)
                {
                    // Node is leaf
                    leaf = (LeafNode)node.Left;
                    if (leaf.Data.Y == y)
                    {
                        // value found
                        return leaf;
                    }
                    else
                    {
                        // Value not find
                        leaf = FindInLeaves(leaf, x, y); // try to find in list of leaves
                        if (leaf != null)
                        {
                            return leaf;
                        }
                    }
                }

                // Right descendant
                if (node.Right.IsLeaf() && ((LeafNode)node.Right).Data.X == x)
                {
                    // Node is leaf
                    leaf = (LeafNode)node.Right;
                    if (leaf.Data.Y == y)
                    {
                        // value found
                        return leaf;
                    }
                    else
                    {
                        // Value not find
                        leaf = FindInLeaves(leaf, x, y); // try to find in list of leaves
                        if (leaf != null)
                        {
                            return leaf;
                        }
                    }
                }

                // traversing a tree
                if (node.Left != null && !node.Left.IsLeaf() && ((NodeBlock)node.Left).From <= x && ((NodeBlock)node.Left).To >= x)
                {
                    node = (NodeBlock)node.Left;
                    continue;
                }
                else
                {
                    if(node.Right != null && (node.Right.IsLeaf() || node.From > x || node.To < x))
                    {
                        return null;
                    }
                    if (node.Right != null && !node.Right.IsLeaf())
                    {
                        node = (NodeBlock)node.Right;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Find value in a leaf list
        /// </summary>
        /// <param name="leaf">Start leaf</param>
        /// <param name="x">Searched value X</param>
        /// <param name="y">Searched value Y</param>
        /// <returns>Value of NULL</returns>
        private LeafNode FindInLeaves(LeafNode leaf, float x, float y)
        {
            bool up = false;

            if(leaf.Data.Y < y)
            {
                up = true;
            }

            while(leaf != null)
            {
                if (leaf.Data.X != x)
                {
                    break;
                }

                if(leaf.Data.Y == y)
                {
                    return leaf;
                }

                if (up)
                {
                    leaf = leaf.Next;
                }
                else 
                { 
                    leaf = leaf.Prev;
                }
            }
            return null;
        }

        /// <summary>
        /// Find values by range search 
        /// </summary>
        /// <param name="from">Point from</param>
        /// <param name="to">point to</param>
        /// <returns>List of values</returns>
        public List<TValue> RangeFind(PointF from, PointF to)
        {
            float xFrom, xTo, yTop, yBottom;

            xFrom = from.X;
            xTo = to.X;

            yTop = from.Y;
            yBottom = to.Y;

            // normalize X range 
            if (xFrom > xTo)
            {
                Swap(ref xFrom, ref xTo);
            }

            // normalize Y range 
            if (yTop > yBottom)
            {
                Swap(ref yTop, ref yBottom);
            }

            var data = new List<TValue>(); // Output data init
            RangeFind(data, xFrom, yTop, xTo, yBottom, _root, true); // find data

            return data;
        }

        /// <summary>
        /// Fin in range of actual Node
        /// </summary>
        /// <param name="data">Reference to list with output data</param>
        /// <param name="fromX">X from</param>
        /// <param name="fromY">Y from</param>
        /// <param name="toX">X to</param>
        /// <param name="toY">Y to</param>
        /// <param name="node">Node for explore</param>
        /// <param name="dim">Dimension (true = primary)</param>
        private void RangeFind(List<TValue> data, float fromX, float fromY, float toX, float toY, Node node, bool dim)
        {
            LeafNode leaf;
            NodeBlock block;

            if(node.IsLeaf())
            {
                // IS LEAF
                leaf = (LeafNode)node;
                if (leaf.Data.X >= fromX && leaf.Data.X <= toX && leaf.Data.Y >= fromY && leaf.Data.Y <= toY)
                {
                    // Valid value
                    data.Add(leaf.Data);
                }
            }
            else
            {
                // IS NOT LEAF
                block = (NodeBlock)node;

                float min = dim ? fromX : fromY;
                float max = dim ? toX : toY;

                if (min <= block.Key && block.Key < max && dim == true)
                {
                    // Can go both substrees in primary tree
                    RangeFind(data, fromX, fromY, toX, toY, block.SecondTree, !dim);
                }
                else
                {
                    if (min <= block.Key)
                    {
                        // Go Left subtree
                        RangeFind(data, fromX, fromY, toX, toY, block.Left, dim); // find in Left subtree
                    }

                    if (block.Key < max)
                    {
                        // Go Right subtree
                        RangeFind(data, fromX, fromY, toX, toY, block.Right, dim); // find in Right subtree
                    }
                }
            }
        }

        /// <summary>
        /// Swap two variables
        /// </summary>
        /// <param name="x">First variable</param>
        /// <param name="y">Second variable</param>
        private void Swap(ref float x, ref float y)
        {
            float tmp = x;
            x = y;
            y = tmp;
        }

        /// <summary>
        /// Check if node is not NULL and it is not a leaf
        /// </summary>
        /// <param name="node">Node for validate</param>
        /// <returns>true when node is a blockNode</returns>
        private bool IsNodeNotNullAndNotLeaf(Node node)
        {
            if (node == null || node.IsLeaf())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check if node is not NULL and it is a leaf
        /// </summary>
        /// <param name="node">Node for validate</param>
        /// <returns>true when node is a leaf</returns>
        private bool IsNodeNotNullAndLeaf(Node node)
        {
            if (node == null || !node.IsLeaf())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check if Node is not NULL
        /// </summary>
        /// <param name="node">Node for validate</param>
        /// <returns>true if instance of Node</returns>
        private bool IsNodeNotNull(Node node)
        {
            if (node == null) 
            { 
                return false; 
            }
            return true;
        }

        /// <summary>
        /// Split the list in half
        /// </summary>
        /// <param name="values">Input data in list</param>
        /// <returns>Two lists with data</returns>
        private (List<TValue> First, List<TValue> Second) SplitList(List<TValue> values)
        {
            int cnt = values.Count();
            int firstCount = cnt - (cnt / 2);

            List<TValue> first = new List<TValue>(values.Take(firstCount));
            List<TValue> second = new List<TValue>(values.Skip(firstCount));

            return (first, second);
        }

        /// <summary>
        /// Create new Leaf with data
        /// </summary>
        /// <param name="value">Value object</param>
        /// <param name="parent">Refefence to parent</param>
        /// <param name="lastLeaf">Reference to previous generated leaf</param>
        /// <returns>New leaf</returns>
        private LeafNode CreateLeaf(TValue value, NodeBlock parent, LeafNode lastLeaf = null)
        {
            return new LeafNode(value, parent, lastLeaf);
        }

        /// <summary>
        /// Create new NodeBlock
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="parent">Reference to parent</param>
        /// <param name="dimension">Dimension (true = primary)</param>
        /// <returns>New NodeBlock</returns>
        private NodeBlock CreateNodeBlock(List<TValue> values, NodeBlock parent, bool dimension)
        {
            values = values.OrderBy(i => dimension ? i.X : i.Y).ThenBy(i => dimension ? i.Y : i.X).ToList();

            TValue minItem = values[0];
            TValue maxItem = values[values.Count - 1];

            float minValue = (float)(dimension ? minItem.X : minItem.Y);
            float maxValue = (float)(dimension ? maxItem.X : maxItem.Y);
            float median = GetMedian(values, dimension);

            NodeBlock newItem = new NodeBlock(median, minValue, maxValue, parent);

            var splited = SplitList(values);

            newItem.Left = Build(splited.First, newItem, dimension);
            newItem.Right = Build(splited.Second, newItem, dimension);

            return newItem;
        }

        /// <summary>
        /// Calculate median value
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="fromX">Define which axe is used</param>
        /// <returns>Median value</returns>
        private float GetMedian(List<TValue> values, bool fromX)
        {
            int count = values.Count();
            int half = count / 2;
            float median;

            if(count % 2 == 0)
            {
                var val1 = values.ElementAt(half);
                var val2 = values.ElementAt(half-1);

                median = (float)((fromX ? val1.X : val1.Y) + (fromX ? val2.X : val2.Y)) / 2;
            }
            else
            {
                var val = values.ElementAt(half);
                median = (float)(fromX ? val.X : val.Y); 
            }

            return (float)Math.Ceiling(median);
        }

        // DEBUG METHOD
// ==========================================================================================================================

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
