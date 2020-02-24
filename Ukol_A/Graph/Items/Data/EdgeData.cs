namespace Ukol_A
{
    public class EdgeData : IEdgeData
    {
        private EdgeType _type;
        private float _distance;

        public EdgeData(float distance, EdgeType type)
        {
            _distance = distance;
            _type = type;
        }

        public EdgeType GetEdgeType()
        {
            return _type;
        }
        public float GetDistance()
        {
            return _distance;
        }

    }
}
