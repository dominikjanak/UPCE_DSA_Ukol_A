using System;

namespace ForestGraph
{
    class RouteNotExistsException : Exception
    {
        public RouteNotExistsException(string message) 
            : base(message)
        { }

        public RouteNotExistsException()
            : base("Cesta mezi body neexistuje!")
        { }
    }
}
