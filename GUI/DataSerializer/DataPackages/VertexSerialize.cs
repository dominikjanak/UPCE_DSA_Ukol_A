using GUI.Graph;

namespace GUI
{
    public class VertexSerialize
    {
        public VertexSerialize() { }

        public VertexSerialize(string id, float x, float y, VertexType type)
        {
            ID = id;
            X = x;
            Y = y;
            Type = type;
        }

        public string ID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public VertexType Type { get; set; }
    }
}
