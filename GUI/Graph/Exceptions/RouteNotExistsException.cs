using GUI.Properties;
using System;

namespace GUI.Graph
{
    public class RouteNotExistsException : Exception
    {
        public RouteNotExistsException(string message) 
            : base(message)
        { }

        public RouteNotExistsException()
            : base(Resources.RouteNotExists)
        { }
    }
}
