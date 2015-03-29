using System;
using System.Collections.Generic;
using BinarySearchTreeTests.TypesForTests;

namespace BinarySearchTreeTests.comparers
{
    class BookComparer : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return String.Compare(x.Title, y.Title, StringComparison.Ordinal);
        }
    }
}
