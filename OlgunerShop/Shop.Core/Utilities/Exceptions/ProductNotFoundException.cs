using Remotion.Linq.Parsing.Structure.ExpressionTreeProcessors;
using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Utilities.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        
         
        public ProductNotFoundException() : base(@"Product Nesnenin bir örneğine rastlanamadı")
        {

        }

         
    }
}
