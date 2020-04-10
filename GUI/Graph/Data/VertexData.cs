using System.Drawing;

namespace GUI.Graph
{
    public class VertexData 
        : GraphService.IVertexData, RangeTree.IValue
    {
        public string Key { get; private set; }
        public VertexType VertexType { get; set; }
        public PointF Location { get; set; }

        public VertexData(string key, PointF location, VertexType type = VertexType.Junction)
        {
            Key = key;
            Location = location;
            VertexType = type;
        }

        public float X => Location.X;

        public float Y => Location.Y;
    }
}
