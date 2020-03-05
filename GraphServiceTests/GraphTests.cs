using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GraphService.Tests
{
    [TestClass()]
    public class GraphTests
    {

        private Graph<string, TestVertexData, string, TestEdgeData> InitGraph()
        {
            var graph = new Graph<string, TestVertexData, string, TestEdgeData>();

            graph.AddVertex("K1", new TestVertexData());
            graph.AddVertex("K2", new TestVertexData());
            graph.AddVertex("K3", new TestVertexData());
            graph.AddVertex("K4", new TestVertexData());
            graph.AddVertex("K5", new TestVertexData());

            graph.AddEdge("H1", "K1", "K2", new TestEdgeData(3));
            graph.AddEdge("H2", "K4", "K2", new TestEdgeData(1));
            graph.AddEdge("H3", "K2", "K3", new TestEdgeData(10));
            graph.AddEdge("H4", "K2", "K5", new TestEdgeData(5));
            graph.AddEdge("H5", "K3", "K5", new TestEdgeData(4));
            graph.AddEdge("H6", "K1", "K3", new TestEdgeData(20));

            return graph;
        }

        [TestMethod()]
        public void AddVertexTest()
        {
            var graph = new Graph<string, TestVertexData, string, TestEdgeData>();

            graph.AddVertex("K1", new TestVertexData());
            Assert.AreEqual(1, graph.VerticesCount());

            graph.AddVertex("K2", new TestVertexData());
            Assert.AreEqual(2, graph.VerticesCount());

            graph.AddVertex("K3", new TestVertexData());
            Assert.AreEqual(3, graph.VerticesCount());

            graph.AddVertex("K4", new TestVertexData());
            Assert.AreEqual(4, graph.VerticesCount());

            Assert.ThrowsException<UniqueKeyException>(() => graph.AddVertex("K2", new TestVertexData()));
        }

        [TestMethod()]
        public void ClearTest()
        {
            var graph = InitGraph();

            graph.Clear();

            Assert.AreEqual(0, graph.EdgesCount());
            Assert.AreEqual(0, graph.VerticesCount());
        }

        [TestMethod()]
        public void IsEmptyTest()
        {
            var graph = InitGraph();

            Assert.IsFalse(graph.IsEmpty());
            graph.Clear();
            Assert.IsTrue(graph.IsEmpty());
        }

        [TestMethod()]
        public void RemoveVertex()
        {
            var graph = InitGraph();

            Assert.AreEqual(6, graph.EdgesCount());
            Assert.AreEqual(5, graph.VerticesCount());

            graph.RemoveVertex("K2");
            graph.RemoveVertex("K4");
            graph.RemoveVertex("K1");
            graph.RemoveVertex("K3");
            graph.RemoveVertex("K5");

            Assert.AreEqual(0, graph.EdgesCount());
            Assert.AreEqual(0, graph.VerticesCount());
        }

        [TestMethod()]
        public void EdgeRemoveAndCountTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(6, graph.EdgesCount());
            graph.RemoveEdge("H1");
            Assert.AreEqual(5, graph.EdgesCount());
            graph.RemoveEdge("K1", "K3");
            Assert.AreEqual(4, graph.EdgesCount());

            graph = InitGraph();

            graph.RemoveEdge("K3", "K1");
            Assert.AreEqual(5, graph.EdgesCount());
        }

        [TestMethod()]
        public void HasVertexTest()
        {
            var graph = InitGraph();

            Assert.IsTrue(graph.HasVertex("K1"));
            Assert.IsTrue(graph.HasVertex("K2"));
            Assert.IsTrue(graph.HasVertex("K3"));
            Assert.IsTrue(graph.HasVertex("K4"));
            Assert.IsTrue(graph.HasVertex("K5"));
            Assert.IsFalse(graph.HasVertex("K6"));
        }

        [TestMethod()]
        public void FindVertexTest()
        {
            var data = new TestVertexData();
            var graph = new Graph<string, TestVertexData, string, TestEdgeData>();
            
            graph.AddVertex("Vertex", data);
            var find = graph.FindVertex("Vertex");
            Assert.AreSame(data, find);
        }

        [TestMethod()]
        public void AddEdgeTest() 
        {
            var graph = InitGraph();

            Assert.AreEqual(6, graph.EdgesCount());

            Assert.ThrowsException<UniqueKeyException>(() => graph.AddEdge("H2", "K1", "K4", new TestEdgeData(5)));

            Assert.AreEqual(6, graph.EdgesCount());
            graph.AddEdge("H7", "K1", "K4", new TestEdgeData(5));
            Assert.AreEqual(7, graph.EdgesCount());

            Assert.ThrowsException<UniqueItemException>(() => graph.AddEdge("H8", "K1", "K4", new TestEdgeData(5)));

            Assert.ThrowsException<ItemNotFoundException>(() => graph.AddEdge("H9", "K9", "K11", new TestEdgeData(5)));
        }

        [TestMethod()]
        public void HasEdgeTest()
        {
            var graph = InitGraph();

            Assert.IsTrue(graph.HasEdge("H1"));
            Assert.IsTrue(graph.HasEdge("H2"));
            Assert.IsTrue(graph.HasEdge("H3"));
            Assert.IsTrue(graph.HasEdge("H4"));
            Assert.IsTrue(graph.HasEdge("H5"));
            Assert.IsTrue(graph.HasEdge("H6"));
            Assert.IsFalse(graph.HasEdge("H7"));
            Assert.IsFalse(graph.HasEdge("H66"));

            Assert.IsTrue(graph.HasEdge("K4", "K2"));
            Assert.IsTrue(graph.HasEdge("K2", "K1"));
            Assert.IsTrue(graph.HasEdge("K1", "K3"));
            Assert.IsTrue(graph.HasEdge("K3", "K5"));
            Assert.IsTrue(graph.HasEdge("K5", "K2"));
            Assert.IsTrue(graph.HasEdge("K2", "K3"));
            Assert.IsFalse(graph.HasEdge("K5", "K1"));
            Assert.IsFalse(graph.HasEdge("K2", "L5"));
        }

        [TestMethod()]
        public void FindEdgeTest()
        {
            var data = new TestEdgeData(5);
            var graph = new Graph<string, TestVertexData, string, TestEdgeData>();

            graph.AddVertex("V1", new TestVertexData());
            graph.AddVertex("V2", new TestVertexData());

            graph.AddEdge("E1", "V1", "V2", data);

            var find = graph.FindEdge("E1");
            Assert.AreSame(data, find);

            find = graph.FindEdge("V1", "V2");
            Assert.AreSame(data, find);

            find = graph.FindEdge("V2", "V1");
            Assert.AreSame(data, find);
        }

        [TestMethod()]
        public void GetAllVerticesTest()
        {
            var graph = InitGraph();
            var data = graph.GetAllVertices();

            Assert.AreEqual(5, graph.VerticesCount());
            Assert.AreEqual("K1", data[0].key);
            Assert.AreEqual("K2", data[1].key);
            Assert.AreEqual("K3", data[2].key);
            Assert.AreEqual("K4", data[3].key);
            Assert.AreEqual("K5", data[4].key);
        }

        [TestMethod()]
        public void GetAllEdgesTest()
        {
            var graph = InitGraph();
            var data = graph.GetAllEdges();

            Assert.AreEqual(6, graph.EdgesCount());

            Assert.AreEqual("H1", data[0].key);
            Assert.AreEqual("K1", data[0].start);
            Assert.AreEqual("K2", data[0].target);

            Assert.AreEqual("H2", data[1].key);
            Assert.AreEqual("K4", data[1].start);
            Assert.AreEqual("K2", data[1].target);

            Assert.AreEqual("H3", data[2].key);
            Assert.AreEqual("K2", data[2].start);
            Assert.AreEqual("K3", data[2].target);

            Assert.AreEqual("H4", data[3].key);
            Assert.AreEqual("K2", data[3].start);
            Assert.AreEqual("K5", data[3].target);

            Assert.AreEqual("H5", data[4].key);
            Assert.AreEqual("K3", data[4].start);
            Assert.AreEqual("K5", data[4].target);

            Assert.AreEqual("H6", data[5].key);
            Assert.AreEqual("K1", data[5].start);
            Assert.AreEqual("K3", data[5].target);

            graph.Clear();

            data = graph.GetAllEdges();
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Count());
        }

        [TestMethod()]
        public void GetVertexIncidentsTest()
        {
            var graph = InitGraph();
            var data = graph.GetVertexIncidents("K2");

            Assert.AreEqual(4, data.Count());

            Assert.AreEqual("H1", data[0].key);
            Assert.AreEqual("K1", data[0].target);

            Assert.AreEqual("H2", data[1].key);
            Assert.AreEqual("K4", data[1].target);

            Assert.AreEqual("H3", data[2].key);
            Assert.AreEqual("K3", data[2].target);

            Assert.AreEqual("H4", data[3].key);
            Assert.AreEqual("K5", data[3].target);

            graph.Clear();

            data = graph.GetVertexIncidents("K2");
            Assert.AreEqual(null, data);
        }

        [TestMethod()]
        public void GetEdgeIncidentsTest()
        {
            var graph = InitGraph();
            var data = graph.GetEdgeIncidents("H6");

            Assert.AreEqual(2, data.Count);

            Assert.AreEqual("K1", data[0].key);
            Assert.AreEqual("K3", data[1].key);

            graph.Clear();

            data = graph.GetEdgeIncidents("H6");
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Count());
        }

        [TestMethod()]
        public void GetEdgeIncidentsTwoTest()
        {
            var graph = InitGraph();
            var data = graph.GetEdgeIncidents("K1", "K3");

            Assert.AreEqual(2, data.Count);

            Assert.AreEqual("K1", data[0].key);
            Assert.AreEqual("K3", data[1].key);

            graph.Clear();

            data = graph.GetEdgeIncidents("K1", "K3");
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Count());
        }
    }
}