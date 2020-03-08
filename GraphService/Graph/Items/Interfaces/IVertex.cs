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
        internal interface IVertex
        {
            /// <summary>
            /// Vertex key object
            /// </summary>
            TVertexKey Key { get; }

            /// <summary>
            /// Data object
            /// </summary>
            TVertexValue Data { get; set; }

            /// <summary>
            /// List of incident edges
            /// </summary>
            List<Edge> IncidentEdges { get; }
        }
    }
}