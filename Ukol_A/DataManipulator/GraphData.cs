using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ukol_A
{
    [Serializable]
    [XmlRoot("GraphData")]
    public class GraphData
    {
        List<VertexSerialize> _vertexes;
        List<EdgeSerialize> _edges;

        public GraphData() 
        {
            _vertexes = new List<VertexSerialize>();
            _edges = new List<EdgeSerialize>();
        }

        [XmlArray("Vertexes")]
        [XmlArrayItem("Vertex")]
        public List<VertexSerialize> Vertexes { get => _vertexes; }


        [XmlArray("Edges")]
        [XmlArrayItem("Edge")]
        public List<EdgeSerialize> Edges { get => _edges; }
    }
}
