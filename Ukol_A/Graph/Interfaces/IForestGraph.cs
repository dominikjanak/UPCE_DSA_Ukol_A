using System.Collections.Generic;
using System.Windows.Forms;

namespace Ukol_A
{
    public interface IForestGraph<TVertexData, TEdgeData>
    {
        /*bool IsEmpty();
        int CountVertex();
        int CountEdges();

        void AddVertex(TVertexKey vertexKey, IVertex vertex);
        void AddEdge(TVertexKey startKey, TVertexKey targetKey, TEdgeKey edgeKey, IEdge edge);

        IVertex RemoveVertex(TVertexKey vertexKey);
        IEdge RemoveEdge(TVertexKey startKey, TVertexKey targetKey);

        IVertex FindVertex(TVertexKey startKey);
        IEdge FindEdge(TVertexKey startKey, TVertexKey targetKey);
        IEdge FindEdge(TEdgeKey edgeKey);

        */
        List<IVertex<TVertexData, TEdgeData>> GetAllVertexes();
        List<IEdge<TEdgeData, TVertexData>> GetAllEdges();
        GraphSize GetGraphSize();
        /*

        List<IVertex> GetSuccessors(TVertexKey vertexKey);
        List<IVertex> GetPredecessors(TVertexKey vertexKey);

        List<IVertex> GetIncidenceElements(TVertexKey vertexKey);
        List<IVertex> GetIncidenceElements(TEdgeKey edgeKey);
        List<IVertex> GetIncidenceElements(TVertexKey startKey, TVertexKey targetKey);

        void DefineGate(TVertexKey vertexKey);
        void DeleteGate(TVertexKey vertexKey);
        List<IVertex> GetGates();*/
    }
}