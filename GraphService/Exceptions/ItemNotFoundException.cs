using System;

namespace GraphService
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) 
            : base(message)
        { }
    }
}
