using ForestGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphService.Dijkstra
{
    public class DijkstraAlhorithm<TVertexKey, TVertexData, TEdgeKey, TEdgeData> 
        where TVertexKey : IComparable<TVertexKey>
        where TEdgeKey : IComparable<TEdgeKey>
    {
        private Func<TEdgeData, float> _GetEdgeWeight;
        private Func<TEdgeData, bool> _IsEdgeFree;

        private ForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData> _graph;
        private HashSet<TVertexKey> finalized;
        private Dictionary<TVertexKey, float> costs;
        private Dictionary<TVertexKey, TVertexKey> prev;

        public DijkstraAlhorithm(ForestGraph<TVertexKey, TVertexData, TEdgeKey, TEdgeData> graph, Func<TEdgeData, float> weightExtractor, Func<TEdgeData, bool> throughExtractor)
        {
            _graph = graph;
            _GetEdgeWeight = weightExtractor;
            _IsEdgeFree = throughExtractor;
            Clean();
        }

        private void Clean()
        {
            finalized = new HashSet<TVertexKey>();
            costs = new Dictionary<TVertexKey, float>();
            prev = new Dictionary<TVertexKey, TVertexKey>();
        }

        public List<TVertexKey> FindRoute(TVertexKey start, TVertexKey target, bool ignoreBlocked = false)
        {
            if(start.CompareTo(target) == 0)
            {
                return new List<TVertexKey>(){ start };
            }

            if(!_graph.HasVertex(start) || !_graph.HasVertex(target))
            {
                throw new ItemNotFoundException();
            }

            Clean();
            costs.Add(start, 0);

            while (costs.Where(p => !finalized.Contains(p.Key)).Count() != 0)
            {
                float doubleMaxValue = float.MaxValue;
                TVertexKey minValue = default(TVertexKey);

                foreach (var item in costs)
                {
                    if (!finalized.Contains(item.Key) && item.Value < doubleMaxValue)
                    {
                        minValue = item.Key;
                        doubleMaxValue = item.Value;
                    }
                }

                finalized.Add(minValue);

                foreach (var item in _graph.GetVertex(minValue).IncidentEdges)
                {
                    Vertex<TVertexKey, TVertexData, TEdgeKey, TEdgeData> opositeVertex = item.GetOpositeVertex(_graph.GetVertex(minValue));

                    if (finalized.Contains(opositeVertex.Key) || !_IsEdgeFree(item.Data)) 
                    { 
                        continue;
                    }

                    float ncost = doubleMaxValue + _GetEdgeWeight(item.Data);

                    if (costs.ContainsKey(opositeVertex.Key))
                    {
                        float ocost = costs[opositeVertex.Key];
                        if (ncost < ocost)
                        {
                            costs[opositeVertex.Key] = ncost;
                            prev[opositeVertex.Key] = minValue;
                        }
                    }
                    else
                    {
                        costs.Add(opositeVertex.Key, ncost);
                        prev[opositeVertex.Key] = minValue;
                    }
                }
            }

            // Get shortest path
            List<TVertexKey> route = new List<TVertexKey>();

            TVertexKey t = target;
            while (t != null)
            {
                route.Add(t);
                t = prev.ContainsKey(t) ? prev[t] : default(TVertexKey);
            }

            if(route.Count==1 && route[0].CompareTo(target) == 0)
            {
                throw new RouteNotExistsException(); 
            }

            route.Reverse();
            return route;
        }
    }
}
