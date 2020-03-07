using GraphService.Properties;
using System;

namespace GraphService
{
    public class EdgeWithSameVerticesException : Exception
    {
        public EdgeWithSameVerticesException(string message) 
            : base(message)
        { }
    }
}
