using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTreeTests.comparers
{
    class ReverseIntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x == y) return 0;
            if (x < y) return 1;
            return -1;
        }
    }
}
