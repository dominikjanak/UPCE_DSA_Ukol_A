using System;

namespace BinaryDataStorageEngine
{
    public class AlreadyInitializedException : Exception
    {
        public AlreadyInitializedException(string message)
            : base(message)
        { }
    }
}
