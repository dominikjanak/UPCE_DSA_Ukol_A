using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryDataStorageEngine.Tests
{
    [TestClass()]
    public class BinaryStorageTests
    {
        private static string _testFile = "./test.bin";

        private static List<DataItem> Random(int rand)
        {
            List<DataItem> items = new List<DataItem>();

            for (int i = 1; i <= rand; i++)
            {
                items.Add(new DataItem("K_" + i, i));
            }

            return items;
        }
        [TestInitialize()]
        public void Startup()
        {
            Clean();
        }

        [ClassCleanup()]
        public static void CleanUp()
        {
            Clean();
        }

        private static void Clean()
        {
            if (File.Exists(_testFile))
            {
                File.Delete(_testFile);
            }
        }

        // Build and Read tests

        [TestMethod()]
        public void BuildAndRead_ZeroItem_Test()
        {
            List<DataItem> data = Random(0);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile);

            Assert.ThrowsException<NoDataException>(() => bw.WriteBinaryFile(data));
        }

        [TestMethod()]
        public void BuildAndRead_OneItem_Test()
        {
            List<DataItem> data = Random(1);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);

            bw.WriteBinaryFile(data);

            List<DataItem> readed = bw.ReadBinaryFile();

            ListContainSameValues(data, readed);
        }

        [TestMethod()]
        public void BuildAndRead_OneHundredItem_Test()
        {
            List<DataItem> data = Random(100);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);

            bw.WriteBinaryFile(data);

            List<DataItem> readed = bw.ReadBinaryFile();

            ListContainSameValues(data, readed);
            Assert.AreEqual(data.Count, readed.Count);
        }

        [TestMethod()]
        public void BuildAndRead_OneHundredOneItem_Test()
        {
            List<DataItem> data = Random(101);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);

            bw.WriteBinaryFile(data);

            List<DataItem> readed = bw.ReadBinaryFile();

            ListContainSameValues(data, readed);
            Assert.AreEqual(data.Count, readed.Count);
        }

        [TestMethod()]
        public void BuildAndRead_OneThousandItem_Test()
        {
            List<DataItem> data = Random(1000);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);

            bw.WriteBinaryFile(data);

            List<DataItem> readed = bw.ReadBinaryFile();

            ListContainSameValues(data, readed);
            Assert.AreEqual(data.Count, readed.Count);
        }

        // Search tests

        [TestMethod()]
        public void Find_Binary_1000Items_Test()
        {
            List<DataItem> data = Random(1000);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);

            bw.WriteBinaryFile(data);

            DataItem find;
            foreach (DataItem expect in data)
            {
                find = bw.Find(expect.Key, SearchMethod.Binary);
                Assert.IsNotNull(find);
            }

        }

        [TestMethod()]
        public void Find_Interpolation_1000Items_Test()
        {
            List<DataItem> data = Random(1000);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);

            bw.WriteBinaryFile(data);

            DataItem find;
            foreach (DataItem expect in data)
            {
                find = bw.Find(expect.Key, SearchMethod.Interpolation);
                Assert.IsNotNull(find);
            }

        }

        [TestMethod()]
        public void Find_Binary_101Items_Test()
        {
            List<DataItem> data = Random(101);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);

            bw.WriteBinaryFile(data);

            DataItem find;
            foreach (DataItem expect in data)
            {
                find = bw.Find(expect.Key, SearchMethod.Binary);
                Assert.IsNotNull(find);
            }

        }

        [TestMethod()]
        public void Find_Interpolation_101Items_Test()
        {
            List<DataItem> data = Random(101);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);

            bw.WriteBinaryFile(data);

            DataItem find;
            foreach (DataItem expect in data)
            {
                find = bw.Find(expect.Key, SearchMethod.Interpolation);
                Assert.IsNotNull(find);
            }

        }

        [TestMethod()]
        public void Find_Interpolation_1500Items_1500NotFound_Test()
        {
            List<DataItem> data = Random(1500);
            List<DataItem> search = Random(3000);

            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);

            bw.WriteBinaryFile(data);

            DataItem find;
            for(int i = 1; i <= 3000; i++)
            {
                find = bw.Find(search[i-1].Key, SearchMethod.Interpolation);
                if(i > 1500)
                {
                    Assert.IsNull(find);
                }
                else
                {
                    Assert.IsNotNull(find);
                }
            }
        }

        [TestMethod()]
        public void Find_Binary_1500Items_1500NotFound_Test()
        {
            List<DataItem> data = Random(1500);
            List<DataItem> search = Random(3000);

            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);

            bw.WriteBinaryFile(data);

            DataItem find;
            for(int i = 1; i <= 3000; i++)
            {
                find = bw.Find(search[i-1].Key, SearchMethod.Binary);
                if(i > 1500)
                {
                    Assert.IsNull(find);
                }
                else
                {
                    Assert.IsNotNull(find);
                }
            }
        }

        [TestMethod()]
        public void Find_Binary_1Items_Test()
        {
            List<DataItem> data = Random(1);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);

            bw.WriteBinaryFile(data);

            DataItem find;
            foreach (DataItem expect in data)
            {
                find = bw.Find(expect.Key, SearchMethod.Binary);
                Assert.IsNotNull(find);
            }
        }

        [TestMethod()]
        public void Find_Interpolation_1Items_Test()
        {
            List<DataItem> data = Random(1);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);

            bw.WriteBinaryFile(data);

            DataItem find;
            foreach (DataItem expect in data)
            {
                find = bw.Find(expect.Key, SearchMethod.Interpolation);
                Assert.IsNotNull(find);
            }

        }

        // Remove test
        
        [TestMethod()]
        public void Remove_Interpolation_Test()
        {
            List<DataItem> data = Random(1000);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);
            bw.WriteBinaryFile(data);

            bool state;
            foreach(var d in data)
            {
                state = bw.RemoveItem(d.Key, SearchMethod.Interpolation);
                Assert.IsTrue(state);
            }
        }
        
        [TestMethod()]
        public void Remove_Binary_Test()
        {
            List<DataItem> data = Random(1000);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);
            bw.WriteBinaryFile(data);

            bool state;
            foreach(var d in data)
            {
                state = bw.RemoveItem(d.Key, SearchMethod.Binary);
                Assert.IsTrue(state);
            }
        }

        [TestMethod()]
        public void Remove_Interpolation_More_Test()
        {
            List<DataItem> data = Random(3000);
            List<DataItem> delete = Random(4000);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);
            bw.WriteBinaryFile(data);

            bool state;
            for (int i = 1; i <= 4000; i++)
            {
                state = bw.RemoveItem(delete[i-1].Key, SearchMethod.Interpolation);
                if (i > 3000)
                {
                    Assert.IsFalse(state);
                }
                else
                {
                    Assert.IsTrue(state);
                }
            }
        }

        [TestMethod()]
        public void Remove_Binary_More_Test()
        {
            List<DataItem> data = Random(3000);
            List<DataItem> delete = Random(4000);
            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);
            bw.WriteBinaryFile(data);

            bool state;
            for (int i = 1; i <= 4000; i++)
            {
                state = bw.RemoveItem(delete[i-1].Key, SearchMethod.Binary);
                if (i > 3000)
                {
                    Assert.IsFalse(state);
                }
                else
                {
                    Assert.IsTrue(state);
                }
            }
        }

        // WARN: It can take a few minutes!
        [TestMethod()]
        public void Full_Binary_Test()
        {
            int valid = 500;
            int full = 700;
            List <DataItem> data = Random(valid);
            List<DataItem> test = Random(full);
            var rand = new Random((int)DateTime.UtcNow.Ticks);

            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);
            bw.WriteBinaryFile(data);

            bool state;
            for (int i = 1; i <= full; i++)
            {
                int idx = rand.Next(0, test.Count - 1);
                DataItem it = test[idx];

                test.RemoveAt(idx);

                DataItem fi = bw.Find(it.Key);
                bool del = bw.RemoveItem(it.Key, SearchMethod.Binary);

                if(it.KeyVal > valid)
                {
                    Assert.IsNull(fi);
                    Assert.IsFalse(del);
                }
                else
                {
                    Assert.IsNotNull(fi);
                    Assert.IsTrue(del);
                    data = data.Where(k => k.KeyVal != it.KeyVal).ToList();
                }

                List<DataItem> loaded = bw.ReadBinaryFile();

                Assert.IsTrue(ListContainSameValues(data, loaded));
            }
        }

        // WARN: It can take a few minutes!
        [TestMethod()]
        public void Full_Interpolation_Test()
        {
            int valid = 500;
            int full = 700;
            List <DataItem> data = Random(valid);
            List<DataItem> test = Random(full);
            var rand = new Random((int)DateTime.UtcNow.Ticks);

            BinaryStorage<DataItem> bw = new BinaryStorage<DataItem>(_testFile, 100);
            bw.WriteBinaryFile(data);

            bool state;
            for (int i = 1; i <= full; i++)
            {
                int idx = rand.Next(0, test.Count - 1);
                DataItem it = test[idx];

                test.RemoveAt(idx);

                DataItem fi = bw.Find(it.Key);
                bool del = bw.RemoveItem(it.Key, SearchMethod.Interpolation);

                if(it.KeyVal > valid)
                {
                    Assert.IsNull(fi);
                    Assert.IsFalse(del);
                }
                else
                {
                    Assert.IsNotNull(fi);
                    Assert.IsTrue(del);
                    data = data.Where(k => k.KeyVal != it.KeyVal).ToList();
                }

                List<DataItem> loaded = bw.ReadBinaryFile();

                Assert.IsTrue(ListContainSameValues(data, loaded));
            }
        }




        /////////////////////////////////////////////////////////////////
        private bool ListContainSameValues(List<DataItem> expected, List<DataItem> actual)
        {
            if (expected.Count != actual.Count)
            {
                return false;
            }
            bool cont = false;

            foreach (var e in expected)
            {

                foreach (var a in actual)
                {

                    if (e.Key == a.Key)
                    {
                        cont = true;
                        break;
                    }
                }

                if (cont)
                {
                    cont = false;
                    continue;
                }
                return false;
            }

            return true;
        }
    }
}