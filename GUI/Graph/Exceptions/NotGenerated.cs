using GUI.Properties;
using System;

namespace GUI.Graph
{
    public class NotGenerated : Exception
    {
        public NotGenerated(string message) 
            : base(message)
        { }

        public NotGenerated()
            : base(Resources.NotGenerated)
        { }
    }
}
