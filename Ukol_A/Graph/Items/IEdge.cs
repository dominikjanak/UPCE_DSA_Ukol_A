namespace Ukol_A
{
    public interface IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>
    {
        /// <summary>
        /// Vrátí klíč hrany
        /// </summary>
        /// <returns>Klíč hrany</returns>
        TEdgeKey GetKey();

        /// <summary>
        /// Manipulace s daty hrany
        /// </summary>
        TEdgeData Data { get; set; }

        /// <summary>
        /// Vrátí odkaz na počáteční vrchol hrany
        /// </summary>
        /// <returns>Vrchol</returns>
        IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> GetStartVertex();

        /// <summary>
        /// Vrátí odkaz na koncový vrchol hrany
        /// </summary>
        /// <returns>Vrchol</returns>
        IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> GetTargetVertex();

        /// <summary>
        /// Vrátí vrchol na druhém konci hrany
        /// </summary>
        /// <param name="vertex">Začítek hrany</param>
        /// <returns>Druhý vrhol hrany</returns>
        IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> GetOpositeVertex(IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> vertex);

        /// <summary>
        /// Vrátí textovou podobu hrany
        /// </summary>
        /// <returns>Hrana převedená na text</returns>
        string ToString();
    }
}