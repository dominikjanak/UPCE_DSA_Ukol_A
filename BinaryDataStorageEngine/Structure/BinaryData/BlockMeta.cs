using System;

namespace BinaryDataStorageEngine
{
    public partial class BinaryStorage<T>
        where T : IValue
    {
        // Nested Class
        /// <summary>
        /// Block data header
        /// </summary>
        [Serializable]
        internal class BlockMeta
        {
            public int Min;
            public int Max;
        }
    }
}
