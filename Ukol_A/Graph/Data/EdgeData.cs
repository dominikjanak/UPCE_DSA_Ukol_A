namespace Ukol_A
{
    public class EdgeData : IEdgeData
    {
        private EdgeType _edgeType;
        private float _distance;

        public EdgeType GetEdgeType()
        {
            return _edgeType;
        }
        public float GetDistance()
        {
            return _distance;
        }

    }
}
