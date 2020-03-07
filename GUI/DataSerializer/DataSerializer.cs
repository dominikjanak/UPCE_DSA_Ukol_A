using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using GraphService;
using GUI.Graph;
using GUI.Properties;

namespace GUI
{
    static class DataSerializer
    {
        public static string SaveDataDialog(Graph<string, VertexData, string, EdgeData> graph, string path = "")
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.FileName = "GraphData_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                saveDialog.Filter = "XML Data|*.xml";
                saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (!string.IsNullOrEmpty(path) || saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(path))
                    {
                        path = saveDialog.FileName;
                    }

                    if (!SaveData(graph, path))
                    {
                        MessageBox.Show(Resources.DataSaveError, Resources.ExportErrorTitle);
                    }
                    return path;
                }
            }                      
            return "";
        }

        public static bool SaveData(Graph<string, VertexData, string, EdgeData> graph, string path)
        {
            try
            {
                GraphData data = new GraphData();

                foreach (var vertex in graph.Vertices)
                {
                    VertexSerialize v = new VertexSerialize(vertex.Key, vertex.Data.Location.X,
                        vertex.Data.Location.Y, vertex.Data.VertexType);
                    data.Vertices.Add(v);
                }

                foreach (var edge in graph.Edges)
                {
                    EdgeSerialize e = new EdgeSerialize(edge.Key, edge.Start, edge.Target, edge.Data.Distance);
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
                MessageBox.Show(Resources.ExportError + ex.Message, Resources.ExportErrorTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public static string LoadDataDialog(Graph<string, VertexData, string, EdgeData> graph)
        {
            OpenFileDialog loadDialog = new OpenFileDialog
            {
                Filter = "XML Data|*.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Vyberte XML soubor"
            };

            if (loadDialog.ShowDialog() == DialogResult.OK)
            {
                if(LoadData(graph, loadDialog.FileName))
                {
                    return loadDialog.FileName;
                }                
            }
            return "";
        }

        public static bool LoadData(Graph<string, VertexData, string, EdgeData> graph, string path)
        {
            GraphData data;

            StreamReader sr = new StreamReader(path);
            XmlSerializer deserializer = new XmlSerializer(typeof(GraphData));
            data = (GraphData)deserializer.Deserialize(sr);
            sr.Close();

            bool replaceData = false;

            if (!graph.IsEmpty())
            {
                DialogResult result = MessageBox.Show(Resources.DataAlreadyExists, Resources.DataAlreadyExistsTitle,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    replaceData = true;
                }
            }

            if(graph.IsEmpty() || (!graph.IsEmpty() && replaceData))
            { 
                graph.Clear();

                foreach (var v in data.Vertices)
                {
                    VertexData vertexData = new VertexData(new PointF(v.X, v.Y), v.Type);
                    graph.AddVertex(v.ID, vertexData);
                }

                foreach (var e in data.Edges)
                {
                    EdgeData edgeData = new EdgeData(e.Size, EdgeType.Free);
                    graph.AddEdge(e.ID, e.Start, e.Target, edgeData);
                }
                return true;
            }
            return false;
        }
    }
}
