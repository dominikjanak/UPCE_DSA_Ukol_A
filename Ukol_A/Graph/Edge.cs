using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ukol_A.DataStructures.Enums;

namespace Ukol_A.Graph
{
    class Edge<TEdgeKey, TVertexKey> : IEdge<TEdgeKey, TVertexKey>
    {
        private IVertex<TVertexKey, TEdgeKey> source;
        private IVertex<TVertexKey, TEdgeKey> destination;

        public IVertex<TVertexKey, TEdgeKey> GetStartVertex()
        {
            return null;
        }
        public IVertex<TVertexKey, TEdgeKey> GetTargetVertex()
        {
            return null;
        }

        public EdgeType GetType()
        {
            return EdgeType.Free;
        }
    }
}
