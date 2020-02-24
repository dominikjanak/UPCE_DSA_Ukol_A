using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Ukol_A.Drawing;

namespace Ukol_A
{
    public static class DrawGraphExtension
    {
        public static void Draw(this IForestGraph<string, VertexData, string, EdgeData> graph, DrawGraph drawer)
        {
            var vertexes = graph.GetAllVertexes();
            var edges = graph.GetAllEdges();

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
                var edgeVertexA = edge.GetStartVertex();
                var edgeVertexB = edge.GetTargetVertex();

                drawer.DrawEdge(edgeVertexA.Data.GetLocation(), edgeVertexB.Data.GetLocation(), edge.Data.GetEdgeType(), edge.Data.GetDistance().ToString());
            }

            for (int i = 0; i < vertexesCount; i++)
            {
                var vertex = vertexes[i];
                drawer.DrawVertex(vertex.Data.GetLocation(), vertex.Data.GetVertexType(), vertex.GetKey().ToString());
            }
        }

        private static GraphSize<float> CalculateGraphSize(List<IVertex<string, VertexData, string, EdgeData>> vertexes)
        {
            float xLoc = vertexes[0].Data.GetLocation().X;
            float yLoc = vertexes[0].Data.GetLocation().Y;
            int vertexesCount = vertexes.Count;

            GraphSize<float> graphSize = new GraphSize<float>(xLoc, xLoc, yLoc, yLoc);

            for (int i = 1; i < vertexesCount; i++)
            {
                float x = vertexes[i].Data.GetLocation().X;
                float y = vertexes[i].Data.GetLocation().Y;

                if (graphSize.XMax < x) { graphSize.XMax = x; }
                if (graphSize.XMin > x) { graphSize.XMin = x; }
                if (graphSize.YMax < y) { graphSize.YMax = y; }
                if (graphSize.YMin > y) { graphSize.YMin = y; }
            }

            return graphSize;
        }
    }
}
