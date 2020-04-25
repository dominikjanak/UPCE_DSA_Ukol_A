using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinaryDataStorageEngine
{
    public partial class BinaryStorage<T>
        where T : IValue
    {
        private int _itemsForBlock;
        private static byte _serializationVersion;
        private string _filePath;
        private FileStream _file; // filestream for close if open

        public BinaryStorage(string path, int itemsForBlock = 100)
        {
            _filePath = path;
            _itemsForBlock = itemsForBlock;
            _serializationVersion = 1;
            _file = null;
        }

        /// <summary>
        /// Get File stream
        /// </summary>
        /// <param name="mode">File mode open</param>
        /// <returns>Opened filestream</returns>
        private FileStream GetFileStream(FileMode mode)
        {
            FileStream file;
            try
            {
                file = File.Open(_filePath, mode);
            }
            catch
            {
                // Close file stream if open
                _file.Close();
                file = File.Open(_filePath, mode);
            }
            _file = file;
            return file;
        }

        /// <summary>
        /// Write data to binary file
        /// </summary>
        /// <param name="input">Data</param>
        public void WriteBinaryFile(List<T> input)
        {
            if (String.IsNullOrWhiteSpace(_filePath))
            {
                throw new ArgumentNullException("Path to file is not valid!");
            }

            if(input == null)
            {
                throw new ArgumentNullException("Data for serialize not Added");
            }

            FileStream file = GetFileStream(FileMode.Create);

            FileMeta init = new FileMeta();
            init.NumberOfBlocks = (int)Math.Ceiling(((double)input.Count / _itemsForBlock)); // Calculate number of blocks
            init.BlockSize = 10;
            init.ValuesCount = input.Count;

            List<byte[]> blocksData = new List<byte[]>();
            List<DataItem> data = new List<DataItem>();

            // Prepare data with hashest
            foreach(T item in input)
            {
                data.Add(new DataItem(item)); // Prepare hash items with value
            } 

            data = data.OrderBy(k => k.HashKey).ToList(); // Order values by HASH

            init.Min = data[0].HashKey;
            init.Max = data[data.Count - 1].HashKey;

            for (int i = 0; i < init.NumberOfBlocks; i++)
            {
                // Take values for store into block
                IEnumerable<DataItem> part = data.Skip(i * _itemsForBlock).Take(_itemsForBlock);

                // Build data block
                byte[] blockBytes = BuildBlock(part);
                blocksData.Add(blockBytes); // Add builded block to array

                if(init.BlockSize < blockBytes.Length)
                {
                    init.BlockSize = blockBytes.Length; // Set max data block size
                }
            }

            // Write Header into bin file
            int offset = WriteMetaHeader(file, init);

            // Write each block into bin file
            foreach(var block in blocksData)
            {
                offset += WriteBlock(file, block, offset, init.BlockSize);
            }

            file.Close();
        }

        /// <summary>
        /// Deserialize all binary file
        /// </summary>
        /// <returns>All data object</returns>
        public List<T> ReadBinaryFile()
        {
            if (String.IsNullOrWhiteSpace(_filePath))
            {
                throw new ArgumentNullException("Path to file is not valid!");
            }

            FileStream file = GetFileStream(FileMode.Open);

            FileMeta metadata;
            int headerLength;

            {
                (int offset, FileMeta data) header = ReadMetaHeader(file);
                metadata = header.data;
                headerLength = header.offset;
            }

            List<T> output = new List<T>(); // Prepare output array

            // Read each block
            for(int i = 0; i < metadata.NumberOfBlocks; i++)
            {
                // Read block
                (BlockMeta meta, DataItem[] data) = ReadBlock(file, i, headerLength, metadata.BlockSize);
                // Prepare data for output
                output.AddRange(data.Select(k => k.Data));
            }

            file.Close();
            return output;
        }

        /// <summary>
        /// Find item in binary data
        /// </summary>
        /// <param name="key">Searching key</param>
        /// <param name="method">Searching method</param>
        /// <returns>Found key or null</returns>
        public T Find(string key, SearchMethod method = SearchMethod.Binary)
        {
            // Get searched hash
            int keyHash = GetHash(key);
            FileStream file = GetFileStream(FileMode.Open);

            FileMeta metadata;
            int headerLength;

            {
                // Read file meta block
                (int offset, FileMeta data) header = ReadMetaHeader(file);
                metadata = header.data;
                headerLength = header.offset;
            }

            // Find block data with valid range
            DataItem[] values = FindBlockData(file, metadata, keyHash, headerLength, method);
            T value = default;

            if (values != null && values.Length > 0)
            {
                // Find value in block data
                value = SearchInBlock(values, keyHash, key, method);
            }

            file.Close();
            return value;
        }

        /// <summary>
        /// Get positive and unique hash 
        /// </summary>
        /// <param name="input">String for hash</param>
        /// <returns>unique hash</returns>
        private static int GetHash(string input)
        {
            int hash = (input.GetHashCode() & 0x7fffffff); // Get hash and keep positive sign
            if (hash < 0) throw new ApplicationException("Calculated negative hash!");
            return hash;
        }

        /// <summary>
        /// Get version of serialization
        /// </summary>
        /// <returns>Serialization version bytes</returns>
        private static byte[] GetSirializationVersionBytes()
        {
            return new byte[1] { _serializationVersion };
        }

        /// <summary>
        /// Search item in block data
        /// </summary>
        /// <param name="data">Data array used for search</param>
        /// <param name="hash">Searching hash</param>
        /// <param name="key">Searching key</param>
        /// <param name="method">Seraching method</param>
        /// <returns>Find value or null</returns>
        private static T SearchInBlock(DataItem[] data, int hash, string key, SearchMethod method)
        {
            int left = 0;
            int right = data.Length - 1;
            int mid;

            while (left <= right && hash >= data[left].HashKey && hash <= data[right].HashKey)
            {
                if (left == right)
                {
                    if (data[left].Data.Key == key)
                    {
                        return data[left].Data;
                    }
                    return default(T);
                }

                mid = CalculateMiddle(left, right, data[left].HashKey, data[right].HashKey, hash, method);

                // Target found 
                if (data[mid].Data.Key == key)
                {
                    return data[mid].Data;
                }

                if (data[mid].HashKey < hash)
                {
                    // Is in upper part 
                    left = mid + 1;
                }
                else
                {
                    // Is in the lower part 
                    right = mid - 1;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Find block with valid range for finding value
        /// </summary>
        /// <param name="stream">File stream used for read</param>
        /// <param name="metadata">File metadata</param>
        /// <param name="keyHash">Searching hash value</param>
        /// <param name="headerLength">File header size (offset for first block)</param>
        /// <param name="method">Searching method</param>
        /// <returns>Data array of appropriate block</returns>
        private DataItem[] FindBlockData(FileStream stream, FileMeta metadata, int keyHash, int headerLength, SearchMethod method)
        {
            int left = 0;
            int right = (metadata.ValuesCount - 1);
            int mid;
            int maxVal = metadata.Max;
            int minVal = metadata.Min;

            // While until search interval is valid
            while (left <= right && keyHash >= minVal && keyHash <= maxVal)
            {
                // Calculate middle index
                mid = CalculateMiddle(left, right, minVal, maxVal, keyHash, method);
                // Get blockID with item with middle index
                int blockId = mid / _itemsForBlock;

                if (mid < 0 && mid > metadata.ValuesCount)
                {
                    throw new ApplicationException("Invalid operation! Out of range!");
                }

                // Read block header
                BlockMeta block = ReadBlockHeader(stream, blockId, headerLength, metadata.BlockSize);

                if (block.Min <= keyHash && keyHash <= block.Max)
                {
                    // Read block data for search value (value is in this range)
                    DataItem[] data = ReadBlock(stream, blockId, headerLength, metadata.BlockSize).blockdata;
                    return data;
                }

                if (block.Max < keyHash)
                {
                    // Is in the upper part 
                    left = mid + 1;
                }
                else
                {
                    // Is in the lower part 
                    right = mid - 1;
                }
            }

            // Valid block not found
            return null;
        }

        /// <summary>
        /// Convert object to data array
        /// </summary>
        /// <typeparam name="E">Object type</typeparam>
        /// <param name="obj">Object for convert to bytes</param>
        /// <returns>Bytes array</returns>
        private static byte[] ObjectToBytes<E>(E obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Calculate middle index
        /// </summary>
        /// <param name="left">Min index</param>
        /// <param name="right">Max index</param>
        /// <param name="min">Min value</param>
        /// <param name="max">Max value</param>
        /// <param name="value">Searched value</param>
        /// <param name="method">Search method</param>
        /// <returns>Middle index</returns>
        private static int CalculateMiddle(int left, int right, int min, int max, int value, SearchMethod method)
        {
            if (method == SearchMethod.Binary)
            {
                return left + (right - left) / 2;
            }
            else
            {
                return (int)Math.Round(left + ((value - min) * ((double)right - left) / (max - min)));
            }
        }

        /// <summary>
        /// Read block data and parse
        /// </summary>
        /// <param name="stream">File stream used for read</param>
        /// <param name="blockIndex">Index of block in sequence</param>
        /// <param name="headerLength">Number of bytes of header</param>
        /// <param name="blockSize">One block size</param>
        /// <returns>(Block MetaHeader, DataItem in array)</returns>
        private static (BlockMeta blockMeta, DataItem[] blockdata) ReadBlock(FileStream stream, int blockIndex, int headerLength, int blockSize)
        {
            // Init array for full block bytes
            byte[] blockBytes = new byte[blockSize];

            // Read all bytes of specific block
            blockBytes = ReadBytes(stream, blockSize, headerLength + (blockSize * blockIndex));
            int offset = 0;

            // Read block metaHeader size
            int blockMetaSize = BitConverter.ToInt32(blockBytes, offset);
            offset += sizeof(int);

            // Read MetaHeader
            BlockMeta blockMeta = BytesToObject<BlockMeta>(blockBytes, offset, blockMetaSize);
            offset += blockMetaSize;

            // Read Data Size
            int blockDataSize = BitConverter.ToInt32(blockBytes, offset);
            offset += sizeof(int);

            // Read Data
            DataItem[] items = BytesToObject<DataItem[]>(blockBytes, offset, blockDataSize);

            return (blockMeta, items);
        }

        /// <summary>
        /// Read block header
        /// </summary>
        /// <param name="stream">File stream used for read</param>
        /// <param name="blockIndex">Index of block in sequence</param>
        /// <param name="headerLength">Number of bytes of header</param>
        /// <param name="blockSize">One block size</param>
        /// <returns>(Block MetaHeader, DataItem in array)</returns>
        private static BlockMeta ReadBlockHeader(FileStream stream, int blockIndex, int headerLength, int blockSize)
        {
            // Read all bytes of specific block
            byte[] blockHeaderSizeBytes = ReadBytes(stream, sizeof(int), headerLength + (blockSize * blockIndex));

            // Read block metaHeader size
            int blockMetaSize = BitConverter.ToInt32(blockHeaderSizeBytes, 0);

            // Read block header bytes
            byte[] blockHeaderBytes = ReadBytes(stream, blockMetaSize, headerLength + (blockSize * blockIndex) + sizeof(int));

            // Deserialize MetaHeader
            BlockMeta blockMeta = BytesToObject<BlockMeta>(blockHeaderBytes);

            return blockMeta;
        }

        /// <summary>
        /// Convert bytes array to object
        /// </summary>
        /// <typeparam name="E">Object type</typeparam>
        /// <param name="input">Bytes array for convert to object</param>
        /// <param name="offset">Offset in array</param>
        /// <param name="cnt">Number of bytes used for convert</param>
        /// <returns>Deserialized object</returns>
        private static E BytesToObject<E>(byte[] input, int offset = 0, int cnt = -1)
        {
            if (input == null)
                return default(E);

            // Object bytes
            byte[] data;
            if (offset > 0 || cnt != 0)
            {
                if (cnt < 0) 
                { 
                    cnt = input.Length - offset;
                }

                data = input.Skip(offset).Take(cnt).ToArray(); // Cut out object data for serialize
            }
            else
            {
                data = input;
            }

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (E)obj;
            }
        }

        /// <summary>
        /// Read bytes from stream
        /// </summary>
        /// <param name="stream">File stream used for read</param>
        /// <param name="count">Number of bytes to read</param>
        /// <param name="offset">Read offset</param>
        /// <returns>Readed bytes</returns>
        private static byte[] ReadBytes(Stream stream, int count, int offset)
        {
            byte[] data = new byte[count];
            stream.Seek(offset, SeekOrigin.Begin);
            stream.Read(data, 0, (int)count);
            return data;
        }

        /// <summary>
        /// Write bytes to stream
        /// </summary>
        /// <param name="stream">File stream used for write</param>
        /// <param name="data">Data for write to stream</param>
        /// <param name="offset">Write offset</param>
        private static void WriteBytes(Stream stream, byte[] data, int offset)
        {
            stream.Seek(offset, SeekOrigin.Begin);
            stream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Read MetaData from file
        /// </summary>
        /// <param name="stream">File stream used for read</param>
        /// <returns>Number of readed bytes and InitMetaBlock</returns>
        private static (int readed, FileMeta data) ReadMetaHeader(FileStream stream)
        {
            int offset = 0;

            // Read serialization version from file
            byte versionBytes = ReadBytes(stream, sizeof(byte), offset)[0];
            offset += sizeof(byte);

            // Compare serialization version
            if (versionBytes != _serializationVersion)
            {
                throw new IncompatibleSerializationVersionException("Incompatible serialization version!");
            }

            // Read MetaHeader size
            byte[] metaHeadersLenghtBytes = ReadBytes(stream, sizeof(int), offset);
            offset += metaHeadersLenghtBytes.Length;
            int metaHeaderLength = BitConverter.ToInt32(metaHeadersLenghtBytes, 0);

            // Read MetaHeader Data
            byte[] metaHeadersBytes = ReadBytes(stream, metaHeaderLength, offset);
            offset += metaHeadersBytes.Length;
            FileMeta metaHeader = BytesToObject<FileMeta>(metaHeadersBytes);

            return (offset, metaHeader);
        }

        /// <summary>
        /// Write Meta Header into binary file
        /// </summary>
        /// <param name="stream">File stream used for write</param>
        /// <param name="meta">Meta block</param>
        /// <returns>Number of written bytes</returns>
        private static int WriteMetaHeader(FileStream stream, FileMeta meta)
        {
            int offset = 0;
            // Get binary data
            byte[] metaHeadersBytes = ObjectToBytes<FileMeta>(meta);
            byte[] metaHeadersLenghtBytes = BitConverter.GetBytes(metaHeadersBytes.Length);
            byte[] versionBytes = GetSirializationVersionBytes();

            // Write version
            WriteBytes(stream, versionBytes, offset);
            offset += versionBytes.Length;

            // Write metaheader size
            WriteBytes(stream, metaHeadersLenghtBytes, offset);
            offset += metaHeadersLenghtBytes.Length;

            // Write metaheader
            WriteBytes(stream, metaHeadersBytes, offset);
            offset += metaHeadersBytes.Length;

            return offset;
        }

        /// <summary>
        /// Build binary data block
        /// </summary>
        /// <param name="data">Data to serialize for block</param>
        /// <returns>Block bytes</returns>
        private static byte[] BuildBlock(IEnumerable<DataItem> data)
        {
            int offset = 0;

            // Prepare block meta data
            BlockMeta meta = new BlockMeta();
            meta.Min = data.ElementAt(0).HashKey;
            meta.Max = data.ElementAt(data.Count() - 1).HashKey;

            // Serialize objects
            byte[] metaBytes = ObjectToBytes(meta);
            byte[] metaSize = BitConverter.GetBytes(metaBytes.Length);
            byte[] dataBytes = ObjectToBytes(data.ToArray());
            byte[] dataSize = BitConverter.GetBytes(dataBytes.Length);

            // Prepare output array
            byte[] output = new byte[metaBytes.Length + metaSize.Length + dataBytes.Length + dataSize.Length];

            // Copy MetaDataSize bytes to output
            Array.Copy(metaSize, output, metaSize.Length);
            offset += metaSize.Length;

            // Copy MetaData bytes to output
            Array.Copy(metaBytes, 0, output, offset, metaBytes.Length);
            offset += metaBytes.Length;

            // Copy DataSize bytes to output
            Array.Copy(dataSize, 0, output, offset, dataSize.Length);
            offset += dataSize.Length;

            // Copy Data bytes to output
            Array.Copy(dataBytes, 0, output, offset, dataBytes.Length);
            offset += dataBytes.Length;

            return output;
        }

        /// <summary>
        /// Write block to file (keep max size for all blocks)
        /// </summary>
        /// <param name="stream">File stream used for write</param>
        /// <param name="block">Bytes for write into stream</param>
        /// <param name="offset">Offset in stream</param>
        /// <param name="blockSize">Block size</param>
        /// <returns>Number of written bytes</returns>
        private static int WriteBlock(FileStream stream, byte[] block, int offset, int blockSize)
        {
            byte[] write = new byte[blockSize];
            int i = 0;

            for (; i < block.Length; i++)
            {
                write[i] = block[i];
            }

            // Fill empty space with zeros
            for (; i < blockSize; i++)
            {
                write[i] = 0x0;
            }

            WriteBytes(stream, write, offset);

            return i;
        }
    }
}
