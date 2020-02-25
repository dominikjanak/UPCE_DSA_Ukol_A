using System;

namespace ForestGraph
{
    class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) 
            : base(message)
        { }

        public ItemNotFoundException()
            : base("Zadaná položka v grafu neexistuje!")
        { }
    }
}
