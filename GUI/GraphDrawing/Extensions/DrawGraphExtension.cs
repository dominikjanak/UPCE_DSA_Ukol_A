using System.Collections.Generic;
using ForestGraph;

namespace GUI.Drawing
{
    public static class DrawGraphExtension
    {
        public static void Draw(this ForestGraph<string, VertexData, string, EdgeData> graph, DrawGraph drawer)
        {
            var vertexes = graph.GetAllVertexes();
            var edges = graph.GetAllEdges();

            Dictionary<string, VertexData> mappedVertexes = new Dictionary<string, VertexData>();

            foreach(var v in vertexes)
            {
                mappedVertexes.Add(v.key, v.data);
            }

            int vertexesCount = vertexes.Count;
            int edgesCount = edges.Count;

            drawer.ClearCanvas();

            if (vertexesCount <= 0)
            {
                return;
            }

            GraphSize<float> graphSize = CalculateGraphSize(vertexes);
            drawer.SetGraphSize(graphSize);

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

            for (int i = 0; i < vertexesCount; i++)
            {
                var vertex = vertexes[i];
                drawer.DrawVertex(vertex.data.Location, vertex.data.VertexType, vertex.key.ToString());
            }
        }

        private static GraphSize<float> CalculateGraphSize(List<(string key, VertexData data)> vertexes)
        {
            float xLoc = vertexes[0].data.Location.X;
            float yLoc = vertexes[0].data.Location.Y;
            int vertexesCount = vertexes.Count;

            GraphSize<float> graphSize = new GraphSize<float>(xLoc, xLoc, yLoc, yLoc);

            for (int i = 1; i < vertexesCount; i++)
            {
                float x = vertexes[i].data.Location.X;
                float y = vertexes[i].data.Location.Y;

                if (graphSize.XMax < x) { graphSize.XMax = x; }
                if (graphSize.XMin > x) { graphSize.XMin = x; }
                if (graphSize.YMax < y) { graphSize.YMax = y; }
                if (graphSize.YMin > y) { graphSize.YMin = y; }
            }

            return graphSize;
        }
    }
}
