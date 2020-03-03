using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTree
{
    public class Leaf<TData>
        where TData : IData
    {
        private TData _data;
        private Leaf<TData> _prev;
        private Leaf<TData> _next;

        public Leaf<TData> Prev 
        { 
            get => _prev; 
            set => _prev = value; 
        }

        public Leaf<TData> Next 
        { 
            get => _next; 
            set => _next = value; 
        }

        public TData Data 
        { 
            get => _data; 
            set => _data = value; 
        }

        public Leaf(TData data)
           : this(data, null, null)
        {
        }

        public Leaf(TData data, Leaf<TData> prev)
            : this(data, prev, null)
        {
        }

        public Leaf(TData data, Leaf<TData> prev, Leaf<TData> next)
        {
            _data = data;
            _prev = prev;
            _next = next;
        }
    }
}
