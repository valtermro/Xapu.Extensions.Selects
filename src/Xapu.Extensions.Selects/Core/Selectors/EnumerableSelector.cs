using System.Collections.Generic;
using System.Linq;
using Xapu.Extensions.Selects.Core.Mappers;

namespace Xapu.Extensions.Selects.Core.Selectors
{
    internal interface IEnumerableSelector
    {
        IEnumerable<TResult> Select<TResult>(IEnumerable<object> source);
    }

    internal class EnumerableSelector<TSource> : IEnumerableSelector
    {
        public IEnumerable<TResult> Select<TResult>(IEnumerable<object> source)
        {
            var func = MapperFuncBag.Get<TSource, TResult>();

            return ((IEnumerable<TSource>)source).Select(func);
        }
    }
}
