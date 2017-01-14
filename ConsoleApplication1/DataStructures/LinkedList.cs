﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ConsoleApplication1.DataStructures
{
    public class LinkedList<T> : IEnumerable
    {
        private Node<T> _head = null;

        public Node<T> Add(T value)
        {
            var node = new Node<T> {Value = value};

            if (_head == null)
            {
                _head = node;
            }
            else
            {
                var current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = node;
            }

            return node;
        }

        public void Remove(Node<T> node)
        {
            if (_head == null)
                return;

            if (_head == node)
            {
                _head = _head.Next;
                node.Next = null;
                return;
            }

            var current = _head;
            while (current.Next != null)
            {
                if (current.Next == node)
                {
                    current.Next = node.Next;
                    return;
                }

                current = current.Next;
            }

        }

        public void Reverse()
        {
            Node<T> prev = null;
            var current = _head;

            if (current == null)
                return;

            while (current != null)
            {
                var next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }

            _head = prev;
        }

        public void ReverseRecurisve()
        {
            reverseRecurive(_head, null);
        }

        private void reverseRecurive(Node<T> current, Node<T> prev)
        {
            if (current.Next == null)
            {
                _head = current;
                _head.Next = prev;
                return;
            }

            var next = current.Next;
            current.Next = prev;
            reverseRecurive(next, current);
        }

        public IEnumerator<T> Enumerator()
        {
            var current = _head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return Enumerator();
        }
    }

    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }
    }
}
