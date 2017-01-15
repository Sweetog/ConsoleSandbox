using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.DataStructures
{
    public class HashTable<T>
    {
        private int _tableSize = 25;
        private HashTableItem<T>[] _hashTable;
        public int Count { get; set; }
        private IComparer<T> _comparer;

        public HashTable()
        {
            _hashTable = new HashTableItem<T>[_tableSize];
            _comparer = Comparer<T>.Default;
        }

        public bool ContainsValue(T value)
        {
            if (_hashTable == null)
                return false;

            foreach (var item in _hashTable)
            {
                if(item == null)
                    continue;
                
                if (_comparer.Compare(item.Value, value) == 0)
                {
                    return true;
                }

                var current = item.Next;
                while (current != null)
                {
                    if (_comparer.Compare(item.Value, value) == 0)
                        return true;

                    current = current.Next;
                }
            }

            return false;
        }

        public bool ContainsKey(string key)
        {
            var index = Hash(key);

            if (_hashTable == null)
                return false;

            if (_hashTable[index] == null)
                return false;

            var item = _hashTable[index];

            if (item.Key == key)
                return true;


            var current = item.Next;
            while (current != null)
            {
                if (item.Key == key)
                    return true;

                current = current.Next;
            }

            return false;
        }

        public T GetValue(string key)
        {
            var index = Hash(key);

            if (_hashTable == null)
                return default(T);

            if (_hashTable[index] == null)
                return default(T);

            return _hashTable[index].Value;
        }

        public void Add(string key, T value)
        {
            var index = Hash(key);
            var newItem = new HashTableItem<T>(key, value);

            if (_hashTable[index] == null)
            {
                _hashTable[index] = newItem;
            }
            else
            {
                var current = _hashTable[index];
                while (current.Next != null && current.Key != key)
                {
                    current = current.Next;
                }

                if (current.Key == key)
                {
                    Console.WriteLine("Add(): Key already exists, new value overwrites old value");
                    current.Value = value;
                    return;
                }

                current.Next = newItem;
            }

            Count++;
        }

        public void Print()
        {
            for (var i = 0; i < _tableSize; i++)
            {
                var temp = _hashTable[i];
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Bucket Index: {0}", i);
                if (temp == null)
                {
                    Console.WriteLine("Is Empty");
                }
                else
                {
                    Console.WriteLine("Key: {0}", temp.Key);
                    Console.WriteLine("Value: {0}", temp.Value);
                }
                Console.WriteLine("Bucket Count: {0}: ", NumberOfItemsInBucket(temp));
                Console.WriteLine("-----------------------------");
            }
        }

        private int NumberOfItemsInBucket(HashTableItem<T> item)
        {
            int count = 0;

            while (item != null)
            {
                count++;
                item = item.Next;
            }

            return count;
        }

        public bool Remove(string key)
        {
            var index = Hash(key);
            var item = _hashTable[index];

            //Case 0. key does not exist in hashtable
            if (item == null)
            {
                Console.WriteLine("Remove(): Key does not exist");
                return false;
            }

            //Case 1. key is first item in the bucket and no other items in bucket
            if (item.Key == key && item.Next == null)
            {
                _hashTable[index] = null;
                Console.WriteLine("Remove(): Item exists and only item in the bucket");
                Count--;
                return true;
            }

            //Case 2. key is first item in the bucket and there are other items in the bucket
            if (item.Key == key)
            {
                var next = item.Next;
                item.Next = null;

                _hashTable[index] = next;
                Count--;
                return true;
            }

            //Case 3. key is not first item in the bucket

            var current = item.Next;
            var previous = item;

            while (current.Next != null && current.Key != key)
            {
                current = current.Next;
                previous = current;
            }

            if (current != null)
            {
                //item to delete is current
                previous.Next = current.Next;
                Count--;
                return true;
            }

            Console.WriteLine("Remove(): Key exists but item is not in bucket");

            return false;
        }

        private int Hash(string key)
        {
            var hash = 0;

            if (string.IsNullOrEmpty(key))
                return hash;

            var chars = key.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                hash += (int)chars[i];
            }

            return hash % _tableSize;
        }

    }
}
