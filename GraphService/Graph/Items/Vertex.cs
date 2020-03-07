using System;
using System.Collections.Generic;

namespace GraphService
{
    public partial class Graph<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        : IGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexData : IVertexData
        where TEdgeData : IEdgeData
    {
        internal class Vertex : IVertex
        {
            public TVertexKey Key { get; }
            public TVertexData Data { get; set; }
            public List<Edge> IncidentEdges { get; }
            
            public Vertex(TVertexKey key, TVertexData data)
            {
                Key = key;
                Data = data;
                IncidentEdges = new List<Edge>();
            }
        }
    }
}
