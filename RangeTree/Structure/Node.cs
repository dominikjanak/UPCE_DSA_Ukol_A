using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTree
{
    public class Node<TData>
        where TData : IData
    {
        private Node<TData> _left;
        private Node<TData> _right; 
        private TData _leaf;
        //next TREE

        public Node()
        {
            _left = null;
            _right = null;
            _leaf = default;
        }

    }
}
