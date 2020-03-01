using GraphService.Properties;
using System;
using System.Runtime.Serialization;
using System.Security;

namespace ForestGraph
{
    class GraphNotExploredException : Exception
    {
        public GraphNotExploredException(string message) 
            : base(message)
        { }

        public GraphNotExploredException()
            : base(Resources.GraphNotExplored)
        { }
    }
}
