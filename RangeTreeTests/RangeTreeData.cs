namespace RangeTree.Tests
{
    internal class RangeTreeData : IValue
    {
        private float _x;
        private float _y;

        public RangeTreeData()
        {
        }

        public RangeTreeData(float x, float y)
        {
            _x = x;
            _y = y;
        }

        public float X => _x;

        public float Y => _y;
    }
}