using GraphService.Properties;
using System;

namespace GraphService
{
    class UniqueItemException : Exception
    {
        public UniqueItemException(string message) 
            : base(message)
        { }

        public UniqueItemException()
            : base(Resources.ValueAlreadyExists)
        { }
    }
}
