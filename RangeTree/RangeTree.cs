using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTree
{
    public class RangeTree<TData>
        where TData : IData
    {
        private Node<TData> _root;

        public RangeTree()
        {
            _root = null;
        }

        public bool Build(List<TData> data)
        {
            var ordered = data.OrderBy(k => k.GetFrom());

            foreach(var d in ordered)
            {
                Console.WriteLine(d.GetFrom());
            }

            return true;
        }

    }
}
