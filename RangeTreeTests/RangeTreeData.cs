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

        public double X => _x;

        public double Y => _y;
    }
}