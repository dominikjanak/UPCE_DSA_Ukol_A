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
        // root node of tree
        private Node _root;
        private LeafNode _lastLeaf; // temporaly variable for building tree

        public RangeTree()
        {
            _root = null;
            _lastLeaf = null;
        }

        /// <summary>
        /// Build RangeTree (can be use only once)
        /// </summary>
        /// <param name="values">List of values</param>
        public RangeTree(List<TValue> values) : this()
        {
            Build(values);
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

        // Popis: metoda pro vtvoření stromu prvně ověří, zda jsou data v pořádku
        //        následně zavolá rezurzivní privátní metodu, která se postará 
        //        o stavbu stromu.
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

            // unique X values
            if (values.Select(x=>x.X).Distinct().Count() != values.Count())
            {
                TValue item = default;

                foreach (var f in values)
                {
                    bool find = false;
                    foreach (var v in values)
                    {
                        if(f.X == v.X && f.Y != v.Y)
                        {
                            find = true;
                            break;
                        }
                    }

                    if (find)
                    {
                        item = f;
                        break;
                    }
                }

                throw new InvalidDataException(String.Format(Resources.SameValue, "X", item.X));
            }

            // unique Y values
            if (values.Select(y => y.Y).Distinct().Count() != values.Count())
            {
                TValue item = default;

                foreach (var f in values)
                {
                    bool find = false;
                    foreach (var v in values)
                    {
                        if (f.Y == v.Y && f.X != v.X)
                        {
                            find = true;
                            break;
                        }
                    }

                    if (find)
                    {
                        item = f;
                        break;
                    }
                }

                throw new InvalidDataException(String.Format(Resources.SameValue, "Y", item.Y));
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


        // Popis: Rekurzivní privástní metoda stavějící strom.
        //        Rozhodne se, zda bude tvořit uzel stromu nebo list.
        //        Následně, pokud byl vytvořen uzel stromu a zanoření je 
        //        v první dimenzi (x), tak se vygeneruje i strom druhé dimenze (y).
        /// <summary>
        /// Build range tree
        /// </summary>
        /// <param name="values">List ordered by X</param>
        /// <param name="parent">Reference to previous node (parent)</param>
        /// <param name="odd">Build X == true or Y == false</param>
        /// <returns></returns>
        private Node Build(List<TValue> values, BlockNode parent, bool odd)
        {
            Node newItem = null;

            if (values.Count > 1)
            {
                // create block node
                newItem = CreateNodeBlock(values, parent, odd);
            }
            else
            {
                // create leaf node
                newItem = _lastLeaf = new LeafNode(values[0], parent, _lastLeaf);
                _lastLeaf = (LeafNode)newItem;
            }

            // if not leaf and second dimension then build sesond tree
            if (odd == true && IsNodeNotNullAndNotLeaf(newItem))
            {
                LeafNode leaf = _lastLeaf; // save last leaf durind second dimension tree build
                _lastLeaf = null;
                ((BlockNode)newItem).SecondTree = Build(values, null, false); // build second dimension tree
                ((BlockNode)newItem).Parent = (BlockNode)newItem;
                _lastLeaf = leaf;
            }

            return newItem;
        }

        // Popis: Zjistí min, max, vypočítá median. Následně vytvoří nový uzel stromu.
        //        Pole vstupních hodnot rozdělí na dvě poloviny a pro každou postaví podstrom.
        /// <summary>
        /// Create new NodeBlock
        /// </summary>
        /// <param name="values">List of values</param>
        /// <param name="parent">Reference to parent</param>
        /// <param name="dimension">Dimension (true = primary)</param>
        /// <returns>New NodeBlock</returns>
        private BlockNode CreateNodeBlock(List<TValue> values, BlockNode parent, bool dimension)
        {
            values = values.OrderBy(i => dimension ? i.X : i.Y).ThenBy(i => dimension ? i.Y : i.X).ToList(); // order values

            TValue firstItem = values[0]; 
            TValue lastItem = values[values.Count - 1]; 

            float minValue = (dimension ? firstItem.X : firstItem.Y); // Min
            float maxValue = (dimension ? lastItem.X : lastItem.Y); // Max
            float median = GetMedian(values, dimension); // Median

            BlockNode newItem = new BlockNode(median, minValue, maxValue, parent); // Create new BlockNode

            var splited = SplitList(values); // split list

            newItem.Left = Build(splited.First, newItem, dimension); // build left descendent
            newItem.Right = Build(splited.Second, newItem, dimension); // build right descendent

            return newItem;
        }

        // Popis: rozdělí vstupní list na dvě poloviny
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

            if (count % 2 == 0)
            {
                var val1 = values.ElementAt(half);
                var val2 = values.ElementAt(half - 1);

                median = ((fromX ? val1.X : val1.Y) + (fromX ? val2.X : val2.Y)) / 2;
            }
            else
            {
                var val = values.ElementAt(half);
                median = (fromX ? val.X : val.Y);
            }

            return (float)Math.Ceiling(median);
        }

        // Popis: Najde konkrétní hodnotu. Zavolá privátní vyhledávací 
        //        metodu a postará se o správné vrácení výsledku.
        /// <summary>
        /// Find specific value
        /// </summary>
        /// <param name="x">Searched value X</param>
        /// <param name="y">Searched value Y</param>
        /// <returns>Return value</returns>
        public TValue Find(float x, float y) 
        {
            var node = FindNode(x, y);

            if(IsNodeNotNullAndLeaf(node)) 
            {
                return ((LeafNode)node).Data;
            }

            return default(TValue);
        }

        // Popis: První krok ověří, zda je kořen listem. Pokud ano, zkusí ověřit tuto hodnotu. 
        //        Pokus ne, začne prozkoumávat strom. Pro každý uzel ověří, zda je levý potomek 
        //        list a zda se shoduje hodnota X s hledanou nodnotou x. Pokud se shoduje i Y,
        //        tak vrátí daný uzel. Pokud se Y neshoduje, prohledá zřetězené listy.
        //        Stejnou operaci provede i pro pravého potomka (zda je list a hodnoty se shodují).
        //        Pokud ani jeden z uzel nebyl list, tak prochází strom. Vždy volí pouze jednu cestu.
        //        Nikdy se nevrací. Pokud levý potomek není list a x spadá do intervalu hodnot, který
        //        je v levém podstromu, tak pokračuje vlevo. Pokud nelze jít v pravo, ověří, zda má vůbec 
        //        cenu pokračovat, pokud ne, končí. Pokud ano a je to možné, jde do pravého potomka.
        /// <summary>
        /// Find node by specific value
        /// </summary>
        /// <param name="x">Searched value X</param>
        /// <param name="y">Searched value Y</param>
        /// <returns>Value or NULL</returns>
        private Node FindNode(float x, float y)
        {
            // root is leaf
            if (IsNodeNotNullAndLeaf(_root))
            {
                LeafNode leafRoot = (LeafNode)_root;
                if (leafRoot.Data.X == x && leafRoot.Data.Y == y)
                {
                    return leafRoot;
                }
                return null;
            }

            BlockNode node = (BlockNode)_root;
            LeafNode leaf = null;

            while (node != null)
            {
                // Left descendant
                if(IsNodeNotNullAndLeaf(node.Left) && ((LeafNode)node.Left).Data.X == x)
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
                if (IsNodeNotNullAndNotLeaf(node.Left) && ((BlockNode)node.Left).Min <= x && ((BlockNode)node.Left).Max >= x)
                {
                    // Continue left descendant
                    node = (BlockNode)node.Left;
                    continue;
                }
                else
                {
                    if(node.Right != null && (node.Right.IsLeaf() || node.Min > x || node.Max < x))
                    {
                        // there is no way to continue
                        return null;
                    }
                    if (IsNodeNotNullAndNotLeaf(node.Right))
                    {
                        // Continue right descendant
                        node = (BlockNode)node.Right;
                    }
                }
            }

            return null;
        }

        // Popis: Prohledává zřetězené listy. Jelikož jsou seřazené, a vždy se hledá
        //        hledá podle Y, tak ví, zda iterovat vpravo, nebo vlevo.
        //        Při správných hodnotách by se do této části kódu nikdy neměl dostat.
        //        Řešení pro možné duplicity v souřadnicích.
        /// <summary>
        /// Find value in a leaf list
        /// </summary>
        /// <param name="leaf">Start leaf</param>
        /// <param name="x">Searched value X</param>
        /// <param name="y">Searched value Y</param>
        /// <returns>Value of NULL</returns>
        private LeafNode FindInLeaves(LeafNode leaf, float x, float y)
        {
            // define where to go
            bool up = false;
            if(leaf.Data.Y < y)
            {
                up = true;
            }

            // go throw linked list of leaves
            while(leaf != null)
            {
                if (leaf.Data.X != x)
                {
                    // invalid X
                    break;
                }

                if(leaf.Data.Y == y)
                {
                    // leaf successfuly find
                    return leaf;
                }

                leaf = up ? leaf.Next : leaf.Prev; // next item
            }
            return null;
        }

        // Popis: Připraví vstupní data pro rekurzivní metodu intervalového vyhledávání.
        //        xFrom musí být menší než xTo
        //        yFrom musí být menší než yTo
        //        Připraví výstupní list dat, která pošle do vyhledávacího
        //        algoritmu.
        /// <summary>
        /// Find values by range search 
        /// </summary>
        /// <param name="xFrom">X from</param>
        /// <param name="yFrom">Y from</param>
        /// <param name="xTo">X to</param>
        /// <param name="yTo">Y to</param>
        /// <returns>List of values</returns>
        public List<TValue> RangeScan(float xFrom, float yFrom, float xTo, float yTo)
        {
            // fix X range 
            if (xFrom > xTo)
            {
                Swap(ref xFrom, ref xTo);
            }

            // fix Y range 
            if (yFrom > yTo)
            {
                Swap(ref yFrom, ref yTo);
            }

            var data = new List<TValue>(); // Output data init
            RangeFind(data, xFrom, yFrom, xTo, yTo, _root, true); // find data
            return data;
        }

        // Popis: Provede rekurzivní prohledání stromu a najde všechny hodnoty v intervalu.
        //        Pokud je uzel list, zjistí, zda spadá do intervalu, pokud ano, přidá data 
        //        do výstupní kolekce. Pokud není listem, traverzuje stromem.
        //        Kudy má jít hledá pomocí hodnoty mediánu. Pokud při procházení primárního
        //        stromu (x) může jít oběma větvemi, tak začne stejným způsobem rekurzivně 
        //        prohledávat strom druhé dimenze (y). Jinak podle pozice hledaného intervalu
        //        vůči mediánu jde do levé, popřípadě vpravé větvě.
        /// <summary>
        /// Fin in range of actual Node
        /// </summary>
        /// <param name="outData">Reference to list with output data</param>
        /// <param name="xFrom">X from</param>
        /// <param name="yFrom">Y from</param>
        /// <param name="xTo">X to</param>
        /// <param name="yTo">Y to</param>
        /// <param name="node">Node for explore</param>
        /// <param name="dim">Dimension (true = primary)</param>
        private void RangeFind(List<TValue> outData, float xFrom, float yFrom, float xTo, float yTo, Node node, bool dim)
        {
            LeafNode leaf;
            BlockNode block;

            if(node.IsLeaf())
            {
                // IS LEAF
                leaf = (LeafNode)node;
                if (leaf.Data.X >= xFrom && leaf.Data.X <= xTo && leaf.Data.Y >= yFrom && leaf.Data.Y <= yTo)
                {
                    // Valid value
                    outData.Add(leaf.Data);
                }
            }
            else
            {
                // IS NOT LEAF
                block = (BlockNode)node;

                float min = dim ? xFrom : yFrom;
                float max = dim ? xTo : yTo;

                if (min <= block.Median && block.Median < max && dim == true)
                {
                    // Can go both substrees in primary tree
                    RangeFind(outData, xFrom, yFrom, xTo, yTo, block.SecondTree, !dim);
                }
                else
                {
                    if (min <= block.Median)
                    {
                        // Go Left subtree
                        RangeFind(outData, xFrom, yFrom, xTo, yTo, block.Left, dim); // find in Left subtree
                    }

                    if (block.Median < max)
                    {
                        // Go Right subtree
                        RangeFind(outData, xFrom, yFrom, xTo, yTo, block.Right, dim); // find in Right subtree
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
                BlockNode n = (BlockNode)node;

                if (second)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }                    

                Console.WriteLine(indent + "+- " + n.Median + " <" + n.Min + " ; " +n.Max + ">");

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
                BlockNode n = (BlockNode)node;
                if(n.Left != null)
                    PrintTree(n.Left, indent, false, second, printSecond, showLeafRef);
                if (n.Right != null)
                    PrintTree(n.Right, indent, true, second, printSecond, showLeafRef);
            }
        }
    }
}
