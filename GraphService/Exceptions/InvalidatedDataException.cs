using System;

namespace GraphService
{
    public class InvalidatedDataException : Exception
    {
        public InvalidatedDataException(string message) 
            : base(message)
        { }
    }
}
