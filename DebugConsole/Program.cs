using BinaryDataStorageEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace DebugConsole
{
    class Program
    {
        private static List<Item> Random(int rand)
        {
            List<Item> items = new List<Item>();

            for(int i = 1; i <= rand; i++)
            {
                items.Add(new Item("K_" + i));
            }

            return items;
        }

        static void Main(string[] args)
        {
            int cnt = 1;
            List<Item> data = Random(cnt);

            BinaryStorage<Item> bw = new BinaryStorage<Item>("./data.bin");

            bw.WriteBinaryFile(data);

            bw.Find("K_1");

            bool res = bw.RemoveItem("K_1");

            bw.Find("K_1");


            List<Item> output = bw.ReadBinaryFile();

            foreach(var i in output)
            {
                Console.WriteLine(i.Key);
            }

            /*for(int i = 1; i <= cnt; i++)
            {
                string search = "K_" + i;
                Item val = bw.Find(search, SearchMethod.Interpolation);
                if(val == null || val.Key != search)
                {
                    break;
                }
            }*/

            Console.ReadLine();
        }
    }

    [Serializable]
    public class Item : IValue
    {
        private string _key;
        public string Key => _key;

        public Item(string key) {
            _key = key;
        }
    }
}
