using GraphService.Properties;
using System;

namespace ForestGraph
{
    class UniqueKeyException : Exception
    {
        public UniqueKeyException(string message) 
            : base(message)
        { }

        public UniqueKeyException()
            : base(Resources.KEY_IS_ALREADY_EXISTS)
        { }
    }
}
