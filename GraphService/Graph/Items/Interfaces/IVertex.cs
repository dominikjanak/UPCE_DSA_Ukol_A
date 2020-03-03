using System;
using System.Collections.Generic;

namespace GraphService
{
    public interface IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexData : IVertexData
        where TEdgeData : IEdgeData
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
        List<Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>> IncidentEdges { get; }
    }
}