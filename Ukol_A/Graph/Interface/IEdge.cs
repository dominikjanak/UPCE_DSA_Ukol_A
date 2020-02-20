using Ukol_A.DataStructures.Enums;

namespace Ukol_A.Graph
{
    public interface IEdge<TEdgeKey, TVertexKey>
    {
        IVertex<TVertexKey, TEdgeKey> GetStartVertex();
        IVertex<TVertexKey, TEdgeKey> GetTargetVertex();

        EdgeType GetType();
    }
}