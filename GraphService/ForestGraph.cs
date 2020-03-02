using GraphService.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForestGraph
{
    public class ForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData> 
        : IForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
    {
        private Dictionary<TVertexKey, Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>> _vertices;
        private Dictionary<TEdgeKey, Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>> _edges;

        public ForestGraph()
        {
            Clear();
        }

        public void Clear()
        {
            _vertices = new Dictionary<TVertexKey, Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>>();
            _edges = new Dictionary<TEdgeKey, Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>>();
        }

        public bool IsEmpty()
        {
            return (_vertices.Count == 0);
        }

        public int VerticesCount()
        {
            return _vertices.Count;
        }

        public int EdgesCount()
        {
            return _edges.Count;
        }

        public void AddVertex(TVertexKey key, TVertexData data)
        {
            var vertex = new Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData>(key, data);
            AddVertex(vertex);
        }

        internal void AddVertex(Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> vertex)
        {
            if (_vertices.ContainsKey(vertex.Key))
            {
                throw new UniqueKeyException(Resources.KeyAlreadyExists);
            }

            _vertices.Add(vertex.Key, vertex);
        }

        public TVertexData RemoveVertex(TVertexKey key)
        {
            var vertex = GetVertex(key); 
            return RemoveVertex(vertex);            
        }

        internal TVertexData RemoveVertex(Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> vertex)
        {
            if (vertex == null)
            {
                throw new ItemNotFoundException(Resources.VertexNotFound);
            }            

            List<TEdgeKey> edges = vertex.IncidentEdges.Select(edge => edge.Key).ToList();

            foreach (var edge in edges)
            {
                RemoveEdge(edge);
            }

            TVertexData data = vertex.Data;
            _vertices.Remove(vertex.Key);
            return data;
        }

        public bool HasVertex(TVertexKey key)
        {
            return _vertices.ContainsKey(key);
        }

        internal Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> GetVertex(TVertexKey key)
        {
            Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> vertex = null;
            if (_vertices.TryGetValue(key, out vertex))
            {
                return vertex;
            }
            return null;
        }

        public TVertexData FindVertex(TVertexKey key)
        {
            var vertex = GetVertex(key);
            if (vertex != null)
            {
                return vertex.Data;
            }
            return default;
        }

        public void AddEdge(TEdgeKey key, TVertexKey start, TVertexKey target, TEdgeData data)
        {
            var vertexStart = GetVertex(start);
            var vertexTarget = GetVertex(target);

            var edge = new Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData>(key, data, vertexStart, vertexTarget);
            AddEdge(edge);
        }

        internal void AddEdge(Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> edge)
        {
            if (HasEdge(edge.Key))
            {
                throw new UniqueKeyException(Resources.KeyAlreadyExists);
            }

            var startVertex = edge.StartVertex;
            var targetVertex = edge.TargetVertex;

            if (startVertex == null || targetVertex == null)
            {
                throw new ItemNotFoundException(Resources.VerticesNotExist);
            }

            if (GetEdge(startVertex, targetVertex) != null)
            {
                throw new UniqueItemException(Resources.EdgeExists);
            }

            _edges.Add(edge.Key, edge);
            startVertex.IncidentEdges.Add(edge);
            targetVertex.IncidentEdges.Add(edge);
        }

        public TEdgeData RemoveEdge(TEdgeKey key)
        {
            var edge = GetEdge(key);
            return RemoveEdge(edge);
        }

        public TEdgeData RemoveEdge(TVertexKey start, TVertexKey target)
        {
            var edge = GetEdge(start, target);
            return RemoveEdge(edge);
        }

        internal TEdgeData RemoveEdge(Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> edge)
        {
            if (edge == null)
            {
                throw new ItemNotFoundException(Resources.EdgeNotFound);
            }

            var startVertex = edge.StartVertex;
            var targetVertex = edge.TargetVertex;

            if (startVertex == null || targetVertex == null)
            {
                throw new ItemNotFoundException(Resources.VerticesNotExist);
            }

            startVertex.IncidentEdges.Remove(edge);
            targetVertex.IncidentEdges.Remove(edge);

            TEdgeData data = edge.Data;
            _edges.Remove(edge.Key);

            return data;
        }

        public bool HasEdge(TEdgeKey key)
        {
            return _edges.ContainsKey(key);
        }

        public bool HasEdge(TVertexKey start, TVertexKey target)
        {
            return (FindEdge(start, target) != null);
        }

        internal Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> GetEdge(TVertexKey start, TVertexKey target)
        {
            var vertexStart = GetVertex(start);
            var vertexTarget = GetVertex(target);

            return GetEdge(vertexStart, vertexTarget);
        }

        internal Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> GetEdge(
            Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> start,
            Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> target)
        {
            if (start == null || target == null)
            {
                return null;
            }

            return start.IncidentEdges.FirstOrDefault(edge => edge.GetOpositeVertex(start) == target);
        }

        internal Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> GetEdge(TEdgeKey key)
        {
            Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> edge = null;

            if (_edges.TryGetValue(key, out edge))
            {
                return edge;
            }
            return null;
        }

        public TEdgeData FindEdge(TEdgeKey key)
        {
            var edge = GetEdge(key);
            return FindEdge(edge);
        }

        public TEdgeData FindEdge(TVertexKey start, TVertexKey target)
        {
            var edge = GetEdge(start, target);
            return FindEdge(edge);
        }

        private TEdgeData FindEdge(Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> edge)
        {
            if (edge != null)
            {
                return edge.Data;
            }
            return default;
        }

        public List<(TVertexKey key, TVertexData data)> GetAllVertices()
        {
            var allVertices = new List<(TVertexKey key, TVertexData data)>();

            foreach (var vertex in _vertices)
            {
                allVertices.Add((vertex.Key, vertex.Value.Data));
            }

            return allVertices;
        }

        public List<(TEdgeKey key, TEdgeData data, TVertexKey start, TVertexKey target)> GetAllEdges()
        {
            var allEdge = new List<(TEdgeKey key, TEdgeData data, TVertexKey start, TVertexKey target)>();

            foreach (var edge in _edges)
            {
                allEdge.Add((edge.Key, edge.Value.Data, edge.Value.StartVertex.Key, edge.Value.TargetVertex.Key));
            }

            return allEdge;
        }

        public List<(TEdgeKey key, TEdgeData data, TVertexKey target)> GetVertexIncidents(TVertexKey key)
        {
            var data = new List<(TEdgeKey key, TEdgeData data, TVertexKey target)>();
            var vertex = GetVertex(key);

            if (vertex == null)
            {
                return null;
            }

            var incidentEdges = vertex.IncidentEdges;
            foreach (var edge in incidentEdges)
            {
                data.Add((edge.Key, edge.Data, edge.TargetVertex.Key));
            }
            return data;
        }

        public List<(TVertexKey key, TVertexData data)> GetEdgeIncidents(TEdgeKey key)
        {
            var edge = GetEdge(key);
            return GetEdgeIncidents(edge);
        }

        public List<(TVertexKey key, TVertexData data)> GetEdgeIncidents(TVertexKey start, TVertexKey target)
        {
            var edge = GetEdge(start, target);
            return GetEdgeIncidents(edge);
        }

        private List<(TVertexKey key, TVertexData data)> GetEdgeIncidents(Edge<TEdgeKey, TEdgeData, TVertexKey, TVertexData> edge)
        {
            if (edge == null)
            {
                return null;
            }

            return new List<(TVertexKey key, TVertexData data)>()
            {
                (edge.StartVertex.Key, edge.StartVertex.Data),
                (edge.TargetVertex.Key, edge.TargetVertex.Data)
            };
        }
    }
}
