using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Mapper
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
