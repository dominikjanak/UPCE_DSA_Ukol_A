using System;

namespace Ukol_A
{
    public class Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> 
        where TVertexKey : IComparable
        where TEdgeKey : IComparable
    {
        private TEdgeKey _key;
        private TEdgeData _data;

        private Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> _startVertex;
        private Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> _endVertex;

        public Edge(TEdgeKey edgeKey, TEdgeData edgeData,
            Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> source,
            Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> destination)
        {
            _key = edgeKey;
            _data = edgeData;
            _startVertex = source;
            _endVertex = destination;
        }

        public TEdgeKey GetKey()
        {
            return _key;
        }

        public TEdgeData Data
        {
            get => _data;
            set => _data = value;
        }

        public Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> GetStartVertex()
        {
            return _startVertex;
        }

        public Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> GetTargetVertex()
        {
            return _endVertex;
        }
        public Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> GetOpositeVertex(Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> vertex)
        {
            if (vertex == _startVertex)
            {
                return _endVertex;
            }
            return _startVertex;
        }

        public override string ToString()
        {
            return _key.ToString();
        }
    }
}
