using GraphService.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphService
{
    public partial class Graph<TVertexKey, TVertexValue, TEdgeKey, TEdgeValue> 
        : IGraph<TVertexKey, TVertexValue, TEdgeKey, TEdgeValue>
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexValue : IVertexData
        where TEdgeValue : IEdgeData
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

        public void AddVertex(TVertexKey key, TVertexValue data)
        {
            var vertex = new Vertex(key, data);
            AddVertex(vertex);
        }

        internal void AddVertex(Vertex vertex)
        {
            if (vertex.Key == null || vertex.Data == null)
            {
                throw new ArgumentNullException();
            }

            if (_vertices.ContainsKey(vertex.Key))
            {
                throw new UniqueKeyException(Resources.KeyAlreadyExists);
            }

            _vertices.Add(vertex.Key, vertex);
        }

        public TVertexValue RemoveVertex(TVertexKey key)
        {
            var vertex = GetVertex(key); 
            return RemoveVertex(vertex);            
        }

        internal TVertexValue RemoveVertex(Vertex vertex)
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

        public TVertexValue FindVertex(TVertexKey key)
        {
            var vertex = GetVertex(key);
            if (vertex != null)
            {
                return vertex.Data;
            }
            return default(TVertexValue);
        }

        public void AddEdge(TEdgeKey key, TVertexKey start, TVertexKey target, TEdgeValue data)
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

            if (edge.Data == null)
            {
                throw new ArgumentNullException();
            }

            var startVertex = edge.StartVertex;
            var targetVertex = edge.TargetVertex;

            if (startVertex == null || targetVertex == null)
            {
                throw new ItemNotFoundException(Resources.VerticesNotExist);
            }

            if (edge.StartVertex.Key.CompareTo(edge.TargetVertex.Key) == 0)
            {
                throw new EdgeWithSameVerticesException(Resources.EdgeWithSameVertices);
            }

            if (GetEdge(startVertex, targetVertex) != null)
            {
                throw new UniqueItemException(Resources.EdgeExists);
            }

            _edges.Add(edge.Key, edge);
            startVertex.IncidentEdges.Add(edge);
            targetVertex.IncidentEdges.Add(edge);
        }

        public TEdgeValue RemoveEdge(TEdgeKey key)
        {
            var edge = GetEdge(key);
            return RemoveEdge(edge);
        }

        public TEdgeValue RemoveEdge(TVertexKey start, TVertexKey target)
        {
            var edge = GetEdge(start, target);
            return RemoveEdge(edge);
        }

        internal TEdgeValue RemoveEdge(Edge edge)
        {
            if (edge == null)
            {
                throw new ItemNotFoundException(Resources.EdgeNotFound);
            }

            var startVertex = edge.StartVertex;
            var targetVertex = edge.TargetVertex;
            
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

        public TEdgeValue FindEdge(TEdgeKey key)
        {
            var edge = GetEdge(key);
            return FindEdge(edge);
        }

        public TEdgeValue FindEdge(TVertexKey start, TVertexKey target)
        {
            var edge = GetEdge(start, target);
            return FindEdge(edge);
        }

        internal TEdgeValue FindEdge(Edge edge)
        {
            if (edge != null)
            {
                return edge.Data;
            }
            return default(TEdgeValue);
        }

        // Vertices Enumerator
        public IEnumerable<(TVertexKey Key, TVertexValue Data)> Vertices
            => _vertices.Select(v => (v.Key, v.Value.Data));

        // Edges Enumerator
        public IEnumerable<(TEdgeKey Key, TVertexKey Start, TVertexKey Target, TEdgeValue Data)> Edges
            => _edges.Select(v => (v.Key, v.Value.StartVertex.Key, v.Value.TargetVertex.Key, v.Value.Data));

        // Vertices Incident enumerator
        public IEnumerable<(TEdgeKey Key, TEdgeValue Data, TVertexKey Target)> VertexIncidents(TVertexKey key)
        {
            var vertex = GetVertex(key);
            if (vertex == null)
            {
                throw new ItemNotFoundException(Resources.VertexNotFound);
            }

            return vertex.IncidentEdges.Select(edge => (edge.Key, edge.Data, edge.GetOpositeVertex(vertex).Key));
        }

        // Edge incidents enumerator
        public IEnumerable<(TVertexKey Key, TVertexValue Data)> EdgeIncidents(TEdgeKey key)
        {
            var edge = GetEdge(key);
            return GetEdgeIncidents(edge);
        }

        // Edge incidents enumerator
        public IEnumerable<(TVertexKey Key, TVertexValue Data)> EdgeIncidents(TVertexKey start, TVertexKey target)
        {
            var edge = GetEdge(start, target);
            return GetEdgeIncidents(edge);
        }

        // Edge incidents enumerator
        internal IEnumerable<(TVertexKey Key, TVertexValue Data)> GetEdgeIncidents(Edge edge)
        {
            if (edge == null)
            {
                throw new ItemNotFoundException(Resources.EdgeNotFound);
            }

            var data = new List<(TVertexKey Key, TVertexValue Data)>();

            data.Add((edge.StartVertex.Key, edge.StartVertex.Data));
            data.Add((edge.TargetVertex.Key, edge.TargetVertex.Data));

            return data;
        }
    }
}
