using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class EdgeSerialize
    {
        public EdgeSerialize() { }
        public EdgeSerialize(string id, string start, string target, float size)
        {
            ID = id;
            Start = start;
            Target = target;
            Size = size;
        }

        public string ID { get; set; }
        public string Start { get; set; }
        public string Target { get; set; }
        public float Size { get; set; }
    }
}
