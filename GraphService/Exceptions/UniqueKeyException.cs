using System;

namespace GraphService
{
    public class UniqueKeyException : Exception
    {
        public UniqueKeyException(string message) 
            : base(message)
        { }
    }
}
