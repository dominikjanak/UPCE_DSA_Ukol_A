using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Ukol_A
{
    class ForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData> : IForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        where TVertexKey : IComparable
        where TEdgeKey : IComparable
    {
        private Dictionary<TVertexKey, Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>> _vertexes;
        private Dictionary<TEdgeKey, Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>> _edges; // Je možné držet i hrany pro optimálnější výkon

        public ForestGraph()
        {
            Clear();
        }

        public bool Clear()
        {
            _vertexes = new Dictionary<TVertexKey, Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>>();
            _edges = new Dictionary<TEdgeKey, Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>>();

            return true;
        }

        public bool IsEmpty()
        {
            return (_vertexes.Count == 0);
        }

        public int CountVertex()
        {
            return _vertexes.Count;
        }

        public int CountEdges()
        {
            return _edges.Count;
        }

        public bool AddVertex(TVertexKey key, TVertexData data)
        {
            var vertex = new Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>(key, data);
            return AddVertex(vertex);
        }

        public bool AddVertex(Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> vertex)
        {
            if (!_vertexes.ContainsKey(vertex.GetKey()))
            {
                _vertexes.Add(vertex.GetKey(), vertex);
                return true;
            }
            return false;
        }

        public Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> RemoveVertex(Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> vertex)
        {
            return RemoveVertex(vertex.GetKey());
        }

        public Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> RemoveVertex(TVertexKey key)
        {
            if (_vertexes.ContainsKey(key))
            {
                var vertex = _vertexes[key];

                foreach (var edge in vertex.GetIncidentEdges())
                {
                    RemoveEdge(edge.GetKey());
                }

                return vertex;
            }
            return null;
        }

        public bool ExistsVertex(TVertexKey startKey)
        {
            return _vertexes.ContainsKey(startKey);
        }

        public Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> FindVertex(TVertexKey key)
        {
            return _vertexes[key];
        }

        public bool AddEdge(TEdgeKey key, TVertexKey start, TVertexKey target, TEdgeData data)
        {
            var vertexStart = FindVertex(start);
            var vertexTarget = FindVertex(target);

            var edge = new Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>(key, data, vertexStart, vertexTarget);
            return AddEdge(edge);
        }

        public bool AddEdge(Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> edge)
        {
            if (ExistsEdge(edge.GetKey()))
            {
                return false;
            }

            var vertexStart = edge.GetStartVertex();
            var vertexTarget = edge.GetTargetVertex();

            if (vertexStart != null && vertexTarget != null)
            {
                if (FindEdge(vertexStart, vertexTarget) == null)
                {
                    _edges.Add(edge.GetKey(), edge);
                    vertexStart.GetIncidentEdges().Add(edge);
                    vertexTarget.GetIncidentEdges().Add(edge);
                    return true;
                }
            }
            return false;
        }

        public Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> RemoveEdge(TEdgeKey key)
        {
            var edge = FindEdge(key);
            return RemoveEdge(edge);
        }

        public Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> RemoveEdge(TVertexKey start, TVertexKey target)
        {
            var edge = FindEdge(start, target);
            return RemoveEdge(edge);
        }

        public Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> RemoveEdge(Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> edge)
        {
            if (!_edges.ContainsKey(edge.GetKey()))
            {
                return null;
            }

            var startVertex = FindVertex(edge.GetStartVertex().GetKey());
            var targetVertex = FindVertex(edge.GetTargetVertex().GetKey());

            if (startVertex != null && targetVertex != null)
            {
                startVertex.GetIncidentEdges().Remove(edge);
                targetVertex.GetIncidentEdges().Remove(edge);
                _edges.Remove(edge.GetKey());
            }

            return null;
        }

        public bool ExistsEdge(TEdgeKey key)
        {
            return _edges.ContainsKey(key);
        }

        public Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> FindEdge(TVertexKey start, TVertexKey target)
        {
            var vertexStart = FindVertex(start);
            var vertexTarget = FindVertex(target);

            return FindEdge(vertexStart, vertexTarget);
        }
        public Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> FindEdge(Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> start, 
            Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> target)
        {
            if (start != null)
            {
                foreach (var edge in start.GetIncidentEdges())
                {
                    if (edge.GetOpositeVertex(start) == target)
                    {
                        return edge;
                    }
                }
            }
            return null;
        }

        public Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> FindEdge(TEdgeKey edgeKey)
        {
            return _edges[edgeKey];
        }





        // ---------------------------------------------------------------------------------
        public List<Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>> GetAllVertexes()
        {
            return _vertexes.Select(vertex => vertex.Value).ToList();
        }

        public List<Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>> GetAllEdges()
        {
            return _edges.Select(vertex => vertex.Value).ToList();
        }
    }
}
