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
        internal interface IVertex
        {
            /// <summary>
            /// Vertex key object
            /// </summary>
            TVertexKey Key { get; }

            /// <summary>
            /// Data object
            /// </summary>
            TVertexData Data { get; set; }

            /// <summary>
            /// List of incident edges
            /// </summary>
            List<Edge> IncidentEdges { get; }
        }
    }
}