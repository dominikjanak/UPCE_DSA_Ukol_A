using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugConsole
{
    class RangeTreeData : RangeTree.IData
    {
        private string _key;
        private float _from;
        private float _to;

        public RangeTreeData(string key, float from, float to)
        {
            _key = key;
            _from = from;
            _to = to;
        }

        public string Key { get => _key; } 

        public float GetFrom()
        {
            return _from;
        }

        public float GetTo()
        {
            return _to;
        }
    }
}
