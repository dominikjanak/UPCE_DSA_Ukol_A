namespace Ukol_A
{
    public interface IEdgeData
    {
        /// <summary>
        /// Vrátí typ hrany
        /// </summary>
        /// <returns>Typ hrany</returns>
        EdgeType GetEdgeType();

        /// <summary>
        /// Vrátí délku hrany
        /// </summary>
        /// <returns>Délka hrany</returns>
        float GetDistance();
    }
}