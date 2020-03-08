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
        private Node Build(List<TValue> values, NodeBlock parent, bool odd, LeafNode lastLeaf = null)
        {
            Node newItem = null;

            if (values.Count > 1)
            {
                newItem = CreateNode(values, parent, odd, lastLeaf);
            }
            else
            {
                lastLeaf = CreateLeaf(values[0], parent, lastLeaf);
                newItem = lastLeaf;
            }

            if (odd == true && !newItem.IsLeaf())
            {

                ((NodeBlock)newItem).SecondTree = Build(values, null, false);
                ((NodeBlock)newItem).Parent = (NodeBlock)newItem;
            }

            return newItem;
        }

        private NodeBlock CreateNode(List<TValue> values, NodeBlock parent, bool odd, LeafNode lastLeaf)
        {
            values = values.OrderBy(i => odd ? i.X : i.Y).ToList();

            TValue minItem = values[0];
            TValue maxItem = values[values.Count - 1];

            float minValue = (float)(odd ? minItem.X : minItem.Y);
            float maxValue = (float)(odd ? maxItem.X : maxItem.Y);
            float median = GetMedian(values, odd);

            NodeBlock newItem = new NodeBlock(median, minValue, maxValue, parent);

            var splited = SplitList(values);

            newItem.Left = Build(splited.First, newItem, odd, lastLeaf);
            newItem.Right = Build(splited.Second, newItem, odd, lastLeaf);

            return newItem;
        }

        /*
        @Override
        public T find(K key) {
            buffer.flushEvent();

            if (!isEmpty()) {
                buffer.flushDraw();
                iterate(root, TreeNodeType.ntRoot, 0, SortType.stX);
            }

            return findNode(key);
        } 
        */

        public TValue Find(float x, float y)
        {
            if (_root.IsLeaf())
            {
                LeafNode leaf = (LeafNode)_root;
                if(leaf.Data.X == x && leaf.Data.Y == y)
                {
                    return leaf.Data;
                }
                return default(TValue);
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
        }

        /*
        private T findNode(K key)
        {
            int level = 1;
            addToEventList(root, TreeNodeType.ntRoot, level++, SortType.stX);
            if (root != null && root.isLeafNode)
            {
                if (root.getNode().getKey().compareX(key.getX()) == 0 &&
                        root.getNode().getKey().compareY(key.getY()) == 0)
                    return root.getNode().getData();
            }

            RNode<K, T> actualNode = root;
            while (actualNode != null)
            {
                if (isNodeNotNullAndLeaf(actualNode.leftSon))
                {
                    if (actualNode.getLeftSon().getNode().getKey().compareX(key.getX()) == 0 &&
                            actualNode.getLeftSon().getNode().getKey().compareY(key.getY()) == 0)
                    {
                        addToEventList(actualNode.getLeftSon(), TreeNodeType.ntLeftSon, level++, SortType.stX);
                        return actualNode.getLeftSon().getNode().getData();
                    }
                }

                if (isNodeNotNullAndLeaf(actualNode.rightSon))
                {
                    if (actualNode.getRightSon().getNode().getKey().compareX(key.getX()) == 0 &&
                            actualNode.getRightSon().getNode().getKey().compareY(key.getY()) == 0)
                    {
                        addToEventList(actualNode.getRightSon(), TreeNodeType.ntRightSon, level++, SortType.stX);
                        return actualNode.getRightSon().getNode().getData();
                    }
                }

                if (isNodeNotNullAndNotLeaf(actualNode.leftSon) &&
                        (actualNode.getLeftSon().getNode().getKey().getX() <= key.getX()) &&
                        (key.getX() <= actualNode.getLeftSon().getNode().getKey().getY()))
                {
                    actualNode = actualNode.leftSon;
                    addToEventList(actualNode, TreeNodeType.ntLeftSon, level++, SortType.stX);
                    continue;
                }
                else
                {

                    if (!isNodeNotNullAndNotLeaf(actualNode.rightSon) ||
                            (!(actualNode.getRightSon().getNode().getKey().getX() <= key.getX())) ||
                            (!(key.getX() <= actualNode.getRightSon().getNode().getKey().getY())))
                    {
                        return null;
                    }
                    actualNode = actualNode.rightSon;
                    addToEventList(actualNode, TreeNodeType.ntRightSon, level++, SortType.stX);
                }
            }

            return null;
        }*/

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

        public void Print(bool printSecond = false)
        {
            Console.WriteLine("Vysvětlika:");
            Console.WriteLine("  - Bíle:  hlavní strom");
            Console.WriteLine("  - Modře: sekundární strom");
            Console.WriteLine("\nStom:");
            PrintTree(_root, "", true, false, printSecond);
        }

        private void PrintTree(Node node, String indent, bool last, bool second, bool printSecond)
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

                if (n.SecondTree != null && printSecond)
                    PrintTree(n.SecondTree, indent + "   ", true, true, false);
            }
            else
            {
                LeafNode n = (LeafNode)node;

                if (second)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                Console.WriteLine(indent + "+- X[" + n.Data.X + " ; " + n.Data.Y + "]");

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
                    PrintTree(n.Left, indent, false, second, printSecond);
                if (n.Right != null)
                    PrintTree(n.Right, indent, true, second, printSecond);
            }
        }
    }
}
