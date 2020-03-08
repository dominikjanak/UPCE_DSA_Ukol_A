
using System;

namespace RangeTree
{
    public partial class RangeTree<TValue>
    where TValue : IValue
    {
        internal class NodeBlock : Node
        {
            public NodeBlock Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node SecondTree { get; set; }

            public double  Min { get; private set; }
            public double  Max { get; private set; }

            public double Key { get; private set; }
            
            public NodeBlock(double key, double min, double max, NodeBlock parent /*, int deep, bool odd*/)
            {
                SecondTree = null;
                Left = Right = null;
                Parent = parent;
                Key = key;
                Min = min;
                Max = max;
            }

            public override bool IsLeaf()
            {
                return false;
            }
        }
    }
}
