using GraphService.Properties;
using System;

namespace ForestGraph
{
    class InvalidatedDataException : Exception
    {
        public InvalidatedDataException(string message) 
            : base(message)
        { }

        public InvalidatedDataException()
            : base(Resources.InvalidData)
        { }
    }
}
