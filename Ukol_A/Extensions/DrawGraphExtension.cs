using System.Collections.Generic;
using System.Windows.Forms;
using Ukol_A.Drawing;
using Ukol_A.Exceptions;
using Ukol_A.Graph;

namespace Ukol_A.Extensions
{
    public static class DrawGraphExtension
    {
        public static void Draw(this IForestGraph<string, string> graph, Control panel)
        {
            var vertexes = graph.GetAllVertexes();
            var edges = graph.GetAllEdges();

            int vertexesCount = vertexes.Count;
            int edgesCount = edges.Count;

            if(vertexesCount <= 0)
            {
                throw new EmptyGraphException("Graf se nedá vykreslit, neboť je prázdný!");
            }

            DrawGraph drawer = new DrawGraph(panel);
            drawer.ClearCanvas();
            drawer.SetGraphSize(graph.GetGraphSize());

            for(int i = 0; i < edgesCount; i++)
            {
                var edge = edges[i];
                var edgeVertexA = edge.GetStartVertex();
                var edgeVertexB = edge.GetTargetVertex();

                drawer.DrawEdge(edgeVertexA.GetLocation(), edgeVertexB.GetLocation(), edge.GetType());
            }

            for (int i = 0; i < vertexesCount; i++)
            {
                var vertex = vertexes[i];
                drawer.DrawVertex(vertex.GetLocation(), vertex.GetType(), vertex.GetLabel());
            }

            MessageBox.Show("Test");

        }
    }
}
