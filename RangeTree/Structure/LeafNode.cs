namespace RangeTree
{
    // vnitřní třída (partial)
    public partial class RangeTree<TValue>
    where TValue : IValue
    {
        internal class LeafNode : Node
        {
            public LeafNode Prev { get; internal set; }
            public LeafNode Next { get; internal set; }
            public TValue Data { get; private set; }

            public LeafNode(TValue data, BlockNode parent, LeafNode prev)
            {
                Parent = parent;
                Data = data;
                Prev = prev;
                if(prev != null)
                {
                    Prev.Next = this;
                }
                Next = null;
            }

            public override bool IsLeaf()
            {
                return true;
            }
        }
    }
}
