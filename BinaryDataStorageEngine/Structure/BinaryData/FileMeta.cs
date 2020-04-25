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
            bool _initValCount;
            bool _initMax;
            bool _initMin;

            int _actualValuesCount;
            int _actualMin;
            int _actualMax;

            public FileMeta()
            {
                _initValCount = _initMax = _initMin = false;
                _actualValuesCount = _actualMin = _actualMax = 0;
            }

            public int NumberOfBlocks { get; set; }
            public int BlockSize { get; set; }

            public int ActualValuesCount 
            {
                get => _actualValuesCount; 
                set 
                {
                    _actualValuesCount = value;
                    if (!_initValCount)
                    {
                        _initValCount = true;
                        BuildValuesCount = value;
                    }
                } 
            }

            public int ActualMin
            {
                get => _actualMin;
                set
                {
                    _actualMin = value;
                    if (!_initMin)
                    {
                        _initMin = true;
                        BuildMin = value;
                    }
                }
            }

            public int ActualMax
            {
                get => _actualMax;
                set
                {
                    _actualMax = value;
                    if (!_initMax)
                    {
                        _initMax = true;
                        BuildMax = value;
                    }
                }
            }


            public int BuildValuesCount { get; private set; }
            public int BuildMin { get; private set; }
            public int BuildMax { get; private set; }

        }
    }
}
