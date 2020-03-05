using GraphService.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphService
{
    public partial class Graph<TVertexKey, TVertexData, TEdgeKey, TEdgeData> 
        : IGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexData : IVertexData
        where TEdgeData : IEdgeData
    {
        private Dictionary<TVertexKey, Vertex> _vertices;
        private Dictionary<TEdgeKey, Edge> _edges;

        public Graph()
        {
            Clear();
        }

        public void Clear()
        {
            _vertices = new Dictionary<TVertexKey, Vertex>();
            _edges = new Dictionary<TEdgeKey, Edge>();
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
            var vertex = new Vertex(key, data);
            AddVertex(vertex);
        }

        internal void AddVertex(Vertex vertex)
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

        internal TVertexData RemoveVertex(Vertex vertex)
        {
            if (vertex == null)
            {
                throw new ItemNotFoundException(Resources.VertexNotFound);
            }

            // Be careful: Collection cannot be edited during foreach (need copy of collection)
            List<Edge> edgesToRemove = new List<Edge>(vertex.IncidentEdges);
            foreach (var edge in edgesToRemove)
            {
                RemoveEdge(edge);
            }

            _vertices.Remove(vertex.Key);
            return vertex.Data;
        }

        public bool HasVertex(TVertexKey key)
        {
            return _vertices.ContainsKey(key);
        }

        internal Vertex GetVertex(TVertexKey key)
        {
            Vertex vertex = null;
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

            var edge = new Edge(key, data, vertexStart, vertexTarget);
            AddEdge(edge);
        }

        internal void AddEdge(Edge edge)
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

        internal TEdgeData RemoveEdge(Edge edge)
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

            _edges.Remove(edge.Key);
            return edge.Data;
        }

        public bool HasEdge(TEdgeKey key)
        {
            return _edges.ContainsKey(key);
        }

        public bool HasEdge(TVertexKey start, TVertexKey target)
        {
            return (FindEdge(start, target) != null);
        }

        internal Edge GetEdge(TEdgeKey key)
        {
            Edge edge = null;

            if (_edges.TryGetValue(key, out edge))
            {
                return edge;
            }
            return null;
        }

        internal Edge GetEdge(TVertexKey start, TVertexKey target)
        {
            var vertexStart = GetVertex(start);
            var vertexTarget = GetVertex(target);

            return GetEdge(vertexStart, vertexTarget);
        }

        internal Edge GetEdge(
            Vertex start,
            Vertex target)
        {
            if (start == null || target == null)
            {
                return null;
            }

            return start.IncidentEdges.FirstOrDefault(edge => edge.GetOpositeVertex(start) == target);
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

        private TEdgeData FindEdge(Edge edge)
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
            var vertex = GetVertex(key);
            if (vertex == null)
            {
                return null;
            }

            var data = new List<(TEdgeKey key, TEdgeData data, TVertexKey target)>();
            var incidentEdges = vertex.IncidentEdges;

            foreach (var edge in incidentEdges)
            {
                //data.Add((edge.Key, edge.Data, edge.TargetVertex.Key));
                data.Add((edge.Key, edge.Data, edge.GetOpositeVertex(vertex).Key));
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

        internal List<(TVertexKey key, TVertexData data)> GetEdgeIncidents(Edge edge)
        {
            var data = new List<(TVertexKey key, TVertexData data)>();

            if (edge == null)
            {
                return data;
            }

            data.Add((edge.StartVertex.Key, edge.StartVertex.Data));
            data.Add((edge.TargetVertex.Key, edge.TargetVertex.Data));

            return data;
        }
    }
}
