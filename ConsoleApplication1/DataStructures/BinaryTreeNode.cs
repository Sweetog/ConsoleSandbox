namespace ConsoleApplication1.DataStructures
{
    public class BinaryTreeNode<T>
    {
        public BinaryTreeNode<T> LeftDescendant { get; set; }
        public BinaryTreeNode<T> RightDescendant { get; set; }
        public T Value { get; set; }

        public BinaryTreeNode(T value)
        {
            Value = value;
        }
    }
}
