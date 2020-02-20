using System.Collections.Generic;
using System.Drawing;

namespace Ukol_A
{
    public interface IVertex<TVertexData, TEdgeData>
    {
        string GetKey();
        List<IEdge<TEdgeData, TVertexData>> GetEdges();
        //void AddEdge();
        //IEdge<TEdgeData, TVertexData> RemoveEdge();
        PointF GetLocation();
        TVertexData Data { get; set; }
        string GetLabel();
        string GetCoodrinations();
        string ToString();
    }
}