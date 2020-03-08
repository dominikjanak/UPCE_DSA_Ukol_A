
using System;

namespace RangeTree
{
    public partial class RangeTree<TValue>
    where TValue : IValue
    {
        internal class NodeBlock : Node
        {
            public double Key { get; private set; }

            public double  From { get; private set; }
            public double  To { get; private set; }

            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node SecondTree { get; set; }

            public NodeBlock(double key, double from, double to, NodeBlock parent)
            {
                SecondTree = null;
                Left = Right = null;
                Parent = parent;
                Key = key;
                From = from;
                To = to;
            }

            public override bool IsLeaf()
            {
                return false;
            }
        }
    }
}
