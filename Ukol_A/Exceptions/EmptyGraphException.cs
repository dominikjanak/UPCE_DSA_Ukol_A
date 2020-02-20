using System;

namespace Ukol_A.Exceptions
{
    public class EmptyGraphException : Exception
    {
        public EmptyGraphException()
        {
        }

        public EmptyGraphException(string message)
            : base(message)
        {
        }

        public EmptyGraphException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
