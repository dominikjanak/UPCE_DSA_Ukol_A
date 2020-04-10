using GraphService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GUI.Graph.Component
{
    static class GraphGenerator
    {
        private static Graph<string, VertexData, string, EdgeData> _graph; // Reference to graph which can be fill by generated data
        private static List<(string id, Point p)> _vertices; // All generated vertices
        private static Dictionary<string, int> _vertexEdges; // Number of incident edges of each vertex

        static GraphGenerator()
        {
            _graph = null;
            _vertices = new List<(string id, Point p)>();
            _vertexEdges = new Dictionary<string, int>();
        }

        public static void Init(Graph<string, VertexData, string, EdgeData> graph)
        {
            _graph = graph; 
        }

        public static void Generate((int junctions, int restAreas, int stops) vertices, int edgesMinForVertex, int edgesMaxForVertex)
        {
            if(_graph == null)
            {
                throw new ArgumentNullException();
            }

            // Clear bebore generation
            _graph.Clear();
            _vertices.Clear();
            _vertexEdges.Clear();

            // Generating step by step
            GenerateVertices(vertices);
            GenerateEdges(edgesMinForVertex, edgesMaxForVertex);
        }

        static private void GenerateVertices((int junctions, int restAreas, int stops) vertices)
        {
            Random rnd = new Random(Environment.TickCount);
            (int xMin, int xMax, int yMin, int yMax) axes = (0, 1000, 0, 700);
            VertexType type = VertexType.Junction;
            int x = 0, y = 0;
            string vertexKey = "";

            // Generate defined count of vertices
            for (int i = 0; i < (vertices.junctions + vertices.restAreas + vertices.stops); i++)
            {
                // New vertex location
                x = rnd.Next(axes.xMin, axes.xMax);
                y = rnd.Next(axes.yMin, axes.yMax);

                // Define appropriate vertex type
                if (i >= vertices.junctions + vertices.restAreas)
                {
                    type = VertexType.Stop;
                    vertexKey = "Z" + (i + 1 - vertices.junctions - vertices.restAreas);
                }
                else if (i >= vertices.junctions)
                {
                    type = VertexType.RestArea;
                    vertexKey = "O" + (i + 1 - vertices.junctions);
                }
                else
                {
                    type = VertexType.Junction;
                    vertexKey = "V" + (i + 1);
                }

                Point p = new Point(x, y);
                VertexData data = new VertexData(vertexKey, p, type);

                // Add new vertex into graph
                _graph.AddVertex(vertexKey, data);
                _vertices.Add((vertexKey, p));
                _vertexEdges.Add(vertexKey, 0);
            }
        }

        static private void GenerateEdges(int edgesMinForVertex, int edgesMaxForVertex)
        {
            Random rnd = new Random(Environment.TickCount);
            int vertexId = -1;
            int loop = 0;
            int vertexKey = 1;

            // For each vertex generate edges
            foreach (var vertex in _vertices)
            {
                int edges = rnd.Next(edgesMinForVertex, edgesMaxForVertex); // Number of generated edges

                for (int i = _vertexEdges[vertex.id]; i < edges; i++)
                {
                    loop = 0;
                    do
                    {
                        vertexId = rnd.Next(_vertices.Count());
                        loop++;

                    } while (_vertexEdges[_vertices[vertexId].id] > edgesMaxForVertex && loop < 5);

                    // if edge does not exists then add it into graph (and both vertexes are not same)
                    if (!_graph.HasEdge(vertex.id, _vertices[vertexId].id) && vertex.id != _vertices[vertexId].id)
                    {
                        _graph.AddEdge("E" + vertexKey, vertex.id, _vertices[vertexId].id, new EdgeData(1, EdgeType.Free));
                        _vertexEdges[vertex.id]++;
                        _vertexEdges[_vertices[vertexId].id]++;
                        vertexKey++;
                    }
                }
            }
        }
    }
}
