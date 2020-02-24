using System.Drawing;

namespace Ukol_A
{
    public interface IVertexData
    {
        /// <summary>
        /// Vrátí souřadnice vrcholu
        /// </summary>
        /// <returns>Souřadnice vrcholu</returns>
        PointF GetLocation();

        /// <summary>
        /// Vrátí typ vrcholu
        /// </summary>
        /// <returns>Typ vrcholu</returns>
        VertexType GetVertexType();
    }
}