namespace Ukol_A
{
    class Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> : IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>
    {
        private TEdgeKey _key;
        private TEdgeData _data;

        private IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> _startVertex;
        private IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> _endVertex;

        public Edge(TEdgeKey edgeKey, TEdgeData edgeData, 
            IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> source, 
            IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> destination)
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

        public IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> GetStartVertex()
        {
            return _startVertex;
        }

        public IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> GetTargetVertex()
        {
            return _endVertex;
        }
        public IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> GetOpositeVertex(IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> vertex)
        {
            if(vertex == _startVertex)
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
