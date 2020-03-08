using System;

namespace RangeTree
{
    public partial class RangeTree<TValue>
    where TValue : IValue
    {
        internal abstract class Node
        {
            abstract public bool IsLeaf();
        }
    }
}
