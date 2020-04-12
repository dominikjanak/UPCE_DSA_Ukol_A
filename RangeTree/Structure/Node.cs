using System;

namespace RangeTree
{
    public partial class RangeTree<TValue>
    where TValue : IValue
    {
        internal abstract class Node
        {
            public BlockNode Parent { get; set; }

            abstract public bool IsLeaf();
        }
    }
}
