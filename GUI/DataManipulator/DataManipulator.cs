using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using ForestGraph;

namespace GUI
{
    static class DataManipulator
    {
        public static bool SaveDataDialog(ForestGraph<string, VertexData, string, EdgeData> graph, bool openAfter = false)
        {
            bool state = false;
            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                FileName = "GraphData_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"),
                Filter = "XML Data|*.xml",
                AddExtension = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                state = SaveData(graph, saveDialog.FileName);
                if (File.Exists(saveDialog.FileName) && openAfter) 
                { 
                    Process.Start(saveDialog.FileName);
                }
            }
            return state;
        }

        public static bool SaveData(ForestGraph<string, VertexData, string, EdgeData> graph, string path)
        {
            try
            {
                GraphData data = new GraphData();

                var allVertexes = graph.GetAllVertexes();
                foreach (var vertex in allVertexes)
                {
                    VertexSerialize v = new VertexSerialize(vertex.key, vertex.data.Location.X,
                        vertex.data.Location.Y, vertex.data.VertexType);
                    data.Vertexes.Add(v);
                }

                var allEdges = graph.GetAllEdges();
                foreach (var edge in allEdges)
                {
                    EdgeSerialize e = new EdgeSerialize(edge.key, edge.start, edge.target, edge.data.Distance);
                    data.Edges.Add(e);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(GraphData));
                TextWriter writer = new StreamWriter(path);
                serializer.Serialize(writer, data);
                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("V průběhu exportování se vyskytla chyba:\n\n" + ex.Message, "Chyba exportu",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public static void LoadData(ForestGraph<string, VertexData, string, EdgeData> graph)
        {
            OpenFileDialog loadDialog = new OpenFileDialog
            {
                Filter = "XML Data|*.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Vyberte XML soubor"
            };

            if (loadDialog.ShowDialog() == DialogResult.OK)
            {
                LoadData(graph, loadDialog.FileName);
            }
        }

        public static void LoadData(ForestGraph<string, VertexData, string, EdgeData> graph, string path)
        {
            try
            {
                GraphData data;

                StreamReader sr = new StreamReader(path);
                XmlSerializer deserializer = new XmlSerializer(typeof(GraphData));
                data = (GraphData)deserializer.Deserialize(sr);
                sr.Close();

                bool replaceData = false;

                if (!graph.IsEmpty())
                {
                    DialogResult result = MessageBox.Show("V programu již existuji data, chcete je přapsat?", "Existující data",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        replaceData = true;
                    }
                }

                if(graph.IsEmpty() || (!graph.IsEmpty() && replaceData))
                { 
                    graph.Clear();

                    foreach (var v in data.Vertexes)
                    {
                        VertexData vertexData = new VertexData(new PointF(v.X, v.Y), v.Type);
                        graph.AddVertex(v.ID, vertexData);
                    }

                    foreach (var e in data.Edges)
                    {
                        EdgeData edgeData = new EdgeData(e.Size, EdgeType.Free);
                        graph.AddEdge(e.ID, e.Start, e.Target, edgeData);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("V průběhu importování se vyskytla chyba:\n\n" + ex.Message, "Chyba importu",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*

        public static void InitializeBig(ForestGraph<string, VertexData, string, EdgeData> graph)
        {
            graph.Clear();
            InitializeVertexes(graph, GetBigGraphVertexes());
            InitializeEdges(graph, GetBigGraphEdges());
        }

        public static void InitializeSmall(ForestGraph<string, VertexData, string, EdgeData> graph)
        {
            graph.Clear();
            InitializeVertexes(graph, GetSmallGraphVertexes());
            InitializeEdges(graph, GetSmallGraphEdges());
        }


        public static void InitializeGraph(ForestGraph<string, VertexData, string, EdgeData> graph,
            List<(int x, int y, VertexType type)> vertexes,
            List<(string start, string target, EdgeType type, float size)> edges)
        {
            graph.Clear();
            InitializeVertexes(graph, vertexes);
            InitializeEdges(graph, edges);
        }


        private static void InitializeVertexes(ForestGraph<string, VertexData, string, EdgeData> graph, List<(int x, int y, VertexType type)> vertexes)
        {
            graph.Clear();
            int[] idx = new int[3] { 1, 1, 1 };

            foreach (var vertex in vertexes)
            {
                string key = "";

                switch (vertex.type)
                {
                    case VertexType.Junction:
                        key = "K" + idx[0]++;
                        break;
                    case VertexType.RestArea:
                        key = "O" + idx[1]++;
                        break;
                    case VertexType.Stop:
                        key = "Z" + idx[2]++;
                        break;
                }

                PointF point = new PointF(vertex.x, vertex.y);
                VertexData vertexData = new VertexData(point, vertex.type);

                graph.AddVertex(key, vertexData);
            }
        }

        private static void InitializeEdges(ForestGraph<string, VertexData, string, EdgeData> graph, List<(string start, string target, EdgeType type, float size)> edges)
        {
            int idx = 1;

            foreach (var vertex in vertexes)
            {
                string key = "";

                switch (vertex.type)
                {
                    case VertexType.Junction:
                        key = "K" + idx[0]++;
                        break;
                    case VertexType.RestArea:
                        key = "O" + idx[1]++;
                        break;
                    case VertexType.Stop:
                        key = "Z" + idx[2]++;
                        break;
                }

                PointF point = new PointF(vertex.x, vertex.y);
                VertexData vertexData = new VertexData(point, vertex.type);

                graph.AddVertex(key, vertexData);
            } 
        }


        private static List<(int x, int y, VertexType type)> GetBigGraphVertexes()
        {
            return new List<(int x, int y, VertexType type)>()
            {
                (182, 305, VertexType.Junction),
                (72, 80, VertexType.Junction),
                (270, 214, VertexType.Junction),
                (446, 324, VertexType.Junction),
                (277, 373, VertexType.Junction),
                (380, 260, VertexType.Junction),
                (338, 455, VertexType.Junction),
                (776, 381, VertexType.Junction),
                (636, 163, VertexType.Junction),
                (483, 544, VertexType.Junction),
                (706, 516, VertexType.Junction),
                (853, 78, VertexType.Junction),
                (729, 194, VertexType.Junction),
                (590, 73, VertexType.Junction),
                (240, 526, VertexType.Junction),
                (50, 389, VertexType.Junction),
                (875, 268, VertexType.Junction),
                (358, 65, VertexType.Junction),
                (934, 184, VertexType.Junction),
                (932, 474, VertexType.Junction),
                (160, 205, VertexType.Junction),
                (136, 555, VertexType.Junction),
                (491, 450, VertexType.Junction),
                (793, 559, VertexType.Junction),
                (729, 444, VertexType.Junction),
                (766, 280, VertexType.Junction),
                (541, 380, VertexType.Junction),
                (549, 210, VertexType.Junction),
                (410, 131, VertexType.Junction),
                (605, 319, VertexType.Junction),
                (591, 500, VertexType.RestArea),
                (701, 347, VertexType.RestArea),
                (510, 281, VertexType.RestArea),
                (140, 388, VertexType.RestArea),
                (442, 205, VertexType.RestArea),
                (515, 120, VertexType.RestArea),
                (170, 104, VertexType.RestArea),
                (845, 167, VertexType.RestArea),
                (661, 248, VertexType.RestArea),
                (889, 383, VertexType.RestArea),
                (377, 515, VertexType.RestArea),
                (832, 479, VertexType.RestArea),
                (623, 424, VertexType.RestArea),
                (723, 103, VertexType.RestArea),
                (294, 123, VertexType.RestArea),
                (70, 211, VertexType.RestArea),
                (87, 483, VertexType.RestArea),
                (217, 456, VertexType.RestArea),
                (411, 390, VertexType.RestArea),
                (302, 303, VertexType.RestArea),
                (180, 35, VertexType.Stop),
                (64, 295, VertexType.Stop),
                (47, 563, VertexType.Stop),
                (442, 32, VertexType.Stop),
                (314, 578, VertexType.Stop),
                (622, 568, VertexType.Stop),
                (936, 566, VertexType.Stop),
                (961, 342, VertexType.Stop),
                (947, 69, VertexType.Stop),
                (754, 42, VertexType.Stop)
            };
        }

        private static List<(string start, string target, EdgeType type, float size)> GetBigGraphEdges()
        {
            return new List<(string start, string target, EdgeType type, float size)>()
            {
            };
        }

        private static List<(int x, int y, VertexType type)> GetSmallGraphVertexes()
        {
            return new List<(int x, int y, VertexType type)>()
            {
                (321, 240, VertexType.Junction),
                (90, 153, VertexType.Junction),
                (285, 345, VertexType.Junction),
                (133, 379, VertexType.Junction),
                (69, 346, VertexType.Junction),
                (333, 146, VertexType.Junction),
                (381, 360, VertexType.Junction),
                (195, 207, VertexType.Junction),
                (117, 40, VertexType.Junction),
                (270, 35, VertexType.Junction),
                (460, 100, VertexType.Junction),
                (420, 189, VertexType.Junction),
                (500, 390, VertexType.Junction),
                (500, 283, VertexType.Junction),
                (580, 186, VertexType.Junction),
                (219, 114, VertexType.RestArea),
                (186, 300, VertexType.RestArea),
                (405, 246, VertexType.RestArea),
                (234, 428, VertexType.Stop),
                (570, 296, VertexType.Stop),
                (60, 219, VertexType.Stop),
                (372, 60, VertexType.Stop)
            };
        }

        private static List<(string start, string target, EdgeType type, float size)> GetSmallGraphEdges()
        {
           return new List<(string start, string target, EdgeType type, float size)>()
           {
           };            
        }
    */
    }
}
