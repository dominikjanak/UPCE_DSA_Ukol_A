using System.Drawing;
using System.Windows.Forms;
using Ukol_A.Drawing;

namespace Ukol_A.Extensions
{
    public static class DrawGraphExtension
    {
        public static void Draw(this IForestGraph<IVertexData, IEdgeData> graph, Size panelSize, Graphics g)
        {
            var vertexes = graph.GetAllVertexes();
            var edges = graph.GetAllEdges();

            int vertexesCount = vertexes.Count;
            int edgesCount = edges.Count;

            DrawGraph drawer = new DrawGraph();
            drawer.InitCanvas(panelSize, g);
            drawer.ClearCanvas();

            if (vertexesCount <= 0)
            {
                return;
            }

            drawer.SetGraphSize(graph.GetGraphSize());

            for (int i = 0; i < edgesCount; i++)
            {
                var edge = edges[i];
                var edgeVertexA = edge.GetStartVertex();
                var edgeVertexB = edge.GetTargetVertex();

                drawer.DrawEdge(edgeVertexA.GetLocation(), edgeVertexB.GetLocation(), edge.Data.GetEdgeType(), "25");
            }

            for (int i = 0; i < vertexesCount; i++)
            {
                var vertex = vertexes[i];
                drawer.DrawVertex(vertex.GetLocation(), vertex.Data.GetVertexType(), vertex.GetLabel());
            }
        }
    }
}
