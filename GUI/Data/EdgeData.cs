using ForestGraph;

namespace GUI
{
    public class EdgeData
    {
        public EdgeType EdgeType { get; set; }
        public float Distance { get; set; }

        public EdgeData(float distance, EdgeType type)
        {
            Distance = distance;
            EdgeType = type;
        }
    }
}
