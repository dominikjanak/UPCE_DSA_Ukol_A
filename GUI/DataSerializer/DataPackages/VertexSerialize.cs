using GUI.Graph;

namespace GUI
{
    public class VertexSerialize
    {
        public VertexSerialize() { }

        public VertexSerialize(string key, float x, float y, VertexType type)
        {
            Key = key;
            X = x;
            Y = y;
            Type = type;
        }

        public string Key { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public VertexType Type { get; set; }
    }
}
