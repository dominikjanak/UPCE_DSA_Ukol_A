using System.Collections.Generic;
using GraphService;
using GUI.Properties;

namespace GUI.Graph.Drawing
{
    public static class DrawGraphExtension
    {

        public static void DrawWithPath(this Graph<string, VertexData, string, EdgeData> graph, DrawGraph drawer, List<string> path)
        {
            if(graph.VerticesCount() > 1000)
            {
                drawer.ClearCanvas();
                drawer.DrawWarning(Resources.CannotDrawGraph);
                return;
            }

            Dictionary<string, VertexData> mappedVertices = InitDraw(graph, drawer);

            if (mappedVertices.Count <= 0)
            {
                return;
            }

            DrawEdges(drawer, mappedVertices, graph.Edges);
            if (path != null) 
            { 
                DrawPath(drawer, mappedVertices, path);
            }
            DrawVertices(drawer, mappedVertices);
        }

        public static void Draw(this Graph<string, VertexData, string, EdgeData> graph, DrawGraph drawer)
        {
            if (graph.VerticesCount() > 1000)
            {
                drawer.DrawWarning(Resources.CannotDrawGraph);
                return;
            }

            Dictionary<string, VertexData> mappedVertices = InitDraw(graph, drawer);

            if (mappedVertices.Count <= 0)
            {
                return;
            }

            DrawEdges(drawer, mappedVertices, graph.Edges);
            DrawVertices(drawer, mappedVertices);
        }

        private static Dictionary<string, VertexData> InitDraw(Graph<string, VertexData, string, EdgeData> graph, DrawGraph drawer)
        {
            drawer.ClearCanvas();
            var vertices = graph.Vertices;

            Dictionary<string, VertexData> mappedVertices = new Dictionary<string, VertexData>();

            foreach (var v in vertices)
            {
                mappedVertices.Add(v.Key, v.Data);
            }

            drawer.SetGraphSize(CalculateGraphSize(mappedVertices));

            return mappedVertices;
        }

        private static void DrawEdges(DrawGraph drawer, Dictionary<string, VertexData> mappedVertexes,
            IEnumerable<(string Key, string Start, string Target, EdgeData Data)> edges)
        {
            foreach(var edge in edges)
            {
                VertexData vertexA = null;
                VertexData vertexB = null;

                if (mappedVertexes.TryGetValue(edge.Start, out vertexA) && mappedVertexes.TryGetValue(edge.Target, out vertexB))
                {
                    drawer.DrawEdge(vertexA.Location, vertexB.Location, edge.Data.EdgeType, edge.Data.Distance.ToString());
                }
            }
        }

        private static void DrawVertices(DrawGraph drawer, Dictionary<string, VertexData> vertices)
        {
            foreach(var vertex in vertices)
            {
                drawer.DrawVertex(vertex.Value.Location, vertex.Value.VertexType, vertex.Key.ToString());
            }
        }

        private static void DrawPath(DrawGraph drawer, Dictionary<string, VertexData> vertices, List<string> path)
        {
            VertexData vertexA = null;
            VertexData vertexB = null;
            for (int i = 1; i < path.Count; i++)
            {
                if(vertices.TryGetValue(path[i - 1], out vertexA) && vertices.TryGetValue(path[i], out vertexB))
                {
                    drawer.DrawPath(vertexA.Location, vertexB.Location);
                }
                
            }
        }

        private static GraphSize<float> CalculateGraphSize(Dictionary<string, VertexData> vertices)
        {
            bool first = true;
            GraphSize<float> graphSize = new GraphSize<float>(0,0,0,0);

            foreach (var vertex in vertices)
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
