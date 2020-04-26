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
        internal struct FileMeta
        {
            public int ActualValuesCount;
            public int ActualMin;
            public int ActualMax;

            public int BuildValuesCount;
            public int BuildMin;
            public int BuildMax;

            public int NumberOfBlocks;
            public int BlockSize;
        }
    }
}
