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
        internal interface IEdge
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
            Vertex StartVertex { get; set; }

            /// <summary>
            /// Edge target vertex
            /// </summary>
            Vertex TargetVertex { get; }

            /// <summary>
            /// Edge target vertex
            /// </summary>
            /// <param name="vertex">First vertex</param>
            /// <returns>Second vertex</returns>
            Vertex GetOpositeVertex(Vertex vertex);
        }
    }
}