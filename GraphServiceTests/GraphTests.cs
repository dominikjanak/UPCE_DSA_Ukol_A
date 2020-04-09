using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace GraphService.Tests
{
    [TestClass()]
    public class GraphTests
    {
        private Graph<string, TestVertexData, string, TestEdgeData> InitGraph()
        {
            var graph = new Graph<string, TestVertexData, string, TestEdgeData>();

            graph.AddVertex("S", new TestVertexData());
            graph.AddVertex("V1", new TestVertexData());
            graph.AddVertex("V2", new TestVertexData());
            graph.AddVertex("V3", new TestVertexData());
            graph.AddVertex("V4", new TestVertexData());
            graph.AddVertex("T", new TestVertexData());

            graph.AddEdge("H1", "S", "T", new TestEdgeData(10));
            graph.AddEdge("H2", "V1", "V2", new TestEdgeData(2));
            graph.AddEdge("H3", "V2", "T", new TestEdgeData(1));
            graph.AddEdge("H4", "S", "V1", new TestEdgeData(3));
            graph.AddEdge("H5", "V3", "V4", new TestEdgeData(3));
            graph.AddEdge("H6", "S", "V3", new TestEdgeData(2));
            graph.AddEdge("H7", "V4", "T", new TestEdgeData(4));

            return graph;
        }

        [TestMethod()]
        public void ClearTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());
            Assert.AreEqual(6, graph.VerticesCount());

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
        public void AddVertexSuccessTest()
        {
            var graph = InitGraph();
            Assert.AreEqual(6, graph.VerticesCount());
            graph.AddVertex("V5", new TestVertexData());
            Assert.AreEqual(7, graph.VerticesCount());
            Assert.IsTrue(graph.HasVertex("V5"));
        }

        [TestMethod()]
        public void VertexKeyAlreadyExistsTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(6, graph.VerticesCount());

            Assert.ThrowsException<UniqueKeyException>(() => graph.AddVertex("S", new TestVertexData()));

            Assert.AreEqual(6, graph.VerticesCount());
        }

        [TestMethod()]
        public void AddVertexKeyNullTest()
        {
            var graph = InitGraph();

            Assert.ThrowsException<ArgumentNullException>(() => graph.AddVertex(null, new TestVertexData()));
        }

        [TestMethod()]
        public void AddVertexDataNullTest()
        {
            var graph = InitGraph();

            Assert.ThrowsException<ArgumentNullException>(() => graph.AddVertex("V5", null));
            Assert.IsFalse(graph.HasVertex("V5"));
        }

        [TestMethod()]
        public void AddVertexKeyAndDataNullTest()
        {
            var graph = InitGraph();

            Assert.ThrowsException<ArgumentNullException>(() => graph.AddVertex(null, null));
        }

        [TestMethod()]
        public void RemoveVertexSuccessTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(2, graph.VertexIncidents("V3").Count());
            Assert.AreEqual(2, graph.VertexIncidents("V1").Count());
            Assert.AreEqual(3, graph.VertexIncidents("T").Count());

            Assert.IsTrue(graph.HasVertex("S"));

            Assert.IsTrue(graph.HasEdge("H6"));
            Assert.IsTrue(graph.HasEdge("H4"));
            Assert.IsTrue(graph.HasEdge("H1"));

            Assert.IsTrue(graph.HasEdge("S", "T"));
            Assert.IsTrue(graph.HasEdge("S", "V3"));
            Assert.IsTrue(graph.HasEdge("S", "V1"));

            Assert.IsTrue(graph.HasEdge("T", "S"));
            Assert.IsTrue(graph.HasEdge("V3", "S"));
            Assert.IsTrue(graph.HasEdge("V1", "S"));

            Assert.AreEqual(7, graph.EdgesCount());
            Assert.AreEqual(6, graph.VerticesCount());
            graph.RemoveVertex("S");
            Assert.AreEqual(4, graph.EdgesCount());
            Assert.AreEqual(5, graph.VerticesCount());

            Assert.IsFalse(graph.HasVertex("S"));

            Assert.IsFalse(graph.HasEdge("H6"));
            Assert.IsFalse(graph.HasEdge("H4"));
            Assert.IsFalse(graph.HasEdge("H1"));

            Assert.IsFalse(graph.HasEdge("S", "T"));
            Assert.IsFalse(graph.HasEdge("S", "V3"));
            Assert.IsFalse(graph.HasEdge("S", "V1"));

            Assert.IsFalse(graph.HasEdge("T", "S"));
            Assert.IsFalse(graph.HasEdge("V3", "S"));
            Assert.IsFalse(graph.HasEdge("V1", "S"));

            Assert.AreEqual(1, graph.VertexIncidents("V3").Count());
            Assert.AreEqual(1, graph.VertexIncidents("V1").Count());
            Assert.AreEqual(2, graph.VertexIncidents("T").Count());
        }

        [TestMethod()]
        public void RemoveNonExistsVertexTest()
        {
            var graph = InitGraph();
            Assert.AreEqual(6, graph.VerticesCount());
            Assert.ThrowsException<ItemNotFoundException>(() => graph.RemoveVertex("Z1"));
            Assert.AreEqual(6, graph.VerticesCount());
        }

        [TestMethod()]
        public void RemoveAlreadyRemovedTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(6, graph.VerticesCount());
            graph.RemoveVertex("S");
            Assert.AreEqual(5, graph.VerticesCount());

            Assert.ThrowsException<ItemNotFoundException>(() => graph.RemoveVertex("S"));
        }

        [TestMethod()]
        public void RemoveValidateDataTest()
        {
            var graph = InitGraph();
            var data = new TestVertexData();

            graph.AddVertex("Test", data);
            var ret = graph.RemoveVertex("Test");

            Assert.AreSame(data, ret);
        }

        [TestMethod()]
        public void HasVertexSuccessTest()
        {
            var graph = InitGraph();
            Assert.IsTrue(graph.HasVertex("S"));
            Assert.IsTrue(graph.HasVertex("T"));
        }

        [TestMethod()]
        public void HasVertexNotExistsTest()
        {
            var graph = InitGraph();
            Assert.IsFalse(graph.HasVertex("V"));
            Assert.IsFalse(graph.HasVertex("P"));
        }

        [TestMethod()]
        public void HasVertexNullTest()
        {
            var graph = InitGraph();
            Assert.ThrowsException<ArgumentNullException>(() => graph.HasVertex(null));
        }

        [TestMethod()]
        public void HasVertexRemovedTest()
        {
            var graph = InitGraph();
            Assert.IsTrue(graph.HasVertex("S"));
            graph.RemoveVertex("S");
            Assert.IsFalse(graph.HasVertex("S"));
        }

        [TestMethod()]
        public void FindVertexSuccessTest()
        {
            var graph = InitGraph();
            var data = new TestVertexData();

            graph.AddVertex("Test", data);
            var ret = graph.FindVertex("Test");

            Assert.AreSame(data, ret);
        }

        [TestMethod()]
        public void FindVertexNotExistsTest()
        {
            var graph = InitGraph();

            var res = graph.FindVertex("Test");
            Assert.IsNull(res);
        }

        [TestMethod()]
        public void FindVertexNullTest()
        {
            var graph = InitGraph();

            Assert.ThrowsException<ArgumentNullException>(() => graph.FindVertex(null));
        }

        [TestMethod()]
        public void FindVertexRemovedTest()
        {
            var graph = InitGraph();
            graph.RemoveVertex("S");

            var res = graph.FindVertex("S");
            Assert.IsNull(res);
        }

        [TestMethod()]
        public void AddEdgeSuccessTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());
            Assert.IsFalse(graph.HasEdge("H8"));
            Assert.IsFalse(graph.HasEdge("V1", "V3"));
            Assert.IsFalse(graph.HasEdge("V3", "V1"));
            Assert.AreEqual(2, graph.VertexIncidents("V3").Count());

            Assert.ThrowsException<ItemNotFoundException>(() => graph.EdgeIncidents("V1", "V3").ElementAt(0).Key);
            Assert.ThrowsException<ItemNotFoundException>(() => graph.EdgeIncidents("V1", "V3").ElementAt(1).Key);
            Assert.ThrowsException<ItemNotFoundException>(() => graph.EdgeIncidents("H8").ElementAt(0).Key);
            Assert.ThrowsException<ItemNotFoundException>(() => graph.EdgeIncidents("H8").ElementAt(1).Key);

            graph.AddEdge("H8", "V1", "V3", new TestEdgeData(5));

            Assert.AreEqual(2, graph.EdgeIncidents("H8").Count());
            Assert.AreEqual("V1", graph.EdgeIncidents("V1", "V3").ElementAt(0).Key);
            Assert.AreEqual("V3", graph.EdgeIncidents("V1", "V3").ElementAt(1).Key);
            Assert.AreEqual("V1", graph.EdgeIncidents("H8").ElementAt(0).Key);
            Assert.AreEqual("V3", graph.EdgeIncidents("H8").ElementAt(1).Key);

            Assert.AreEqual(3, graph.VertexIncidents("V3").Count());
            Assert.AreEqual("H5", graph.VertexIncidents("V3").ElementAt(0).Key);
            Assert.AreEqual("V4", graph.VertexIncidents("V3").ElementAt(0).Target);
            Assert.AreEqual("H6", graph.VertexIncidents("V3").ElementAt(1).Key);
            Assert.AreEqual("S", graph.VertexIncidents("V3").ElementAt(1).Target);
            Assert.AreEqual("H8", graph.VertexIncidents("V3").ElementAt(2).Key);
            Assert.AreEqual("V1", graph.VertexIncidents("V3").ElementAt(2).Target);

            Assert.IsTrue(graph.HasEdge("H8"));
            Assert.IsTrue(graph.HasEdge("V1", "V3"));
            Assert.IsTrue(graph.HasEdge("V3", "V1"));
            Assert.AreEqual(8, graph.EdgesCount());
        }

        [TestMethod()]
        public void AddEdgeEdgeAlreadyExistsTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());
            graph.AddEdge("H8", "V1", "V3", new TestEdgeData(5));
            Assert.AreEqual(8, graph.EdgesCount());
            Assert.ThrowsException<UniqueItemException>(() => graph.AddEdge("H9", "V1", "V3", new TestEdgeData(5)));
        }

        [TestMethod()]
        public void AddEdgeEdgeKeyAlreadyExistsTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<UniqueKeyException>(() => graph.AddEdge("H7", "V1", "V3", new TestEdgeData(5)));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void AddEdgeSameStartTargetTest()
        {
            var graph = InitGraph();
            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<EdgeWithSameVerticesException>(() => graph.AddEdge("H8", "V1", "V1", new TestEdgeData(5)));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void AddEdgeStartNotExistsTest()
        {
            var graph = InitGraph();
            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ItemNotFoundException>(() => graph.AddEdge("H8", "T5", "V1", new TestEdgeData(5)));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void TargetNotExistsTest()
        {
            var graph = InitGraph();
            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ItemNotFoundException>(() => graph.AddEdge("H8", "V1", "T9", new TestEdgeData(5)));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void AddEdgeVerticesNotExistTest()
        {
            var graph = InitGraph();
            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ItemNotFoundException>(() => graph.AddEdge("H8", "T5", "T9", new TestEdgeData(5)));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void AddEdgeNullKeyTest()
        {
            var graph = InitGraph();
            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ArgumentNullException>(() => graph.AddEdge(null, "T5", "V1", new TestEdgeData(5)));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void AddEdgeNullDataTest()
        {
            var graph = InitGraph();
            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ArgumentNullException>(() => graph.AddEdge("H8", "T5", "V1", null));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void AddEdgeNullStartTest()
        {
            var graph = InitGraph();
            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ArgumentNullException>(() => graph.AddEdge("H8", null, "V1", new TestEdgeData(5)));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void AddEdgeNullTargetTest()
        {
            var graph = InitGraph();
            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ArgumentNullException>(() => graph.AddEdge("H8", "V1", null, new TestEdgeData(5)));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void RemoveEdgeSuccessByKeyTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());

            Assert.AreEqual(3, graph.VertexIncidents("S").Count());
            Assert.AreEqual("H1", graph.VertexIncidents("S").ElementAt(0).Key);
            Assert.AreEqual("T", graph.VertexIncidents("S").ElementAt(0).Target);
            Assert.AreEqual("H4", graph.VertexIncidents("S").ElementAt(1).Key);
            Assert.AreEqual("V1", graph.VertexIncidents("S").ElementAt(1).Target);
            Assert.AreEqual("H6", graph.VertexIncidents("S").ElementAt(2).Key);
            Assert.AreEqual("V3", graph.VertexIncidents("S").ElementAt(2).Target);

            Assert.AreEqual(3, graph.VertexIncidents("T").Count());
            Assert.AreEqual("H1", graph.VertexIncidents("T").ElementAt(0).Key);
            Assert.AreEqual("S", graph.VertexIncidents("T").ElementAt(0).Target);
            Assert.AreEqual("H3", graph.VertexIncidents("T").ElementAt(1).Key);
            Assert.AreEqual("V2", graph.VertexIncidents("T").ElementAt(1).Target);
            Assert.AreEqual("H7", graph.VertexIncidents("T").ElementAt(2).Key);
            Assert.AreEqual("V4", graph.VertexIncidents("T").ElementAt(2).Target);

            graph.RemoveEdge("H1");

            Assert.AreEqual(6, graph.EdgesCount());
            Assert.AreEqual(2, graph.VertexIncidents("S").Count());
            Assert.AreEqual("H4", graph.VertexIncidents("S").ElementAt(0).Key);
            Assert.AreEqual("V1", graph.VertexIncidents("S").ElementAt(0).Target);
            Assert.AreEqual("H6", graph.VertexIncidents("S").ElementAt(1).Key);
            Assert.AreEqual("V3", graph.VertexIncidents("S").ElementAt(1).Target);

            Assert.AreEqual(2, graph.VertexIncidents("T").Count());
            Assert.AreEqual("H3", graph.VertexIncidents("T").ElementAt(0).Key);
            Assert.AreEqual("V2", graph.VertexIncidents("T").ElementAt(0).Target);
            Assert.AreEqual("H7", graph.VertexIncidents("T").ElementAt(1).Key);
            Assert.AreEqual("V4", graph.VertexIncidents("T").ElementAt(1).Target);
        }

        [TestMethod()]
        public void RemoveEdgeSuccessByVerticesReversedTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());

            Assert.AreEqual(3, graph.VertexIncidents("S").Count());
            Assert.AreEqual("H1", graph.VertexIncidents("S").ElementAt(0).Key);
            Assert.AreEqual("T", graph.VertexIncidents("S").ElementAt(0).Target);
            Assert.AreEqual("H4", graph.VertexIncidents("S").ElementAt(1).Key);
            Assert.AreEqual("V1", graph.VertexIncidents("S").ElementAt(1).Target);
            Assert.AreEqual("H6", graph.VertexIncidents("S").ElementAt(2).Key);
            Assert.AreEqual("V3", graph.VertexIncidents("S").ElementAt(2).Target);

            Assert.AreEqual(3, graph.VertexIncidents("T").Count());
            Assert.AreEqual("H1", graph.VertexIncidents("T").ElementAt(0).Key);
            Assert.AreEqual("S", graph.VertexIncidents("T").ElementAt(0).Target);
            Assert.AreEqual("H3", graph.VertexIncidents("T").ElementAt(1).Key);
            Assert.AreEqual("V2", graph.VertexIncidents("T").ElementAt(1).Target);
            Assert.AreEqual("H7", graph.VertexIncidents("T").ElementAt(2).Key);
            Assert.AreEqual("V4", graph.VertexIncidents("T").ElementAt(2).Target);

            graph.RemoveEdge("T", "S");
            
            Assert.AreEqual(6, graph.EdgesCount());
            Assert.AreEqual(2, graph.VertexIncidents("S").Count());
            Assert.AreEqual("H4", graph.VertexIncidents("S").ElementAt(0).Key);
            Assert.AreEqual("V1", graph.VertexIncidents("S").ElementAt(0).Target);
            Assert.AreEqual("H6", graph.VertexIncidents("S").ElementAt(1).Key);
            Assert.AreEqual("V3", graph.VertexIncidents("S").ElementAt(1).Target);

            Assert.AreEqual(2, graph.VertexIncidents("T").Count());
            Assert.AreEqual("H3", graph.VertexIncidents("T").ElementAt(0).Key);
            Assert.AreEqual("V2", graph.VertexIncidents("T").ElementAt(0).Target);
            Assert.AreEqual("H7", graph.VertexIncidents("T").ElementAt(1).Key);
            Assert.AreEqual("V4", graph.VertexIncidents("T").ElementAt(1).Target);
        }

        [TestMethod()]
        public void RemoveEdgeSuccessByVerticesTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());

            Assert.AreEqual(3, graph.VertexIncidents("S").Count());
            Assert.AreEqual("H1", graph.VertexIncidents("S").ElementAt(0).Key);
            Assert.AreEqual("T", graph.VertexIncidents("S").ElementAt(0).Target);
            Assert.AreEqual("H4", graph.VertexIncidents("S").ElementAt(1).Key);
            Assert.AreEqual("V1", graph.VertexIncidents("S").ElementAt(1).Target);
            Assert.AreEqual("H6", graph.VertexIncidents("S").ElementAt(2).Key);
            Assert.AreEqual("V3", graph.VertexIncidents("S").ElementAt(2).Target);

            Assert.AreEqual(3, graph.VertexIncidents("T").Count());
            Assert.AreEqual("H1", graph.VertexIncidents("T").ElementAt(0).Key);
            Assert.AreEqual("S", graph.VertexIncidents("T").ElementAt(0).Target);
            Assert.AreEqual("H3", graph.VertexIncidents("T").ElementAt(1).Key);
            Assert.AreEqual("V2", graph.VertexIncidents("T").ElementAt(1).Target);
            Assert.AreEqual("H7", graph.VertexIncidents("T").ElementAt(2).Key);
            Assert.AreEqual("V4", graph.VertexIncidents("T").ElementAt(2).Target);

            graph.RemoveEdge("S", "T");
            
            Assert.AreEqual(6, graph.EdgesCount());
            Assert.AreEqual(2, graph.VertexIncidents("S").Count());
            Assert.AreEqual("H4", graph.VertexIncidents("S").ElementAt(0).Key);
            Assert.AreEqual("V1", graph.VertexIncidents("S").ElementAt(0).Target);
            Assert.AreEqual("H6", graph.VertexIncidents("S").ElementAt(1).Key);
            Assert.AreEqual("V3", graph.VertexIncidents("S").ElementAt(1).Target);

            Assert.AreEqual(2, graph.VertexIncidents("T").Count());
            Assert.AreEqual("H3", graph.VertexIncidents("T").ElementAt(0).Key);
            Assert.AreEqual("V2", graph.VertexIncidents("T").ElementAt(0).Target);
            Assert.AreEqual("H7", graph.VertexIncidents("T").ElementAt(1).Key);
            Assert.AreEqual("V4", graph.VertexIncidents("T").ElementAt(1).Target);
        }

        [TestMethod()]
        public void RemoveEdgeKeyNotExistsTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ItemNotFoundException>(() => graph.RemoveEdge("H10"));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void RemoveEdgeStartNotExistsTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ItemNotFoundException>(() => graph.RemoveEdge("V55", "T"));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void RemoveEdgeTargetNotExistsTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ItemNotFoundException>(() => graph.RemoveEdge("S", "V55"));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void RemoveEdgeVerticesNotExistsTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ItemNotFoundException>(() => graph.RemoveEdge("V54", "V55"));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void RemoveEdgeStartNullTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ArgumentNullException>(() => graph.RemoveEdge(null, "T"));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void RemoveEdgeTargetNullTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ArgumentNullException>(() => graph.RemoveEdge("S", null));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void RemoveEdgeVerticesNullTest()
        {
            var graph = InitGraph();

            Assert.AreEqual(7, graph.EdgesCount());
            Assert.ThrowsException<ArgumentNullException>(() => graph.RemoveEdge(null, null));
            Assert.AreEqual(7, graph.EdgesCount());
        }

        [TestMethod()]
        public void HasEdgeSuccessByKeyTest()
        {
            var graph = InitGraph();
            Assert.IsTrue(graph.HasEdge("H1"));
        }

        [TestMethod()]
        public void HasEdgeSuccessByVerticesTest()
        {
            var graph = InitGraph();
            Assert.IsTrue(graph.HasEdge("S", "T"));
        }

        [TestMethod()]
        public void HasEdgeSuccessByVerticesReversedTest()
        {
            var graph = InitGraph();
            Assert.IsTrue(graph.HasEdge("T", "S"));
        }

        [TestMethod()]
        public void HasEdgeNotExistsByKeyTest()
        {
            var graph = InitGraph();
            Assert.IsFalse(graph.HasEdge("H55"));
        }

        [TestMethod()]
        public void HasEdgeNotExistsByVerticesTest()
        {
            var graph = InitGraph();
            Assert.IsFalse(graph.HasEdge("S", "V4"));
        }

        [TestMethod()]
        public void HasEdgeNotExistsStartTest()
        {
            var graph = InitGraph();
            Assert.IsFalse(graph.HasEdge("L5", "V4"));
        }

        [TestMethod()]
        public void HasEdgeNotExistsTargetTest()
        {
            var graph = InitGraph();
            Assert.IsFalse(graph.HasEdge("S", "L5"));
        }

        [TestMethod()]
        public void HasEdgeRemovedByKeyTest()
        {
            var graph = InitGraph();
            Assert.IsTrue(graph.HasEdge("H1"));
            graph.RemoveEdge("H1");
            Assert.IsFalse(graph.HasEdge("H1"));
        }

        [TestMethod()]
        public void HasEdgeRemovedByVerticesTest()
        {
            var graph = InitGraph();
            Assert.IsTrue(graph.HasEdge("S", "T"));
            graph.RemoveEdge("S", "T");
            Assert.IsFalse(graph.HasEdge("S", "T"));
        }

        [TestMethod()]
        public void HasEdgeRemovedByVerticesReversedTest()
        {
            var graph = InitGraph();
            Assert.IsTrue(graph.HasEdge("T", "S"));
            graph.RemoveEdge("S", "T");
            Assert.IsFalse(graph.HasEdge("T", "S"));
        }

        [TestMethod()]
        public void HasEdgeNullKeyTest()
        {
            var graph = InitGraph();
            Assert.ThrowsException<ArgumentNullException>(() => graph.HasEdge(null));
        }

        [TestMethod()]
        public void HasEdgeNullStartTest()
        {
            var graph = InitGraph();
            Assert.ThrowsException<ArgumentNullException>(() => graph.HasEdge(null, "S"));
        }

        [TestMethod()]
        public void HasEdgeNullTargetTest()
        {
            var graph = InitGraph();
            Assert.ThrowsException<ArgumentNullException>(() => graph.HasEdge("S", null));
        }

        [TestMethod()]
        public void FindEdgeSuccessByKeyTest()
        {
            var graph = InitGraph();
            var data = new TestEdgeData(25);

            graph.AddEdge("E1", "V3", "V2", data);

            var res = graph.FindEdge("E1");

            Assert.AreSame(data, res);
        }

        [TestMethod()]
        public void FindEdgeSuccessByVerticesTest()
        {
            var graph = InitGraph(); var data = new TestEdgeData(25);

            graph.AddEdge("E1", "V3", "V2", data);
            var res = graph.FindEdge("V3", "V2");

            Assert.AreSame(data, res);
        }

        [TestMethod()]
        public void FindEdgeSuccessByVerticesRevesedTest()
        {
            var graph = InitGraph(); var data = new TestEdgeData(25);

            graph.AddEdge("E1", "V3", "V2", data);
            var res = graph.FindEdge("V2", "V3");

            Assert.AreSame(data, res);
        }

        [TestMethod()]
        public void FindEdgeNotExistsByKeyTest()
        {
            var graph = InitGraph();

            var res = graph.FindEdge("E1");
            Assert.IsNull(res);
        }

        [TestMethod()]
        public void FindEdgeNotExistsByVerticesTest()
        {
            var graph = InitGraph();

            var res = graph.FindEdge("S", "V4");
            Assert.IsNull(res);
        }

        [TestMethod()]
        public void FindEdgeStartNotExistsTest()
        {
            var graph = InitGraph();

            var res = graph.FindEdge("P45", "T");
            Assert.IsNull(res);
        }

        [TestMethod()]
        public void FindEdgeTargetNotExistsTest()
        {
            var graph = InitGraph();

            var res = graph.FindEdge("T", "P45");
            Assert.IsNull(res);
        }

        [TestMethod()]
        public void FindEdgeRemovedByKeyTest()
        {
            var graph = InitGraph();
            var data = new TestEdgeData(25);

            graph.AddEdge("E1", "V3", "V2", data);

            var res = graph.FindEdge("E1");
            Assert.AreSame(data, res);

            graph.RemoveEdge("E1");

            res = graph.FindEdge("E1");
            Assert.IsNull(res);
        }

        [TestMethod()]
        public void FindEdgeRemovedByVerticesTest()
        {
            var graph = InitGraph();
            var data = new TestEdgeData(25);

            graph.AddEdge("E1", "V3", "V2", data);

            var res = graph.FindEdge("V3", "V2");
            Assert.AreSame(data, res);

            graph.RemoveEdge("E1");

            res = graph.FindEdge("E1");
            Assert.IsNull(res);
        }

        [TestMethod()]
        public void FindEdgeStartNullTest()
        {
            var graph = InitGraph();

            Assert.ThrowsException<ArgumentNullException>(() => graph.FindEdge(null, "T"));
        }

        [TestMethod()]
        public void FindEdgeTargetNullTest()
        {
            var graph = InitGraph();

            Assert.ThrowsException<ArgumentNullException>(() => graph.FindEdge("T", null));
        }

        [TestMethod()]
        public void VerticesEnumeratorExistsTest()
        {
            var graph = InitGraph();
            var data = graph.Vertices;

            Assert.AreEqual(6, graph.VerticesCount());
            Assert.AreEqual("S", data.ElementAt(0).Key);
            Assert.AreEqual("V1", data.ElementAt(1).Key);
            Assert.AreEqual("V2", data.ElementAt(2).Key);
            Assert.AreEqual("V3", data.ElementAt(3).Key);
            Assert.AreEqual("V4", data.ElementAt(4).Key);
            Assert.AreEqual("T", data.ElementAt(5).Key);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => data.ElementAt(6).Key);
        }

        [TestMethod()]
        public void VerticesEnumeratorEmptyTest()
        {
            var graph = InitGraph();
            graph.RemoveVertex("S");
            graph.RemoveVertex("V1");
            graph.RemoveVertex("V2");
            graph.RemoveVertex("V3");
            graph.RemoveVertex("V4");
            graph.RemoveVertex("T");

            var data = graph.Vertices;

            Assert.AreEqual(0, graph.VerticesCount());

            foreach(var e in data)
            {
                Assert.IsTrue(false);
            }

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => data.ElementAt(0).Key);
        }

        [TestMethod()]
        public void EdgesEnumeratorExistsTest()
        {
            var graph = InitGraph();
            var data = graph.Edges;

            Assert.AreEqual(7, graph.EdgesCount());
            Assert.AreEqual("H1", data.ElementAt(0).Key);
            Assert.AreEqual("S", data.ElementAt(0).Start);
            Assert.AreEqual("T", data.ElementAt(0).Target);
            Assert.AreEqual("H2", data.ElementAt(1).Key);
            Assert.AreEqual("V1", data.ElementAt(1).Start);
            Assert.AreEqual("V2", data.ElementAt(1).Target);
            Assert.AreEqual("H3", data.ElementAt(2).Key);
            Assert.AreEqual("V2", data.ElementAt(2).Start);
            Assert.AreEqual("T", data.ElementAt(2).Target);
            Assert.AreEqual("H4", data.ElementAt(3).Key);
            Assert.AreEqual("S", data.ElementAt(3).Start);
            Assert.AreEqual("V1", data.ElementAt(3).Target);
            Assert.AreEqual("H5", data.ElementAt(4).Key);
            Assert.AreEqual("V3", data.ElementAt(4).Start);
            Assert.AreEqual("V4", data.ElementAt(4).Target);
            Assert.AreEqual("H6", data.ElementAt(5).Key);
            Assert.AreEqual("S", data.ElementAt(5).Start);
            Assert.AreEqual("V3", data.ElementAt(5).Target);
            Assert.AreEqual("H7", data.ElementAt(6).Key);
            Assert.AreEqual("V4", data.ElementAt(6).Start);
            Assert.AreEqual("T", data.ElementAt(6).Target);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => data.ElementAt(7).Key);
        }

        [TestMethod()]
        public void EdgesEnumeratorEmptyTest()
        {
            var graph = InitGraph();
            graph.RemoveEdge("H1");
            graph.RemoveEdge("H2");
            graph.RemoveEdge("H3");
            graph.RemoveEdge("H4");
            graph.RemoveEdge("H5");
            graph.RemoveEdge("H6");
            graph.RemoveEdge("H7");

            var data = graph.Edges;

            Assert.AreEqual(0, graph.EdgesCount());

            foreach (var e in data)
            {
                Assert.IsTrue(false);
            }

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => data.ElementAt(0).Key);
        }

        [TestMethod()]
        public void GetVertexIncidentsSuccessTest()
        {
            var graph = InitGraph();
            var data = graph.VertexIncidents("S");

            Assert.AreEqual(3, data.Count());

            Assert.AreEqual("H1", data.ElementAt(0).Key);
            Assert.AreEqual("T", data.ElementAt(0).Target);

            Assert.AreEqual("H4", data.ElementAt(1).Key);
            Assert.AreEqual("V1", data.ElementAt(1).Target);

            Assert.AreEqual("H6", data.ElementAt(2).Key);
            Assert.AreEqual("V3", data.ElementAt(2).Target);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => data.ElementAt(3).Key);
        }

        [TestMethod()]
        public void GetVertexIncidentsEmptyTest()
        {
            var graph = InitGraph();

            graph.RemoveEdge("H1");
            graph.RemoveEdge("H4");
            graph.RemoveEdge("H6");
            var data = graph.VertexIncidents("S");

            Assert.AreEqual(0, data.Count());
            foreach (var e in data)
            {
                Assert.IsTrue(false);
            }

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => data.ElementAt(0).Key);
        }

        [TestMethod()]
        public void GetVertexIncidentsKeyNotExistsTest()
        {
            var graph = InitGraph();
                        
            Assert.ThrowsException<ItemNotFoundException>(() => graph.VertexIncidents("Z"));
        }

        [TestMethod()]
        public void GetVertexIncidentsNullTest()
        {
            var graph = InitGraph();
                        
            Assert.ThrowsException<ArgumentNullException>(() => graph.VertexIncidents(null));
        }

        [TestMethod()]
        public void GetEdgeIncidentsSuccessByKeyTest()
        {
            var graph = InitGraph();
            var data = graph.EdgeIncidents("H1");

            Assert.AreEqual(2, data.Count());

            Assert.AreEqual("S", data.ElementAt(0).Key);
            Assert.AreEqual("T", data.ElementAt(1).Key);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => data.ElementAt(2));
        }

        [TestMethod()]
        public void GetEdgeIncidentsSuccessByVerticesTest()
        {
            var graph = InitGraph();
            var data = graph.EdgeIncidents("S", "T");

            Assert.AreEqual(2, data.Count());

            Assert.AreEqual("S", data.ElementAt(0).Key);
            Assert.AreEqual("T", data.ElementAt(1).Key);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => data.ElementAt(2));
        }

        [TestMethod()]
        public void GetEdgeIncidentsSuccessByVerticesReversedTest()
        {
            var graph = InitGraph();
            var data = graph.EdgeIncidents("T", "S");

            Assert.AreEqual(2, data.Count());

            Assert.AreEqual("S", data.ElementAt(0).Key);
            Assert.AreEqual("T", data.ElementAt(1).Key);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => data.ElementAt(2));
        }

        [TestMethod()]
        public void GetEdgeIncidentsKeyNullTest()
        {
            var graph = InitGraph();

            Assert.ThrowsException<ArgumentNullException>(() => graph.EdgeIncidents(null));
        }

        [TestMethod()]
        public void GetEdgeIncidentsStartNullTest()
        {
            var graph = InitGraph();

            Assert.ThrowsException<ArgumentNullException>(() => graph.EdgeIncidents(null, "T"));
        }

        [TestMethod()]
        public void GetEdgeIncidentsTargetNullTest()
        {
            var graph = InitGraph();

            Assert.ThrowsException<ArgumentNullException>(() => graph.EdgeIncidents("S", null));
        }
    }
}
 