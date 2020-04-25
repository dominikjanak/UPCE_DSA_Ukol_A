using System;

namespace BinaryDataStorageEngine
{
    public class IncompatibleSerializationVersionException : Exception
    {
        public IncompatibleSerializationVersionException(string message)
            : base(message)
        { }
    }
}
