using GraphService.Properties;
using System;

namespace GraphService
{
    class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) 
            : base(message)
        { }

        public ItemNotFoundException()
            : base(Resources.ValueNotExists)
        { }
    }
}
