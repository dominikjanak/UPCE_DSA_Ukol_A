namespace GraphService
{
    public interface IEdgeData
    {
        /// <summary>
        /// Ověží, zda je hrana blokována
        /// </summary>
        /// <returns>Informace o blokaci</returns>
        bool IsBlocked();

        /// <summary>
        /// Vrátí délku hrany
        /// </summary>
        /// <returns>hodnota délky hrany</returns>
        float GetDistance();
    }
}
