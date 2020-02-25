using System.Drawing;

namespace Ukol_A
{
    public class VertexData
    {
        private PointF _location;
        private VertexType _type;

        public VertexData(PointF location, VertexType type = VertexType.Junction)
        {
            _location = location;
            _type = type;
        }

        public PointF GetLocation()
        {
            return _location;
        }

        public VertexType GetVertexType()
        {
            return _type;
        }
    }
}
