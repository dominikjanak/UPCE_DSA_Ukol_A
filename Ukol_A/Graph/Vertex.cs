using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ukol_A.DataStructures.Enums;

namespace Ukol_A.Graph
{
    class Vertex<TVertexKey, TEdgeKey> : IVertex<TVertexKey, TEdgeKey>
    {
        public PointF GetLocation()
        {
            return new PointF();
        }
        public VertexType GetType()
        {
            return VertexType.Crossroads;
        }
        public string GetLabel()
        {
            return "";
        }
    }
}
