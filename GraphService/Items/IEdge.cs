using System;

namespace ForestGraph
{
    public interface IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>
        where TEdgeKey : IComparable
        where TVertexKey : IComparable
    {
        /// <summary>
        /// Edge key object
        /// </summary>
        TEdgeKey Key { get; }

        /// <summary>
        /// Edge data object
        /// </summary>
        TEdgeData Data { get; set; }

        /// <summary>
        /// Edge start vertex
        /// </summary>
        Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> StartVertex { get; set; }

        /// <summary>
        /// Edge target vertex
        /// </summary>
        Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> TargetVertex { get; }

        /// <summary>
        /// Edge target vertex
        /// </summary>
        /// <param name="vertex">First vertex</param>
        /// <returns>Second vertex</returns>
        Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> GetOpositeVertex(Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> vertex);
    }
}