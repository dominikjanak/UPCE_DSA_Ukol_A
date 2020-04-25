using System;

namespace BinaryDataStorageEngine
{
    class AlreadyInitializedException : Exception
    {
        public AlreadyInitializedException(string message)
            : base(message)
        { }
    }
}
