using System;

namespace GraphService
{
    public partial class Graph<TVertexKey, TVertexValue, TEdgeKey, TEdgeValue>
        : IGraph<TVertexKey, TVertexValue, TEdgeKey, TEdgeValue>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexValue : IVertexData
        where TEdgeValue : IEdgeData
    {
        internal class Edge : IEdge
        {
            public TEdgeKey Key { get; }
            public TEdgeValue Data { get; set; }

            public Vertex StartVertex { get; set; }
            public Vertex TargetVertex { get; set; }

            public Edge(TEdgeKey edgeKey, TEdgeValue edgeData, 
                Vertex source, 
                Vertex destination)
            {
                Key = edgeKey;
                Data = edgeData;
                StartVertex = source;
                TargetVertex = destination;
            }

            public Vertex GetOpositeVertex(Vertex vertex)
            {
                if (vertex == StartVertex)
                {
                    return TargetVertex;
                }
                return StartVertex;
            }
        }
    }
}
