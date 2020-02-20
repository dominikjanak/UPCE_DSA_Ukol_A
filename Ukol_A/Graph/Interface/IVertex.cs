using System.Collections.Generic;
using System.Drawing;
using Ukol_A.DataStructures.Enums;

namespace Ukol_A.Graph
{
    public interface IVertex<TVertexKey, TEdgeKey>
    {
        PointF GetLocation();
        VertexType GetType();
        string GetLabel();
    }
}