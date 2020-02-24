using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ukol_A
{
    public interface IForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
    {
        /// <summary>
        /// Vyprázdní graf a smaže všechna data
        /// </summary>
        /// <returns>Zda se podařilo vyprázdnit graf</returns>
        bool Clear();

        /// <summary>
        /// Zjištění zda je graf prázný
        /// </summary>
        /// <returns>true = prázdný graf</returns>
        bool IsEmpty();

        /// <summary>
        /// Počet vrcholů grafu
        /// </summary>
        /// <returns>Počet vrcholů</returns>
        int CountVertex();

        /// <summary>
        /// Počet hran grafu
        /// </summary>
        /// <returns>Počet hran</returns>
        int CountEdges();
        
        /// <summary>
        /// Přidat vrchol grafu
        /// </summary>
        /// <param name="key">ID vrcholu</param>
        /// <param name="data">Data vrcholu</param>
        /// <returns>Zda se podařilo přidat vrchol</returns>
        bool AddVertex(TVertexKey key, TVertexData data);

        /// <summary>
        /// Odstranit vrchol
        /// </summary>
        /// <param name="key">ID vrcholu</param>
        /// <returns>Vyjmutý vrchol</returns>
        IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> RemoveVertex(TVertexKey key);

        /// <summary>
        /// Ověří zda vrchol existuje
        /// </summary>
        /// <param name="key">ID vrcholu</param>
        /// <returns>Informace o existenci vrcholu</returns>
        bool ExistsVertex(TVertexKey key);

        /// <summary>
        /// Najít a získat vrchol
        /// </summary>
        /// <param name="key">ID vrcholu</param>
        /// <returns>Vrchol grafu</returns>
        IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> FindVertex(TVertexKey key);


        //void AddEdge(TVertexData startKey, TVertexData targetKey, TEdgeKey edgeKey, IEdge edge);

        //IEdge RemoveEdge(TVertexData startKey, TVertexData targetKey);




        //IEdge FindEdge(TVertexKey startKey, TVertexKey targetKey);
        //IEdge FindEdge(TEdgeKey edgeKey);


        List<IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>> GetAllVertexes();
        List<IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>> GetAllEdges();
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