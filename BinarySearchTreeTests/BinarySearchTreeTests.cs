using System;
using System.Linq;
using BinarySearchTreeTests.comparers;
using BinarySearchTreeTests.TypesForTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinarySearchTree;

namespace BinarySearchTreeTests
{
    [TestClass]
    public class BinarySearchTreeTests
    {
        [TestMethod]
        public void AddIntWithoutComparer()
        {
            var intsTree = new BinarySearchTree<int>();
            var innerData = new[] { 5, 3, 1, 4, 7, 9, 6, 8, 2 };

            intsTree.AddRange(innerData);

            Array.Sort(innerData);
            var actual = intsTree.ToArray();    // GetEnumerator uses In-order  

            CollectionAssert.AreEqual(innerData, actual);
        }

        [TestMethod]
        public void AddIntWithReverseComparer()
        {
            var intsTree = new BinarySearchTree<int>(new ReverseIntComparer());
            var innerData = new[] { 5, 3, 1, 4, 7, 9, 6, 8, 2 };

            intsTree.AddRange(innerData);

            // Array.Sort(innerData, new ReverseIntComparer());
            var expected = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            var actual = intsTree.ToArray();    // GetEnumerator uses In-order  

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddString()
        {
            var intsTree = new BinarySearchTree<string>();
            var innerData = new[] { "abc", "abd", "abcd", "abe" };

            intsTree.AddRange(innerData);

            var expected = new[] { "abc", "abcd", "abd", "abe" };
            var actual = intsTree.ToArray();    // GetEnumerator uses In-order  

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddStringWithOrdinalIgnoreCaseComparer()
        {
            var intsTree = new BinarySearchTree<string>(StringComparer.OrdinalIgnoreCase);
            var innerData = new[] { "Abc", "abd", "ABcd", "aBe" };

            intsTree.AddRange(innerData);

            var expected = new[] { "Abc", "ABcd", "abd", "aBe" };
            var actual = intsTree.ToArray();    // GetEnumerator uses In-order  

            CollectionAssert.AreEqual(expected, actual);
        }

        [ExpectedException(typeof(DefaultComparerNotExistsException))]
        [TestMethod]
        public void AddPointStructWithoutComparer()
        {
            var intsTree = new BinarySearchTree<Point2D>();
            var innerData = new[] { new Point2D(0, 0), new Point2D(0, 1) };

            intsTree.AddRange(innerData);
        }

        [TestMethod]
        public void AddBookWitoutComparer()
        {
            // book's compareTo compares first of all by isbn
            var intsTree = new BinarySearchTree<Book>();
            var innerData = new[] { new Book { ISBN = "1111" }, new Book { ISBN = "3333" }, new Book { ISBN = "2222" } };

            intsTree.AddRange(innerData);

            var expected = new[] { new Book { ISBN = "2222" }, new Book { ISBN = "3333" }, new Book { ISBN = "1111" } };
            var actual = intsTree.PostOrder().ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddBookWithTitleComparer()
        {
            var intsTree = new BinarySearchTree<Book>(new BookComparer());
            var innerData = new[] { new Book { ISBN = "1111", Title = "3" }, new Book { ISBN = "3333", Title = "2" }, 
                new Book { ISBN = "2222", Title = "1" } };

            intsTree.AddRange(innerData);

            var expected = new[] { new Book { ISBN = "1111", Title = "3" }, new Book { ISBN = "3333", Title = "2" },
                new Book { ISBN = "2222", Title = "1" }};

            var actual = intsTree.PreOrder().ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
