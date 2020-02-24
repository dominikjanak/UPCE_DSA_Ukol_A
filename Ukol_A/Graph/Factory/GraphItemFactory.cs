namespace Ukol_A
{
    class GraphItemFactory<TVertexKey, TVertexData, TEdgeKey, TEdgeData> : IGraphItemFactory<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
    {
        public IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> CreateVertex(TVertexKey key, TVertexData data)
        {
            return new Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>(key, data);
        }

        public IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> CreateEdge(TEdgeKey key, TEdgeData data, 
            IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> start, 
            IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> end)
        {
            return new Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>(key, data, start, end);
        }
    }
}
