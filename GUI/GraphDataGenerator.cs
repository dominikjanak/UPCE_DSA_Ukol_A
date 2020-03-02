using ForestGraph;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    static class GraphDataGenerator
    {
        private static List<(string id, Point p)> _vertices;
        private static ForestGraph<string, VertexData, string, EdgeData> _graph;
        private static Dictionary<string, int> _vertexEdges;

        static GraphDataGenerator()
        {
            _graph = null;
            _vertices = new List<(string id, Point p)>();
            _vertexEdges = new Dictionary<string, int>();
        }

        public static void Init(ForestGraph<string, VertexData, string, EdgeData> graph)
        {
            _graph = graph; 
        }

        public static void Generate((int junctions, int restAreas, int stops) vertices, int edgesMinForVertex, int edgesMaxForVertex)
        {
            if(_graph == null)
            {
                throw new ArgumentNullException();
            }
            _graph.Clear();
            _vertices.Clear();
            _vertexEdges.Clear();

            GenerateVertices(vertices);
            GenerateEdges(edgesMinForVertex, edgesMaxForVertex);
        }

        static private void GenerateEdges(int edgesMinForVertex, int edgesMaxForVertex)
        {
            Random rnd = new Random(Environment.TickCount);
            int itemId = -1;
            int loop = 0;
            int idx = 1;
            foreach (var vertex in _vertices)
            {
                int edges = rnd.Next(edgesMinForVertex, edgesMaxForVertex);
                int startVertexCnt = _vertexEdges[vertex.id];

                for(int i = startVertexCnt; i < edges; i++)
                {
                    loop = 0;
                    do
                    {
                        itemId = rnd.Next(_vertices.Count());
                        loop++;

                    } while(_vertexEdges[_vertices[itemId].id] > edgesMaxForVertex && loop < 5);


                    if (!_graph.HasEdge(vertex.id, _vertices[itemId].id) && vertex.id != _vertices[itemId].id)
                    {
                        _graph.AddEdge("E" + idx, vertex.id, _vertices[itemId].id, new EdgeData(1, EdgeType.Free));
                        _vertexEdges[vertex.id]++;
                        _vertexEdges[_vertices[itemId].id]++;
                        idx++;
                    }
                }
            }


        }

        static private void GenerateVertices((int junctions, int restAreas, int stops) vertices)
        {
            Random rnd = new Random(Environment.TickCount);
            (int xMin, int xMax, int yMin, int yMax)axes = (0, 1000, 0, 700);


            VertexType type = VertexType.Junction;
            int x = 0, y = 0;
            string id = "";
            for (int i = 0; i < (vertices.junctions + vertices.restAreas + vertices.stops); i++)
            {
                x = rnd.Next(axes.xMin, axes.xMax);
                y = rnd.Next(axes.yMin, axes.yMax);



                if (i >= vertices.junctions + vertices.restAreas)
                {
                    type = VertexType.Stop;
                    id = "Z" + (i + 1 - vertices.junctions - vertices.restAreas);
                }
                else if (i >= vertices.junctions)
                {
                    type = VertexType.RestArea;
                    id = "O" + (i + 1 - vertices.junctions);
                }
                else
                {
                    type = VertexType.Junction;
                    id = "V" + (i + 1);
                }

                Point p = new Point(x, y);
                VertexData data = new VertexData(p, type);

                _graph.AddVertex(id, data);
                _vertices.Add((id, p));
                _vertexEdges.Add(id, 0);
            }
        }
    }
}
