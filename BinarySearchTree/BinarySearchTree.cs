using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinarySearchTree
{

    public class BinarySearchTree<T> : IEnumerable<T>
    {
        private IComparer<T> comparer;
        private TreeNode rootNode;

        public BinarySearchTree()
        {
            if (TryGetDefaultComparer())
            {
                this.comparer = Comparer<T>.Default;
            }
            else
            {
                string message = String.Format("Type {0} doesn't implements IComparable.{1}For the correct work IComparer<{0}> required. ",
                    typeof(T), Environment.NewLine);
                throw new DefaultComparerNotExistsException(message);
            }
        }

        public BinarySearchTree(IComparer<T> comparer)
        {
            if (comparer == null)
            {
                if (TryGetDefaultComparer())
                    this.comparer = Comparer<T>.Default;
                else
                    throw new ArgumentNullException("comparer");
            }
            else
            {
                this.comparer = comparer;
            }
        }

        public void Add(T value)
        {
            TreeNode parentNode = null;
            TreeNode tempNode = rootNode;
            while (tempNode != null)
            {
                parentNode = tempNode;
                if (comparer.Compare(value, tempNode.Value) < 0)
                    tempNode = tempNode.LeftChildNode;
                else
                    tempNode = tempNode.RightChildNode;
            }

            var newNode = new TreeNode(value) { ParentNode = parentNode };
            if (parentNode == null)
            {
                rootNode = newNode;
            }
            else
            {
                if (comparer.Compare(value, parentNode.Value) < 0)
                    parentNode.LeftChildNode = newNode;
                else
                    parentNode.RightChildNode = newNode;
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            foreach (var value in collection)
            {
                Add(value);
            }
        }

        public void Remove(T value)
        {
            TreeNode node = GetNode(value);
            TreeNode removedNode;
            TreeNode tempNode;

            if (node.LeftChildNode == null || node.RightChildNode == null)
                removedNode = node;
            else
                removedNode = GetNexNode(node);

            if (removedNode.LeftChildNode != null)
                tempNode = removedNode.LeftChildNode;
            else
                tempNode = removedNode.RightChildNode;


            if (tempNode != null)
                tempNode.ParentNode = removedNode.ParentNode;


            if (removedNode.ParentNode == null)
                rootNode = tempNode;
            else
            {
                if (removedNode.ParentNode.LeftChildNode == removedNode)
                    removedNode.ParentNode.LeftChildNode = tempNode;
                else
                    removedNode.ParentNode.RightChildNode = tempNode;

            }
            if (removedNode != node)
                node.Value = removedNode.Value;


        }

        public bool Contains(T value)
        {
            return GetNode(value) != null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrder().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> PreOrder()
        {
            var top = rootNode;
            var stack = new Stack<TreeNode>();

            while (top != null || stack.Count != 0)
            {
                if (stack.Count != 0)
                {
                    top = stack.Pop();
                }
                while (top != null)
                {
                    yield return top.Value;
                    if (top.RightChildNode != null)
                        stack.Push(top.RightChildNode);
                    top = top.LeftChildNode;
                }
            }
        }

        public IEnumerable<T> InOrder()
        {
            var top = rootNode;
            var stack = new Stack<TreeNode>();

            while (top != null || stack.Count != 0)
            {
                if (stack.Count != 0)
                {
                    top = stack.Pop();
                    yield return top.Value;

                    top = top.RightChildNode;
                }
                while (top != null)
                {
                    stack.Push(top);
                    top = top.LeftChildNode;
                }
            }
        }

        public IEnumerable<T> PostOrder()
        {
            var top = rootNode;
            var stack = new Stack<TreeNode>();

            while (top != null || stack.Count != 0)
            {
                if (stack.Count != 0)
                {
                    top = stack.Pop();
                    if (stack.Count != 0 && top.RightChildNode == stack.Peek())
                    {
                        top = stack.Pop();
                    }
                    else
                    {
                        yield return top.Value;
                        top = null;
                    }
                }
                while (top != null)
                {
                    stack.Push(top);
                    if (top.RightChildNode != null)
                    {
                        stack.Push(top.RightChildNode);
                        stack.Push(top);
                    }

                    top = top.LeftChildNode;
                }
            }
        }

        private TreeNode GetNode(T value)
        {
            TreeNode currentNode = rootNode;
            while (currentNode != null && comparer.Compare(value, currentNode.Value) != 0)
            {
                if (comparer.Compare(value, currentNode.Value) < 0)
                    currentNode = currentNode.LeftChildNode;
                else
                    currentNode = currentNode.RightChildNode;
            }

            return currentNode;
        }

        private TreeNode GetNexNode(TreeNode targetNode)
        {
            if (targetNode.RightChildNode != null)
                return GetMinimum(targetNode.RightChildNode);

            TreeNode nextNode = targetNode.ParentNode;
            while (nextNode != null && targetNode == nextNode.RightChildNode)
            {
                targetNode = nextNode;
                nextNode = nextNode.ParentNode;
            }

            return nextNode;
        }

        private TreeNode GetMinimum(TreeNode root)
        {
            while (root.LeftChildNode != null)
                root = root.LeftChildNode;
            return root;
        }

        private bool TryGetDefaultComparer()
        {
            return (typeof(IComparable)).IsAssignableFrom(typeof(T)) || (typeof(IComparable<T>)).IsAssignableFrom(typeof(T));

        }

        private class TreeNode
        {
            public TreeNode LeftChildNode { get; set; }
            public TreeNode RightChildNode { get; set; }
            public TreeNode ParentNode { get; set; }
            public T Value { get; set; }

            public TreeNode(T value)
            {
                Value = value;
                LeftChildNode = null;
                RightChildNode = null;
                ParentNode = null;
            }
        }
    }
}
