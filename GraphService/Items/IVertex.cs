using System;
using System.Collections.Generic;

namespace ForestGraph
{
    public interface IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        where TEdgeKey : IComparable
        where TVertexKey : IComparable
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