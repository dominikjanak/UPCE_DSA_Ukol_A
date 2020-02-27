using ForestGraph;
using GraphService.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphService.Dijkstra
{
    public class DijkstraAlhorithm<TVertexKey, TVertexData, TEdgeKey, TEdgeData> 
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
    {
        private readonly ForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData> _graph;
        private HashSet<TVertexKey> _finalized;
        private Dictionary<TVertexKey, float> _costs;
        private Dictionary<TVertexKey, TVertexKey> _prev;

        private TVertexKey _start;
        private bool _ignoreBlocked;
        private bool _invalidated;

        private readonly Func<TEdgeData, float> _GetEdgeWeight;
        private readonly Func<TEdgeData, bool> _IsEdgeBlocked;

        public DijkstraAlhorithm(ForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData> graph, Func<TEdgeData, float> weightExtractor, Func<TEdgeData, bool> blockedExtractor)
        {
            _graph = graph;
            _GetEdgeWeight = weightExtractor;
            _IsEdgeBlocked = blockedExtractor;
            _start = default(TVertexKey);
            _ignoreBlocked = true;
            _invalidated = true;
            Clean();
        }

        private void Clean()
        {
            _finalized = new HashSet<TVertexKey>();
            _costs = new Dictionary<TVertexKey, float>();
            _prev = new Dictionary<TVertexKey, TVertexKey>();
        }

        public void Invalidate()
        {
            _invalidated = true;
        }

        public List<TVertexKey> GetPath(TVertexKey target)
        {
            if (_invalidated)
            {
                throw new InvalidatedDataException(Resources.INVALID_CALCULATIONS);
            }

            if (target == null)
            {
                throw new ArgumentNullException(Resources.START_KEY_NOT_RECIEVE);
            }

            if(_start.CompareTo(default(TVertexKey)) == 0)
            {
                throw new GraphNotExploredException(Resources.GRAPH_NOT_EXPLORED);
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

        public DijkstraAlhorithm<TVertexKey, TVertexData, TEdgeKey, TEdgeData> FindPaths(TVertexKey start, bool ignoreBlocked = false)
        {
            if (start == null)
            {
                throw new ArgumentNullException(Resources.TARGET_KEY_NOT_RECIEVE);
            }

            if(!_graph.HasVertex(start))
            {
                throw new ItemNotFoundException(Resources.VERTEX_NOT_EXISTS);
            }

            if (_invalidated || _ignoreBlocked != ignoreBlocked || _start == null || (_start != null && _start.CompareTo(start) != 0)) 
            {
                _start = start;
                _ignoreBlocked = ignoreBlocked;
                CalculateRoutes();
                _invalidated = false;
            }

            return this;
        }

        private void CalculateRoutes()
        {
            Clean();
            _costs.Add(_start, 0);

            while (_costs.Count(p => !_finalized.Contains(p.Key)) != 0)
            {
                float doubleMaxValue = float.MaxValue;
                TVertexKey minValue = default(TVertexKey);

                foreach (var item in _costs)
                {
                    if (!_finalized.Contains(item.Key) && item.Value < doubleMaxValue)
                    {
                        minValue = item.Key;
                        doubleMaxValue = item.Value;
                    }
                }

                _finalized.Add(minValue);
                ExploreIncidentEdges(doubleMaxValue, minValue);
            }
        }

        private void ExploreIncidentEdges(float doubleMaxValue, TVertexKey minValue)
        {
            foreach (var item in _graph.GetVertex(minValue).IncidentEdges)
            {
                var oposite = item.GetOpositeVertex(_graph.GetVertex(minValue));

                // skip if blocked or key is alread exists
                if (_finalized.Contains(oposite.Key) || (_IsEdgeBlocked(item.Data) && !_ignoreBlocked))
                {
                    continue;
                }

                float ncost = doubleMaxValue + _GetEdgeWeight(item.Data);

                if (_costs.ContainsKey(oposite.Key))
                {
                    float ocost = _costs[oposite.Key];
                    if (ncost < ocost)
                    {
                        _costs[oposite.Key] = ncost;
                        _prev[oposite.Key] = minValue;
                    }
                }
                else
                {
                    _costs.Add(oposite.Key, ncost);
                    _prev[oposite.Key] = minValue;
                }
            }
        }
    }
}
