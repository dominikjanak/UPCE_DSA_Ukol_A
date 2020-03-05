using System;

namespace GraphService
{
    public partial class Graph<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        : IGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexData : IVertexData
        where TEdgeData : IEdgeData
    {
        internal class Edge : IEdge
        {
            public TEdgeKey Key { get; }
            public TEdgeData Data { get; set; }

            public Vertex StartVertex { get; set; }
            public Vertex TargetVertex { get; set; }

            public Edge(TEdgeKey edgeKey, TEdgeData edgeData, 
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

            public override string ToString()
            {
                return Key.ToString();
            }
        }
    }
}
