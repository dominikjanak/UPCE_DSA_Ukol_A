using System.Collections.Generic;
using System.Drawing;
using ForestGraph;

namespace GUI.Drawing
{
    public static class DrawGraphExtension
    {
        public static void DrawWithPath(this ForestGraph<string, VertexData, string, EdgeData> graph, DrawGraph drawer, List<string> path)
        {
            Dictionary<string, VertexData> mappedVertexes = InitDraw(graph, drawer);

            if (mappedVertexes.Count <= 0)
            {
                return;
            }

            DrawEdges(drawer, mappedVertexes, graph.GetAllEdges());
            DrawPath(drawer, mappedVertexes, path);
            DrawVertexes(drawer, mappedVertexes);
        }

        public static void Draw(this ForestGraph<string, VertexData, string, EdgeData> graph, DrawGraph drawer)
        {
            Dictionary<string, VertexData> mappedVertexes = InitDraw(graph, drawer);

            if (mappedVertexes.Count <= 0)
            {
                return;
            }

            DrawEdges(drawer, mappedVertexes, graph.GetAllEdges());
            DrawVertexes(drawer, mappedVertexes);
        }

        private static Dictionary<string, VertexData> InitDraw(ForestGraph<string, VertexData, string, EdgeData> graph, DrawGraph drawer)
        {
            drawer.ClearCanvas();
            var vertexes = graph.GetAllVertexes();

            Dictionary<string, VertexData> mappedVertexes = new Dictionary<string, VertexData>();

            foreach (var v in vertexes)
            {
                mappedVertexes.Add(v.key, v.data);
            }

            drawer.SetGraphSize(CalculateGraphSize(mappedVertexes));

            return mappedVertexes;
        }

        private static void DrawEdges(DrawGraph drawer, Dictionary<string, VertexData> mappedVertexes, 
            List<(string key, EdgeData data, string start, string target)> edges)
        {
            int edgesCount = edges.Count;
            for (int i = 0; i < edgesCount; i++)
            {
                var edge = edges[i];
                VertexData vertexA = null;
                VertexData vertexB = null;

                if (mappedVertexes.TryGetValue(edge.start, out vertexA) && mappedVertexes.TryGetValue(edge.target, out vertexB))
                {
                    drawer.DrawEdge(vertexA.Location, vertexB.Location, edge.data.EdgeType, edge.data.Distance.ToString());
                }
            }
        }

        private static void DrawVertexes(DrawGraph drawer, Dictionary<string, VertexData> vertexes)
        {
            foreach(var vertex in vertexes)
            {
                drawer.DrawVertex(vertex.Value.Location, vertex.Value.VertexType, vertex.Key.ToString());
            }
        }

        private static void DrawPath(DrawGraph drawer, Dictionary<string, VertexData> vertexes, List<string> path)
        {
            VertexData vertexA = null;
            VertexData vertexB = null;
            for (int i = 1; i < path.Count; i++)
            {
                if(vertexes.TryGetValue(path[i - 1], out vertexA) && vertexes.TryGetValue(path[i], out vertexB))
                {
                    drawer.DrawPath(vertexA.Location, vertexB.Location);
                }
                
            }
        }

        private static GraphSize<float> CalculateGraphSize(Dictionary<string, VertexData> vertexes)
        {
            bool first = true;
            GraphSize<float> graphSize = new GraphSize<float>(0,0,0,0);

            foreach (var vertex in vertexes)
            {
                if (first)
                {
                    float xLoc = vertex.Value.Location.X;
                    float yLoc = vertex.Value.Location.Y;
                    graphSize = new GraphSize<float>(xLoc, xLoc, yLoc, yLoc);
                    first = false;
                    continue;
                }

                float x = vertex.Value.Location.X;
                float y = vertex.Value.Location.Y;

                if (graphSize.XMax < x) { graphSize.XMax = x; }
                if (graphSize.XMin > x) { graphSize.XMin = x; }
                if (graphSize.YMax < y) { graphSize.YMax = y; }
                if (graphSize.YMin > y) { graphSize.YMin = y; }

            }
            return graphSize;
        }
    }
}
