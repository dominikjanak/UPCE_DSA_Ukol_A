using System;
using System.Collections.Generic;

namespace GraphService
{
    public partial class Graph<TVertexKey, TVertexValue, TEdgeKey, TEdgeValue>
        : IGraph<TVertexKey, TVertexValue, TEdgeKey, TEdgeValue>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexValue : IVertexData
        where TEdgeValue : IEdgeData
    {
        internal class Vertex : IVertex
        {
            public TVertexKey Key { get; }
            public TVertexValue Data { get; set; }
            public List<Edge> IncidentEdges { get; }
            
            public Vertex(TVertexKey key, TVertexValue data)
            {
                Key = key;
                Data = data;
                IncidentEdges = new List<Edge>();
            }
        }
    }
}
