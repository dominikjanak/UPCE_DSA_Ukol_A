using GraphService.Properties;
using System;

namespace GraphService
{
    class UniqueKeyException : Exception
    {
        public UniqueKeyException(string message) 
            : base(message)
        { }

        public UniqueKeyException()
            : base(Resources.KeyAlreadyExists)
        { }
    }
}
