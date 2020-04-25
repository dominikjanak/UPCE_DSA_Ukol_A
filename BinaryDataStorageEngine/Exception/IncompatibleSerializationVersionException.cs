using System;

namespace BinaryDataStorageEngine
{
    class IncompatibleSerializationVersionException : Exception
    {
        public IncompatibleSerializationVersionException(string message)
            : base(message)
        { }
    }
}
