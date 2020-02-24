using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ukol_A
{
    public interface IForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData> 
        where TVertexKey : IComparable 
        where TEdgeKey : IComparable
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
        /// Přidat vrchol grafu
        /// </summary>
        /// <param name="vertex">Vrchol grafu</param>
        /// <returns>Zda se podařilo přidat vrchol</returns>
        bool AddVertex(Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> vertex);

        /// <summary>
        /// Odstranit vrchol
        /// </summary>
        /// <param name="key">ID vrcholu</param>
        /// <returns>Vyjmutý vrchol</returns>
        Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> RemoveVertex(TVertexKey key);

        /// <summary>
        /// Odstranit vrchol
        /// </summary>
        /// <param name="vertex">Vrchol</param>
        /// <returns>Vyjmutý vrchol</returns>
        Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> RemoveVertex(Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> vertex);

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
        Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> FindVertex(TVertexKey key);

        /// <summary>
        /// Přidat novou hranu
        /// </summary>
        /// <param name="key">Klíč hrany</param>
        /// <param name="start">Klíš počátečního vrcholu</param>
        /// <param name="target">Klíč koncového vrcholu</param>
        /// <param name="data">Data hrany</param>
        /// <returns>Zda se podařilo vložit hranu</returns>
        bool AddEdge(TEdgeKey key, TVertexKey start, TVertexKey target, TEdgeData data);

        /// <summary>
        /// Přidat novou hranu
        /// </summary>
        /// <param name="edge">Hrana</param>
        /// <returns>Zda se podařilo vložit hranu</returns>
        bool AddEdge(Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> edge);

        /// <summary>
        /// Odstranit hranu grafu
        /// </summary>
        /// <param name="key">Klíč hrany</param>
        /// <returns>Vyjmutá hrana</returns>
        Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> RemoveEdge(TEdgeKey key);

        /// <summary>
        /// Odstranit hranu grafu
        /// </summary>
        /// <param name="start">Klíč počátečního vrcholu</param>
        /// <param name="target">Klíč koncového vrcholu</param>
        /// <returns>Vyjmutá hrana</returns>
        Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> RemoveEdge(TVertexKey start, TVertexKey target);

        /// <summary>
        /// Odstranit hranu grafu
        /// </summary>
        /// <param name="edge">Hrana k odstranění</param>
        /// <returns>Vyjmutá hrana</returns>
        Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> RemoveEdge(Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> edge);

        /// <summary>
        /// Ověří zda hrana existuje
        /// </summary>
        /// <param name="key">ID hrany</param>
        /// <returns>Informace o existenci hrany</returns>
        bool ExistsEdge(TEdgeKey key);

        /// <summary>
        /// Najít hranu
        /// </summary>
        /// <param name="start">Klíč počátečního vrcholu</param>
        /// <param name="target">Klíč koncového vrcholu</param>
        /// <returns>Nalezená hrana</returns>
        Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> FindEdge(TVertexKey start, TVertexKey target);

        /// <summary>
        /// Najít hranu
        /// </summary>
        /// <param name="start">Počáteční vrchol</param>
        /// <param name="target">Koncový vrchol</param>
        /// <returns>Nalezená hrana</returns>
        Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> FindEdge(Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> start, Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> target);

        /// <summary>
        /// Najít hranu
        /// </summary>
        /// <param name="edgeKey">Klíč hrany</param>
        /// <returns>Nalezená hrana</returns>
        Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> FindEdge(TEdgeKey edgeKey);








        List<Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>> GetAllVertexes();
        List<Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>> GetAllEdges();
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