using System;

namespace BinaryDataStorageEngine
{
    class InitializationException : Exception
    {
        public InitializationException(string message)
            : base(message)
        { }
    }
}
