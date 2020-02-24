using System;
using System.Collections.Generic;

namespace Ukol_A
{
    public class Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        where TVertexKey : IComparable
        where TEdgeKey : IComparable
    {
        private TVertexKey _key;
        private TVertexData _data;
        List<Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>> _incidentEdges;

        public Vertex(TVertexKey vertexKey, TVertexData vertexData)
        {
            _key = vertexKey;
            _data = vertexData;
            _incidentEdges = new List<Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>>();
        }

        public TVertexKey GetKey()
        {
            return _key;
        }

        public List<Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>> GetIncidentEdges()
        {
            return _incidentEdges;
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
