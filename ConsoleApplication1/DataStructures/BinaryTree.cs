using System;
using System.Collections.Generic;

namespace ConsoleApplication1.DataStructures
{
    public delegate void BinaryTreeVisitor<in T>(T nodeData);

    public partial class BinaryTree<T> where T : IComparable<T>
    {
        private BinaryTreeNode<T> _root = null;
        private IComparer<T> _comparer = null;

        public int Size { get; set; }

        public BinaryTreeNode<T> Root
        {
            get { return _root; }
        }

        public BinaryTree()
        {
            _comparer = Comparer<T>.Default;
        }

        public BinaryTree(T value, IComparer<T> comparer)
        {
            _root = new BinaryTreeNode<T>(value);
            _comparer = comparer ?? Comparer<T>.Default;
        }

        public T Add(T value)
        {
            var newNode = new BinaryTreeNode<T>(value);
            if (_root == null)
            {
                _root = newNode;
                Size++;
                return value;
            }

            var parent = FindParentForNewNodeInsert(value, _root);

            if (parent.LeftDescendant == null)
            {
                parent.LeftDescendant = newNode;
            }
            else
            {
                parent.RightDescendant = newNode;
            }

            Size++;

            return value;
        }

        private BinaryTreeNode<T> FindParentForNewNodeInsert(T value, BinaryTreeNode<T> current)
        {
            if (current.LeftDescendant == null)
                return current;

            if (current.RightDescendant == null)
                return current;

            return FindParentForNewNodeInsert(value, current.LeftDescendant);
        }

        public void Traverse(BinaryTreeVisitor<T> visitor)
        {
            Traverse(_root, visitor);
        }

        private static void Traverse(BinaryTreeNode<T> node, BinaryTreeVisitor<T> visitor)
        {
            if(node.LeftDescendant != null)
                Traverse(node.LeftDescendant, visitor);

                visitor(node.Value);

            if (node.RightDescendant != null)
                Traverse(node.RightDescendant, visitor);
        }

        public bool ContainsSameT(BinaryTree<T> other)
        {
            if (_root == null && other.Root == null)
                return true;

            if (other.Root == null && other.Root != null)
                return false;

            if (other.Root != null && other.Root == null)
                return false;

            var hash1 = new HashSet<T>();
            var hash2 = new HashSet<T>();

            TraverseBuildUnique(_root, hash1);
            TraverseBuildUnique(other.Root, hash2);

            return hash1.SetEquals(hash2);

        }

        private static void TraverseBuildUnique(BinaryTreeNode<T> current, HashSet<T> hash)
        {
            hash.Add(current.Value);

            if (current.LeftDescendant != null)
                TraverseBuildUnique(current.LeftDescendant, hash);
            
            if (current.RightDescendant != null)
                TraverseBuildUnique(current.RightDescendant, hash);
        }

        private enum EdgeDirection
        {
            Left,
            Right
        }

    }


}
