using System;

namespace RangeTree.Exceptions
{
    public class TreeIsAleadyBuilded : Exception
    {
        public TreeIsAleadyBuilded(string message) 
            : base(message)
        { }
    }
}
