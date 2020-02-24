using System.Collections.Generic;
using System.Drawing;

namespace Ukol_A
{
    public interface IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
    {
        /// <summary>
        /// Vrátí klíč vrcholu
        /// </summary>
        /// <returns>Klíč vrcholu</returns>
        TVertexKey GetKey();

        /// <summary>
        /// Vrátí seznam incidenčních vrcholů a klíče incidenčních hran
        /// </summary>
        /// <returns>List incidenčních vrcholů a klíčů hran</returns>
        List<(IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>, IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>)> GetIncidentVertexes(); 
        //void AddEdge();
        //IEdge<TEdgeData, TVertexData> RemoveEdge();

        /// <summary>
        /// Manipulace s daty vrcholu
        /// </summary>
        TVertexData Data { get; set; }

        /// <summary>
        /// Vrátí textovou podobu vrcholu
        /// </summary>
        /// <returns>Vrchol převedený na text</returns>
        string ToString();
    }
}