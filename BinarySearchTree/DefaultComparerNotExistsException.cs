using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class DefaultComparerNotExistsException : Exception
    {
        public DefaultComparerNotExistsException()
            : base()
        {

        }
        public DefaultComparerNotExistsException(string message)
            : base(message)
        {

        }
        public DefaultComparerNotExistsException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
