using GraphService.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphService.Dijkstra
{
    public class DijkstraAlhorithm<TVertexKey, TVertexData, TEdgeKey, TEdgeData> 
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
        where TVertexData : IVertexData
        where TEdgeData : IEdgeData
    {
        private readonly Graph<TVertexKey, TVertexData, TEdgeKey, TEdgeData> _graph; // reference to graph
        private HashSet<TVertexKey> _finalized;
        private Dictionary<TVertexKey, float> _costs;
        private Dictionary<TVertexKey, TVertexKey> _prev;

        private TVertexKey _start;
        private bool _ignoreBlocked;
        private bool _invalidated;

        public DijkstraAlhorithm(Graph<TVertexKey, TVertexData, TEdgeKey, TEdgeData> graph)
        {
            _graph = graph;
            _start = default(TVertexKey);
            _ignoreBlocked = true;
            _invalidated = true;
            Clean();
        }


        // Clear analyzed data
        private void Clean()
        {
            _finalized = new HashSet<TVertexKey>();
            _costs = new Dictionary<TVertexKey, float>();
            _prev = new Dictionary<TVertexKey, TVertexKey>();
        }

        // invalidate data
        public void Invalidate()
        {
            _invalidated = true;
        }

        // Chceck if data is invalidate 
        public bool IsInvalidated()
        {
            return _invalidated;
        }

        // Calculate path from _start vertex to target vertex
        public List<TVertexKey> GetPath(TVertexKey target)
        {
            if (_invalidated)
            {
                throw new InvalidatedDataException(Resources.InvalidCalculations);
            }

            if (target == null)
            {
                throw new ArgumentNullException(Resources.StartKeyNull);
            }

            if(_start.CompareTo(default(TVertexKey)) == 0)
            {
                throw new GraphNotExploredException(Resources.GraphNotExplored);
            }

            List<TVertexKey> route = new List<TVertexKey>();

            TVertexKey vertex = target;
            while (vertex != null)
            {
                route.Add(vertex);
                vertex = _prev.ContainsKey(vertex) ? _prev[vertex] : default(TVertexKey);
            }

            if (route.Count == 1 && route[0].CompareTo(target) == 0)
            {
                return null;
            }

            route.Reverse();
            return route;
        }

        // Initialize calculation of path from start vertex to every vertex in graph
        public DijkstraAlhorithm<TVertexKey, TVertexData, TEdgeKey, TEdgeData> FindPaths(TVertexKey start, bool ignoreBlocked = false)
        {
            if (start == null)
            {
                throw new ArgumentNullException(Resources.TargetKeyNull);
            }

            if(!_graph.HasVertex(start))
            {
                throw new ItemNotFoundException(Resources.VertexNotExists);
            }

            // Calculate only if:
            //    - data is invalidated
            //    - ignore blocked edges is different in new search
            //    - predefined _start is null
            //    - calculated _start is different from new start
            if (_invalidated || _ignoreBlocked != ignoreBlocked || _start == null || (_start != null && _start.CompareTo(start) != 0)) 
            {
                _start = start;
                _ignoreBlocked = ignoreBlocked;
                CalculateRoutes();
                _invalidated = false;
            }

            return this;
        }

        // Calculate paths from _start to every vertex in graph
        private void CalculateRoutes()
        {
            Clean();
            _costs.Add(_start, 0);

            while (_costs.Count(p => !_finalized.Contains(p.Key)) != 0)
            {
                float cost = float.MaxValue;
                TVertexKey nearestVertex = default(TVertexKey);

                foreach (var storeCost in _costs)
                {
                    if (!_finalized.Contains(storeCost.Key) && storeCost.Value < cost)
                    {
                        nearestVertex = storeCost.Key;
                        cost = storeCost.Value;
                    }
                }

                _finalized.Add(nearestVertex);
                ExploreIncidentEdges(cost, nearestVertex);
            }
        }

        // Explore incident edges of vertex
        private void ExploreIncidentEdges(float cost, TVertexKey nearestVertex)
        {
            foreach (var edge in _graph.GetVertex(nearestVertex).IncidentEdges)
            {
                var oposite = edge.GetOpositeVertex(_graph.GetVertex(nearestVertex));

                // skip if blocked (only when it is not ignored) or key is alread exists
                if (_finalized.Contains(oposite.Key) || (edge.Data.IsBlocked() && !_ignoreBlocked))
                {
                    continue;
                }

                float ncost = cost + edge.Data.GetDistance();

                if (_costs.ContainsKey(oposite.Key))
                {
                    float opositCost = _costs[oposite.Key];
                    if (ncost < opositCost)
                    {
                        _costs[oposite.Key] = ncost;
                        _prev[oposite.Key] = nearestVertex;
                    }
                }
                else
                {
                    _costs.Add(oposite.Key, ncost);
                    _prev[oposite.Key] = nearestVertex;
                }
            }
        }
    }
}
