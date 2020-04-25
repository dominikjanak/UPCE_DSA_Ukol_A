using BinaryDataStorageEngine;
using System;

namespace BinaryDataStorageEngine.Tests
{
    [Serializable]
    public class DataItem : IValue
    {
        private string _key;
        private int _val;
        public string Key => _key;
        public int KeyVal => _val;

        public DataItem(string key, int val = 0)
        {
            _key = key;
            _val = val;
        }
    }
}
