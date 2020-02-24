using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Ukol_A
{
    class ForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData> : IForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
    {
        private bool _emptyGraph;
        private int _vertexesCount;
        private int _edgesCount;
        private IGraphItemFactory<TVertexKey, TVertexData, TEdgeKey, TEdgeData> _factory;

        private Dictionary<TVertexKey, IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>> _vertexes;
        private Dictionary<TEdgeKey, IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>> _edges;
        //private Dictionary<(IVertex<TVertexKey, TVertexData> s,IVertex<TVertexKey, TVertexData> t), IEdge<TVertexKey, TVertexData, TEdgeKey, TEdgeData>> _edges;
        //        Dictionary<(IVertex, IVertex), IEdge> // bez generiky

        public ForestGraph(IGraphItemFactory<TVertexKey, TVertexData, TEdgeKey, TEdgeData> factory)
        {
            _factory = factory;
            Clear();
        }

        public bool Clear()
        {
            try
            {
                _emptyGraph = true;
                _vertexesCount = 0;
                _edgesCount = 0;

                _vertexes = new Dictionary<TVertexKey, IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>>();
                _edges = new Dictionary<TEdgeKey, IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>>();

                return true;
            }
            catch { }

            return false;
        }

        public bool IsEmpty()
        {
            return _emptyGraph;
        }

        public int CountVertex()
        {
            return _vertexesCount;
        }

        public int CountEdges()
        {
            return _edgesCount;
        }

        public bool AddVertex(TVertexKey key, TVertexData data)
        {
            if (!_vertexes.ContainsKey(key)) 
            {
                var vertex = _factory.CreateVertex(key, data);
                _vertexes.Add(key, vertex);
                _vertexesCount++;
                return true;
            }
            return false;
        }

        public IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> RemoveVertex(TVertexKey key)
        {
            if (_vertexes.ContainsKey(key))
            {
                var vertex = _vertexes[key];
                _vertexes.Remove(key);
                _vertexesCount--;
                return vertex;
            }
            return null;
        }

        public bool ExistsVertex(TVertexKey startKey)
        {
            return _vertexes.ContainsKey(startKey);
        }

        public IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> FindVertex(TVertexKey key)
        {
            return _vertexes[key];
        }








        // ---------------------------------------------------------------------------------
        public List<IVertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>> GetAllVertexes()
        {
            return _vertexes.Select(vertex => vertex.Value).ToList();
        }

        public List<IEdge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>> GetAllEdges()
        {
            return _edges.Select(vertex => vertex.Value).ToList();
        }
    }
}
