using System;

namespace BinaryDataStorageEngine
{
    public partial class BinaryStorage<T>
        where T : IValue
    {
        // Nested Class
        /// <summary>
        /// Data item
        /// </summary>
        [Serializable]
        internal class DataItem
        {
            private T _dataObject;
            public int HashKey { get; private set; }
            public T Data => _dataObject;

            public DataItem(T data) 
            {
                HashKey = BinaryStorage<T>.GetHash(data.Key);
                _dataObject = data;
            }
        }
    }
}
