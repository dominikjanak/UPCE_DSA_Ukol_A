using System;

namespace GraphService
{
    public interface IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexData : IVertexData
        where TEdgeData : IEdgeData
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