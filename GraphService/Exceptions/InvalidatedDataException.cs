using System;

namespace ForestGraph
{
    class InvalidatedDataException : Exception
    {
        public InvalidatedDataException(string message) 
            : base(message)
        { }

        public InvalidatedDataException()
            : base("Data již nejsou platná a je potřeba je přegenerovat!")
        { }
    }
}
