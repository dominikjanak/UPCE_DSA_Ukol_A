using System;

namespace RangeTree
{
    public partial class RangeTree<TValue>
    where TValue : IValue
    {
        internal class BlockNode : Node
        {
            public float Median { get; private set; }

            public float From { get; private set; }
            public float To { get; private set; }

            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node SecondTree { get; set; }

            public BlockNode(float median, float from, float to, BlockNode parent)
            {
                SecondTree = null;
                Left = Right = null;
                Parent = parent;
                Median = median;
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
