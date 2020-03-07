using System;
using System.Collections.Generic;

namespace GraphService
{
    public interface IGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexData : IVertexData
        where TEdgeData : IEdgeData
    {
        /// <summary>
        /// Vymaže všechna data
        /// </summary>
        void Clear();

        /// <summary>
        /// Počet vrcholů grafu
        /// </summary>
        /// <returns>Počet vrcholů</returns>
        int VerticesCount();

        /// <summary>
        /// Počet hran grafu
        /// </summary>
        /// <returns>Počet hran</returns>
        int EdgesCount();

        /// <summary>
        /// Zjistí zda je graf prázný
        /// </summary>
        /// <returns>Prázdný graf</returns>
        bool IsEmpty();

        /// <summary>
        /// Přidá vrchol grafu
        /// </summary>
        /// <param name="key">Klíč vrcholu</param>
        /// <param name="data">Data vrcholu</param>
        void AddVertex(TVertexKey key, TVertexData data);

        /// <summary>
        /// Zjistí zda vrchol s klíčem existuje
        /// </summary>
        /// <param name="key">Klíč vrcholu</param>
        /// <returns>Existuje vrchol</returns>
        bool HasVertex(TVertexKey key);

        /// <summary>
        /// Najde vrchol grafu
        /// </summary>
        /// <param name="key">Klíč vrcholu</param>
        /// <returns>Data vrcholu</returns>
        TVertexData FindVertex(TVertexKey key);

        /// <summary>
        /// Odstraní vrchol
        /// </summary>
        /// <param name="key">Klíč vrcholu</param>
        /// <returns>Data vrcholu</returns>
        TVertexData RemoveVertex(TVertexKey key);

        /// <summary>
        /// Přidá hranu grafu
        /// </summary>
        /// <param name="key">Klíč hrany</param>
        /// <param name="start">Klíč počátečního vrcholu</param>
        /// <param name="target">Klíč koncového vrcholu</param>
        /// <param name="data">Data hrany</param>
        void AddEdge(TEdgeKey key, TVertexKey start, TVertexKey target, TEdgeData data);

        /// <summary>
        /// Zjistí zda hrana s klíčem existuje
        /// </summary>
        /// <param name="key">Klíč hrany</param>
        /// <returns>Existuje hrana</returns>
        bool HasEdge(TEdgeKey key);

        /// <summary>
        /// Zjistí zda existuje hrana mezi dvěma body
        /// </summary>
        /// <param name="start">Počáteční vrchol</param>
        /// <param name="target">Cílový vrchol</param>
        /// <returns>Existuje hrana</returns>
        bool HasEdge(TVertexKey start, TVertexKey target);

        /// <summary>
        /// Najde hranu grafu
        /// </summary>
        /// <param name="key">Klíč hrany</param>
        /// <returns>Data hrany</returns>
        TEdgeData FindEdge(TEdgeKey key);

        /// <summary>
        /// Najde hranu grafu
        /// </summary>
        /// <param name="start">Klíč počátečního vrcholu</param>
        /// <param name="target">Klíč koncového vrcholu</param>
        /// <returns>Data hrany</returns>
        TEdgeData FindEdge(TVertexKey start, TVertexKey target);

        /// <summary>
        /// Odstraní hranu grafu
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Data hrany</returns>
        TEdgeData RemoveEdge(TEdgeKey key);

        /// <summary>
        /// Odstraní hranu grafu
        /// </summary>
        /// <param name="start">Klíč počátečního vrcholu</param>
        /// <param name="target">Klíč koncového vrcholu</param>
        /// <returns>Data hrany</returns>
        TEdgeData RemoveEdge(TVertexKey start, TVertexKey target);

        /// <summary>
        /// Získá iterátor pro všechny vrcholy
        /// </summary>
        IEnumerable<(TVertexKey Key, TVertexData Data)> Vertices { get; }

        /// <summary>
        /// Získá iterátor pro všechny hrany
        /// </summary>
        IEnumerable<(TEdgeKey Key, TVertexKey Start, TVertexKey Target, TEdgeData Data)> Edges { get; }

        /// <summary>
        /// Vrátí iterátor přes incidentní hrany pro vrchol
        /// </summary>
        /// <param name="key">Klíč vrcholu</param>
        /// <returns>Vrátí iterátor přes incidentní hrany</returns>
        IEnumerable<(TEdgeKey Key, TEdgeData Data, TVertexKey Target)> VertexIncidents(TVertexKey key);

        /// <summary>
        /// Vrátí iterátor přes incidentní vrcholy pro hranu
        /// </summary>
        /// <param name="key">Klíč vrcholu</param>
        /// <returns>Vrátí iterátor přes incidentní vrcholy</returns>
        IEnumerable<(TVertexKey Key, TVertexData Data)> EdgeIncidents(TEdgeKey key);

        /// <summary>
        /// Vrátí iterátor přes incidentní vrcholy pro hranu
        /// </summary>
        /// <param name="start">Klíč počátečního vrcholu</param>
        /// <param name="target">Klíč koncového vrcholu</param>
        /// <returns>Vrátí iterátor přes incidentní vrcholy na hraně</returns>
        IEnumerable<(TVertexKey Key, TVertexData Data)> EdgeIncidents(TVertexKey start, TVertexKey target);
    }
}