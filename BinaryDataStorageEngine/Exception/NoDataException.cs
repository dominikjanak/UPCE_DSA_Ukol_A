using System;

namespace BinaryDataStorageEngine
{
    public class NoDataException : Exception
    {
        public NoDataException(string message)
            : base(message)
        { }
    }
}
