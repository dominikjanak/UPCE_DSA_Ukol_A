namespace Ukol_A
{
    public class EdgeData
    {
        private EdgeType _type;
        private float _distance;

        public EdgeData(float distance, EdgeType type)
        {
            _distance = distance;
            _type = type;
        }

        public EdgeType EdgeType
        {
            get => _type;
            set => _type = value;
        }

        public float GetDistance()
        {
            return _distance;
        }

    }
}
