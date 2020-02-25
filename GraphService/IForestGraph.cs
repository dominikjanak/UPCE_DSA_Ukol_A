using System;
using System.Collections.Generic;

namespace ForestGraph
{
    public interface IForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData> 
        where TVertexKey : IComparable 
        where TEdgeKey : IComparable
    {
        /// <summary>
        /// Vymaže všechna data
        /// </summary>
        void Clear();

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
        /// Vrátí data všech vrcholů
        /// </summary>
        /// <returns>Data o všech vrcholech</returns>
        List<(TVertexKey key, TVertexData data)> GetAllVertexes();

        /// <summary>
        /// Vrátí data všech hran
        /// </summary>
        /// <returns>Data o všech hranách</returns>
        List<(TEdgeKey key, TEdgeData data, TVertexKey start, TVertexKey target)> GetAllEdges();

        /// <summary>
        /// Vrátí incidentní hrany pro vrchol
        /// </summary>
        /// <param name="key">Klíč vrcholu</param>
        /// <returns>Data o všech incidentních hranách</returns>
        List<(TEdgeKey key, TEdgeData data, TVertexKey target)> GetVertexIncidents(TVertexKey key);

        /// <summary>
        /// Vrátí incidentní vrcholy pro hranu
        /// </summary>
        /// <param name="key">Klíč vrcholu</param>
        /// <returns>Data o všech incidentních vrcholech</returns>
        List<(TVertexKey key, TVertexData data)> GetEdgeIncidents(TEdgeKey key);

        /// <summary>
        /// Vrátí incidentní vrcholy pro hranu
        /// </summary>
        /// <param name="start">Klíč počátečního vrcholu</param>
        /// <param name="target">Klíč koncového vrcholu</param>
        /// <returns>Data o všech incidentních vrcholech</returns>
        List<(TVertexKey key, TVertexData data)> GetEdgeIncidents(TVertexKey start, TVertexKey target);


    }
}