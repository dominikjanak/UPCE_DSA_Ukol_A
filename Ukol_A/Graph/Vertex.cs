using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ukol_A
{
    class Vertex<TVertexData, TEdgeData> : IVertex<TVertexData, TEdgeData>
    {
        private string _vertexKey;
        private PointF _vertexLocation;
        private TVertexData _vertexData;
        List<IEdge<TEdgeData, TVertexData>> _edges;

        public Vertex(string vertexKey, PointF vertexLocation, TVertexData vertexData)
        {
            _vertexKey = vertexKey;
            _vertexLocation = vertexLocation;
            _vertexData = vertexData;
            _edges = new List<IEdge<TEdgeData, TVertexData>>();
        }
        public string GetKey()
        {
            return _vertexKey;
        }

        public List<IEdge<TEdgeData, TVertexData>> GetEdges()
        {
            return _edges;
        }

        public PointF GetLocation()
        {
            return _vertexLocation;
        }

        public TVertexData Data
        {
            get => _vertexData;
            set => _vertexData = value;
        }

        public string GetLabel()
        {
            return _vertexKey;
        }
        public string GetCoodrinations()
        {
            return "[" + _vertexLocation.X + ";" + _vertexLocation.Y + "]";
        }

        public override string ToString()
        {
            return _vertexKey + " " + GetCoodrinations();
        }
    }
}
