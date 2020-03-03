using GraphService.Properties;
using System;

namespace GraphService
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
