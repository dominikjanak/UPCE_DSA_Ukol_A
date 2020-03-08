using System;

namespace GraphService
{
    public partial class Graph<TVertexKey, TVertexValue, TEdgeKey, TEdgeValue>
        : IGraph<TVertexKey, TVertexValue, TEdgeKey, TEdgeValue>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexValue : IVertexData
        where TEdgeValue : IEdgeData
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
            TEdgeValue Data { get; set; }

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