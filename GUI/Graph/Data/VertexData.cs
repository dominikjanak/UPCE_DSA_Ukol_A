using System.Drawing;

namespace GUI.Graph
{
    public class VertexData 
        : GraphService.IVertexData, RangeTree.IValue
    {
        public VertexType VertexType { get; set; }
        public PointF Location { get; set; }

        public VertexData(PointF location, VertexType type = VertexType.Junction)
        {
            Location = location;
            VertexType = type;
        }

        public float X => Location.X;

        public float Y => Location.Y;
    }
}
