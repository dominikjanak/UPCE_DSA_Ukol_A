using System;

namespace RangeTree
{
    /// <summary>
    /// This is tree node which is not a leaf
    /// </summary>
    /// <typeparam name="TValue">Data type object</typeparam>
    public partial class RangeTree<TValue>
    where TValue : IValue
    {
        internal class BlockNode : Node
        {
            // Define median value of both descendants
            public float Median { get; private set; }

            // Minimum value of leaf in descendants
            public float Min { get; private set; }

            // Maximum value of leaf in descendants
            public float Max { get; private set; }

            // left subtree
            public Node Left { get; set; }

            // rigth subtree
            public Node Right { get; set; }

            // second dimension tree (Y axis)
            public Node SecondTree { get; set; }

            public BlockNode(float median, float min, float max, BlockNode parent)
            {
                SecondTree = null;
                Left = Right = null;
                Parent = parent;
                Median = median;
                Min = min;
                Max = max;
            }

            // check if this node is Leaf
            public override bool IsLeaf()
            {
                return false;
            }
        }
    }
}
