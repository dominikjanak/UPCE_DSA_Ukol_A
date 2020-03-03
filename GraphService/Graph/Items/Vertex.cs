using System;
using System.Collections.Generic;

namespace GraphService
{
    public class Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        : IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexData : IVertexData
        where TEdgeData : IEdgeData
    {
        public TVertexKey Key { get; }
        public TVertexData Data { get; set; }
        public List<Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>> IncidentEdges { get; }

        public Vertex(TVertexKey key, TVertexData data)
        {
            Key = key;
            Data = data;
            IncidentEdges = new List<Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>>();
        }

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}
