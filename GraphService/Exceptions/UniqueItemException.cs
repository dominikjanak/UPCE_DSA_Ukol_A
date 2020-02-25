using System;

namespace ForestGraph
{
    class UniqueItemException : Exception
    {
        public UniqueItemException(string message) 
            : base(message)
        { }

        public UniqueItemException()
            : base("Zadaná položka v grafu již existuje!")
        { }
    }
}
