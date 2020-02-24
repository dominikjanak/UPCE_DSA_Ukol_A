using System.Collections.Generic;

namespace Ukol_A
{
    class Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> : IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
    {
        private TVertexKey _key;
        private TVertexData _data;
        List<(IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>, IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>)> _incidentVertexes;

        public Vertex(TVertexKey vertexKey, TVertexData vertexData)
        {
            _key = vertexKey;
            _data = vertexData;
            _incidentVertexes = new List<(IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>, IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>)>();
        }

        public TVertexKey GetKey()
        {
            return _key;
        }

        public List<(IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>, IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>)> GetIncidentVertexes()
        {
            return _incidentVertexes;
        }

        public TVertexData Data
        {
            get => _data;
            set => _data = value;
        }

        public override string ToString()
        {
            return _key.ToString();
        }
    }
}
