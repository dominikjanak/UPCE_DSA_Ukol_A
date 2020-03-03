using System;

namespace GraphService
{
    public class Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>
        : IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexData : IVertexData
        where TEdgeData : IEdgeData
    {
        public TEdgeKey Key { get; }
        public TEdgeData Data { get; set; }

        public Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> StartVertex { get; set; }
        public Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> TargetVertex { get; set; }

        public Edge(TEdgeKey edgeKey, TEdgeData edgeData, 
            Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> source, 
            Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> destination)
        {
            Key = edgeKey;
            Data = edgeData;
            StartVertex = source;
            TargetVertex = destination;
        }

        public Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> GetOpositeVertex(Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> vertex)
        {
            if (vertex == StartVertex)
            {
                return TargetVertex;
            }
            return StartVertex;
        }

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}
