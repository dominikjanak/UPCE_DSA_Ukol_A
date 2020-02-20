namespace Ukol_A
{
    public interface IEdge<TEdgeData, TVertexData>
    {
        string GetKey();
        TEdgeData Data { get; set; }
        IVertex<TVertexData, TEdgeData> GetStartVertex();
        IVertex<TVertexData, TEdgeData> GetTargetVertex();
        string GetLabel();
        string ToString();
    }
}