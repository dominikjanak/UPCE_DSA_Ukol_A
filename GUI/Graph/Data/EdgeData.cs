namespace GUI.Graph
{
    public class EdgeData 
        : GraphService.IEdgeData
    {
        public EdgeType EdgeType { get; set; }
        public float Distance { get; set; }

        public EdgeData(float distance, EdgeType type)
        {
            Distance = distance;
            EdgeType = type;
        }

        public bool IsBlocked()
        {
            return EdgeType.Blocked == this.EdgeType;
        }

        public float GetDistance()
        {
            return Distance;
        }
    }
}
