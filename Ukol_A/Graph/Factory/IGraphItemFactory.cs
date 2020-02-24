namespace Ukol_A
{
    interface IGraphItemFactory<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
    {
        /// <summary>
        /// Vytvoř nový vrchol grafu
        /// </summary>
        /// <param name="key">ID vrcholu</param>
        /// <param name="data">Data vrcholu</param>
        /// <returns>Vrchol</returns>
        IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> CreateVertex(TVertexKey key, TVertexData data);

        /// <summary>
        /// Vytvoř novou hranu grafu
        /// </summary>
        /// <param name="key">ID hrany</param>
        /// <param name="data">Data hrany</param>
        /// <param name="start">Počátečná vrchol</param>
        /// <param name="end">Koncový vrchol hrany</param>
        /// <returns>Hrana</returns>
        IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> CreateEdge(TEdgeKey key, TEdgeData data, 
            IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> start, 
            IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> end);
    }
}
