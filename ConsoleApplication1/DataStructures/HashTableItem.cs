using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;

namespace ConsoleApplication1.DataStructures
{
    public class HashTableItem<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
        public HashTableItem<T> Next { get; set; }

        public HashTableItem(string key, T value)
        {
            Key = key;
            Value = value;
        }
    }
}
