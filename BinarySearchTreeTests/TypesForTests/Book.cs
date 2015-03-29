using System;

namespace BinarySearchTreeTests.TypesForTests
{
    class Book : IEquatable<Book>, IComparable<Book>
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }


        public bool Equals(Book other)
        {
            if (other == null)
            {
                return false;
            }
            if (Author == other.Author && Title == other.Title &&
                Publisher == other.Publisher && ISBN == other.ISBN)
            {
                return true;
            }
            return false;
        }

        public int CompareTo(Book other)
        {
            if (other == null)
            {
                return 1;
            }

            int result = String.Compare(ISBN, other.ISBN, StringComparison.Ordinal);
            if (result != 0)
            {
                return result;
            }

            result = String.Compare(Author, other.Author, StringComparison.Ordinal);
            if (result != 0)
            {
                return result;
            }

            result = String.Compare(Title, other.Title, StringComparison.Ordinal);
            if (result != 0)
            {
                return result;
            }

            result = String.Compare(Publisher, other.Publisher, StringComparison.Ordinal);
            if (result != 0)
            {
                return result;
            }

            return result;
        }

        public override int GetHashCode()
        {
            return (Author + Title).GetHashCode() ^ (Publisher + ISBN).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Book))
            {
                return false;
            }
            return this.Equals(obj as Book);
        }

        public override string ToString()
        {
            return "Author:" + Author + " Title:" + Title + " ISBN:" + ISBN + " Publisher:" + Publisher;
        }
    }
}
