using System;
using System.Collections.Generic;

namespace ConsoleApplication1.DataStructures
{
    public delegate void BinarySearchTreeVisitor<in T>(T nodeData);

    public partial class BinarySearchTree<T> where T : IComparable<T>
    {
        private BinaryTreeNode<T> _root = null;
        private IComparer<T> _comparer = null;

        public int Size { get; set; }

        public BinaryTreeNode<T> Root
        {
            get { return _root; }
        }

        public BinarySearchTree()
        {
            _comparer = Comparer<T>.Default;
        }

        public BinarySearchTree(T value, IComparer<T> comparer)
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

            if (parent == null) return value;

            if (_comparer.Compare(value, parent.Value) > 0)
            {
                parent.RightDescendant = newNode;
            }
            else
            {
                parent.LeftDescendant = newNode;
            }

            Size++;

            return value;
        }

        public bool Exists(T value)
        {
            if (_root == null)
                return false;

            var node = Search(value, _root);

            return node != null;
        }

        public void BreadthFirst(BinarySearchTreeVisitor<T> visitor)
        {
            if (_root == null)
                return;

            //visitor(_root.Value);

            var q = new Queue<BinaryTreeNode<T>>();
            q.Enqueue(_root);

            while(q.Count > 0)
            {
                BinaryTreeNode<T> temp = q.Dequeue();

                visitor(temp.Value);

                if(temp.LeftDescendant != null)
                    q.Enqueue(temp.LeftDescendant);

                if(temp.RightDescendant != null)
                    q.Enqueue(temp.RightDescendant);
            }
        }
        
        private BinaryTreeNode<T> Search(T value, BinaryTreeNode<T> current)
        {
            if (_comparer.Compare(value, current.Value) == 0)
                return current;

            var direction = EdgeDirection.Left;

            if (_comparer.Compare(value, current.Value) > 0)
                direction = EdgeDirection.Right;

            if (direction == EdgeDirection.Left)
            {
                if (current.LeftDescendant == null)
                    return null;

                return Search(value, current.LeftDescendant);
            }
            else
            {
                if (current.RightDescendant == null)
                    return null;

                return Search(value, current.RightDescendant);

            }
        }

        public T Min()
        {
            if (_root == null)
                return default(T);

            return findMin(_root).Value;
        }

        public T Max()
        {
            if (_root == null)
                return default(T);

            return findMax(_root).Value;
        }

        private BinaryTreeNode<T> findMax(BinaryTreeNode<T> current)
        {
            if (current.RightDescendant == null)
                return current;

            return findMin(current.RightDescendant);
        }


        private BinaryTreeNode<T> findMin(BinaryTreeNode<T> current)
        {
            if (current.LeftDescendant == null)
                return current;

            return findMin(current.LeftDescendant);
        }

        public void TraverseInOrder(BinaryTreeVisitor<T> visitor)
        {
            TraverseInOrder(_root, visitor);
        }

        private static void TraverseInOrder(BinaryTreeNode<T> node, BinaryTreeVisitor<T> visitor)
        {
            if(node.LeftDescendant != null)
                TraverseInOrder(node.LeftDescendant, visitor);

                visitor(node.Value);

            if (node.RightDescendant != null)
                TraverseInOrder(node.RightDescendant, visitor);
        }

        public void TraversePreOrder(BinaryTreeVisitor<T> visitor)
        {
            TraversePreOrder(_root, visitor);
        }

        private static void TraversePreOrder(BinaryTreeNode<T> node, BinaryTreeVisitor<T> visitor)
        {

            visitor(node.Value);

            if (node.LeftDescendant != null)
                TraverseInOrder(node.LeftDescendant, visitor);

            if (node.RightDescendant != null)
                TraverseInOrder(node.RightDescendant, visitor);
        }


        private BinaryTreeNode<T> FindParentForNewNodeInsert(T value, BinaryTreeNode<T> current)
        {
            
            if (_comparer.Compare(value, current.Value) == 0)
                return null;

            var direction = EdgeDirection.Left;

            if (_comparer.Compare(value, current.Value) > 0)
                direction = EdgeDirection.Right;

            if (direction == EdgeDirection.Left)
            {
                if (current.LeftDescendant == null)
                    return current;

                return FindParentForNewNodeInsert(value, current.LeftDescendant);
            }
            else
            {
                if (current.RightDescendant == null)
                    return current;

                return FindParentForNewNodeInsert(value, current.RightDescendant);

            }
        }

        public bool TreeEquals(BinarySearchTree<T> other)
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
