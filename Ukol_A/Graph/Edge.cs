namespace Ukol_A
{
    class Edge<TEdgeData, TVertexData> : IEdge<TEdgeData, TVertexData>
    {
        private string _edgeKey;
        private TEdgeData _edgeData;

        private IVertex<TVertexData, TEdgeData> _source;
        private IVertex<TVertexData, TEdgeData> _destination;

        public Edge(string edgeKey, TEdgeData edgeData)
        {
            _edgeKey = edgeKey;
            _edgeData = edgeData;
        }

        public string GetKey()
        {
            return _edgeKey;
        }

        public TEdgeData Data
        { 
            get => _edgeData;
            set => _edgeData = value;
        }

        public IVertex<TVertexData, TEdgeData> GetStartVertex()
        {
            return _source;
        }
        public IVertex<TVertexData, TEdgeData> GetTargetVertex()
        {
            return _destination;
        }

        public string GetLabel()
        {
            return _edgeKey;
        }

        public override string ToString()
        {
            return GetLabel() + " v: " + _source.ToString() + " -> " + _destination.ToString();
        }
    }
}
