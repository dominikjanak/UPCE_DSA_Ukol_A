using System.Collections.Generic;
using Ukol_A.DataStructures;

namespace Ukol_A.Graph
{
    class ForestGraph<TVertexKey, TEdgeKey> : IForestGraph<TVertexKey, TEdgeKey>
    {
        private bool _EmptyGraph;
        private GraphSize _boderedValues;

        internal Dictionary<string, IEdge<TEdgeKey, TVertexKey>> _edges;

        public ForestGraph()
        {
            _EmptyGraph = true;
            _boderedValues = new GraphSize();
            _edges = new Dictionary<string, IEdge<TEdgeKey, TVertexKey>>();
        }

        public List<IVertex<TVertexKey, TEdgeKey>> GetAllVertexes()
        {
            return new List<IVertex<TVertexKey, TEdgeKey>>();
        }

        public List<IEdge<TEdgeKey, TVertexKey>> GetAllEdges()
        {
            return new List<IEdge<TEdgeKey, TVertexKey>>();
        }
        public GraphSize GetGraphSize()
        {
            return new GraphSize();
        }
    }
}
