using System;

namespace ForestGraph
{
    class UniqueKeyException : Exception
    {
        public UniqueKeyException(string message) 
            : base(message)
        { }

        public UniqueKeyException()
            : base("Zadaný klíč v grafu již existuje!")
        { }
    }
}
