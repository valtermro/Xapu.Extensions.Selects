using System.Collections.Generic;
using System.Linq;
using Xapu.Extensions.Selects.Mappers;

namespace Xapu.Extensions.Selects.Proxies
{
    internal interface IEnumerableSelectProxy
    {
        IEnumerable<TResult> Select<TResult>(IEnumerable<object> source);
    }

    internal class EnumerableSelectProxy<TSource> : IEnumerableSelectProxy
    {
        public IEnumerable<TResult> Select<TResult>(IEnumerable<object> source)
        {
            var func = MapperFuncBag.Get<TSource, TResult>();

            return ((IEnumerable<TSource>)source).Select(func);
        }
    }
}
