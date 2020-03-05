using GraphService.Properties;
using System;

namespace GraphService
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
