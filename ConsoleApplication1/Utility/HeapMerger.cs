using System;
using System.Collections.Generic;
using ConsoleApplication1.DataStructures;

namespace ConsoleApplication1.Utility
{
    public class HeapMerger<T>
    {
        protected Comparison<T> _comparison;

        public HeapMerger()
        {
            _comparison = new Comparison<T>(Comparer<T>.Default.Compare);
        }

        public HeapMerger(Comparison<T> comparison)
        {
            _comparison = comparison;
            if (_comparison == null)
                _comparison = new Comparison<T>(Comparer<T>.Default.Compare);
        }

        public ICollection<T> Merge(ICollection<ICollection<T>> collections)
        {
            BinaryHeap<Frontier> heap = new BinaryHeap<Frontier>(collections.Count);
            int count = 0;
            foreach (ICollection<T> coll in collections)
            {
                count += coll.Count;
                Frontier frontier = new Frontier(coll, _comparison);
                if (frontier.MoveNext())
                {
                    heap.Insert(frontier);
                }
            }

            List<T> mergeList = new List<T>(count);

            while (heap.Size > 0)
            {
                Frontier frontier = heap.Pop();
                mergeList.Add(frontier.Value);
                if (frontier.MoveNext())
                {
                    heap.Insert(frontier);
                }
            }

            return mergeList;
        }

        public ICollection<T> Merge(ICollection<ICollection<T>> collections, bool removeDuplicates)
        {
            if (!removeDuplicates)
                return Merge(collections);

            BinaryHeap<Frontier> heap = new BinaryHeap<Frontier>(collections.Count);
            int count = 0;
            foreach (ICollection<T> coll in collections)
            {
                count += coll.Count;
                Frontier frontier = new Frontier(coll, _comparison);
                if (frontier.MoveNext())
                {
                    heap.Insert(frontier);
                }
            }

            List<T> mergeList = new List<T>(count);

            T lastValue = default(T);
            count = 0;

            while (heap.Size > 0)
            {
                Frontier frontier = heap.Pop();

                if (count > 0 && _comparison.Invoke(frontier.Value, lastValue) != 0)
                {
                    mergeList.Add(frontier.Value);
                    lastValue = frontier.Value;
                }
                if (frontier.MoveNext())
                {
                    heap.Insert(frontier);
                }

                count++;
            }

            return mergeList;
        }

        class Frontier : IComparable<Frontier>
        {
            protected IEnumerator<T> _enumerator;

            protected Comparison<T> _comparison;

            public T Value
            {
                get { return _enumerator.Current; }
            }

            public Frontier(ICollection<T> collection, Comparison<T> comparison)
            {
                _enumerator = collection.GetEnumerator();
                _comparison = comparison;
            }

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public int CompareTo(Frontier cursor)
            {
                return _comparison.Invoke(this.Value, cursor.Value);
            }
        }
    }
}