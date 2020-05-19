using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Utilities.Exceptions
{
    public class OutOfBorderException : Exception
    {
        public OutOfBorderException() : base(@"Out of Border...")
        {

        }
    }
}
