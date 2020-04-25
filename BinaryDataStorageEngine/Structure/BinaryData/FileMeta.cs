using System;

namespace BinaryDataStorageEngine
{
    public partial class BinaryStorage<T>
        where T : IValue
    {
        // Nested Class
        /// <summary>
        /// File data header
        /// </summary>
        [Serializable]
        internal class FileMeta
        {
            public int NumberOfBlocks { get; set; }
            public int BlockSize { get; set; }
            public int ValuesCount { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
        }
    }
}
