using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GUI
{
    [Serializable]
    [XmlRoot("GraphData")]
    public class GraphData
    {
        List<VertexSerialize> _vertices;
        List<EdgeSerialize> _edges;

        public GraphData() 
        {
            _vertices = new List<VertexSerialize>();
            _edges = new List<EdgeSerialize>();
        }

        [XmlArray("Vertices")]
        [XmlArrayItem("Vertex")]
        public List<VertexSerialize> Vertices { get => _vertices; }


        [XmlArray("Edges")]
        [XmlArrayItem("Edge")]
        public List<EdgeSerialize> Edges { get => _edges; }
    }
}
