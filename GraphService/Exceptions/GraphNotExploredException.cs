using System;

namespace ForestGraph
{
    class GraphNotExploredException : Exception
    {
        public GraphNotExploredException(string message) 
            : base(message)
        { }

        public GraphNotExploredException()
            : base("Nelze získat cestu, neboť graf není prozkoumán!")
        { }
    }
}
