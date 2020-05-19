using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Utilities.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException() : base(@"Invalid Command.")
        {

        }
    }
}
