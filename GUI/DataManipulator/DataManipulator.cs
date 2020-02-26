using System;
using System.Collections.Generic;
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
    }
}
