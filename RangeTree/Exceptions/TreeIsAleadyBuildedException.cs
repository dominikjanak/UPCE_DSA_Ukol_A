using System;

namespace RangeTree.Exceptions
{
    public class TreeIsAleadyBuildedException : Exception
    {
        public TreeIsAleadyBuildedException(string message) 
            : base(message)
        { }
    }
}
