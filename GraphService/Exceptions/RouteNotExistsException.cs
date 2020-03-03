using GraphService.Properties;
using System;

namespace GraphService
{
    class RouteNotExistsException : Exception
    {
        public RouteNotExistsException(string message) 
            : base(message)
        { }

        public RouteNotExistsException()
            : base(Resources.RouteNotExists)
        { }
    }
}
