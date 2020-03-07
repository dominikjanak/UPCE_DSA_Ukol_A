using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphService.Dijkstra;
using System;
using System.Collections.Generic;

namespace GraphService.Tests
{
    [TestClass()]
    public class DijkstraAlhorithmTests
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

            graph.AddEdge("H1", "S", "T", new TestEdgeData(20));

            graph.AddEdge("H2", "S", "V3", new TestEdgeData(3));
            graph.AddEdge("H3", "V3", "V4", new TestEdgeData(2));
            graph.AddEdge("H4", "V4", "T", new TestEdgeData(5));
            
            graph.AddEdge("H5", "S", "V1", new TestEdgeData(5));
            graph.AddEdge("H6", "V1", "V2", new TestEdgeData(3));
            graph.AddEdge("H7", "V2", "T", new TestEdgeData(3));

            return graph;
        }

        [TestMethod()]
        public void GetPathTest()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);

            dijkstra.FindPaths("S");
            var path = dijkstra.GetPath("T");

            CollectionAssert.AreEqual(new List<string>() { "S", "V3", "V4", "T" }, path);
        }

        [TestMethod()]
        public void GetPathBlocked_1Test()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);

            graph.FindEdge("V3", "V4").Block();

            dijkstra.FindPaths("S");
            var path = dijkstra.GetPath("T");

            CollectionAssert.AreEqual(new List<string>() { "S", "V1", "V2", "T" }, path);
        }

        [TestMethod()]
        public void GetPathBlocked_2Test()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);

            graph.FindEdge("V3", "V4").Block();
            graph.FindEdge("V1", "V2").Block();

            dijkstra.FindPaths("S");
            var path = dijkstra.GetPath("T");

            CollectionAssert.AreEqual(new List<string>() { "S", "T" }, path);
        }

        [TestMethod()]
        public void GetPathNoRouteExistsTest()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);

            graph.FindEdge("V3", "V4").Block();
            graph.FindEdge("V1", "V2").Block();
            graph.FindEdge("S", "T").Block();

            dijkstra.FindPaths("S");
            var path = dijkstra.GetPath("T");

            CollectionAssert.AreEqual(new List<string>() { }, path);
        }
        
        [TestMethod()]
        public void IgnoreBlockedTest()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);
        
            dijkstra.FindPaths("S", true);

            graph.FindEdge("V3", "V4").Block();
            graph.FindEdge("V1", "V2").Block();
            graph.FindEdge("S", "T").Block();

            Assert.IsFalse(dijkstra.IsInvalidated());
            
            var path = dijkstra.GetPath("T");
            CollectionAssert.AreEqual(new List<string>() { "S", "V3", "V4", "T" }, path);
        }


        [TestMethod()]
        public void FindPathNullStart()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);
        
            Assert.ThrowsException<ArgumentNullException>(() => dijkstra.FindPaths(null));
        }

        [TestMethod()]
        public void GraphNotExplored()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);

            Assert.ThrowsException<GraphNotExploredException>(() => dijkstra.GetPath("T"));
        }

        [TestMethod()]
        public void GetPathNullTarget()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);
            dijkstra.FindPaths("T");

            Assert.ThrowsException<ArgumentNullException>(() => dijkstra.GetPath(null));
        }

        [TestMethod()]
        public void FindPathTargetNotExists()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);

            Assert.ThrowsException<ItemNotFoundException>(() => dijkstra.FindPaths("L"));
        }

        [TestMethod()]
        public void GetPathStartNotExists()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);
            dijkstra.FindPaths("S");

            Assert.ThrowsException<ItemNotFoundException>(() => dijkstra.GetPath("L"));
        }

        [TestMethod()]
        public void ChangeIgnoreBlocked()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);

            graph.FindEdge("V3", "V4").Block();

            dijkstra.FindPaths("S");
            var path = dijkstra.GetPath("T");
            CollectionAssert.AreEqual(new List<string>() { "S", "V1", "V2", "T" }, path);

            dijkstra.FindPaths("S", true);
            path = dijkstra.GetPath("T");
            CollectionAssert.AreEqual(new List<string>() { "S", "V3", "V4", "T" }, path);
        }

        [TestMethod()]
        public void ChangeStartTest()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);

            dijkstra.FindPaths("S");
            var path = dijkstra.GetPath("T");
            CollectionAssert.AreEqual(new List<string>() { "S", "V3", "V4", "T" }, path);

            dijkstra.FindPaths("T");
            path = dijkstra.GetPath("S");
            CollectionAssert.AreEqual(new List<string>() { "T", "V4", "V3", "S" }, path);
        }

        [TestMethod()]
        public void FindPathsInvalidate()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);

            dijkstra.FindPaths("S");
            var path = dijkstra.GetPath("T");
            CollectionAssert.AreEqual(new List<string>() { "S", "V3", "V4", "T" }, path);

            dijkstra.Invalidate();
            dijkstra.FindPaths("S");
            path = dijkstra.GetPath("T");
            CollectionAssert.AreEqual(new List<string>() { "S", "V3", "V4", "T" }, path);
        }

        [TestMethod()]
        public void GetPathOnInvalidate()
        {
            var graph = InitGraph();
            var dijkstra = new DijkstraAlhorithm<string, TestVertexData, string, TestEdgeData>(graph);

            dijkstra.FindPaths("S");
            var path = dijkstra.GetPath("T");
            CollectionAssert.AreEqual(new List<string>() { "S", "V3", "V4", "T" }, path);

            dijkstra.Invalidate();
            Assert.ThrowsException<InvalidatedDataException>(() => dijkstra.GetPath("T"));
        }
    }
}