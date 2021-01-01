using System;
using System.Collections.Generic;
using System.Linq;

namespace Xapu.Extensions.Selects
{
    internal interface IEnumerableSelector
    {
        IEnumerable<TResult> Select<TResult>(IEnumerable<object> source);
    }

    internal interface IEnumerableSelector<TSource>
    {
        public IEnumerable<object> SelectFields(IEnumerable<TSource> source, IEnumerable<string> fields);
    }

    internal class EnumerableSelector<TSource> : IEnumerableSelector<TSource>, IEnumerableSelector
    {
        public IEnumerable<TResult> Select<TResult>(IEnumerable<object> source)
        {
            var func = MapperFuncBag.Get<TSource, TResult>();

            return ((IEnumerable<TSource>)source).Select(func);
        }

        public IEnumerable<object> SelectFields(IEnumerable<TSource> source, IEnumerable<string> fields)
        {
            var sourceType = typeof(TSource);
            var resultType = WithFieldsTypeBag.Get(sourceType, fields);

            var func = (Func<TSource, object>)MapperFuncBag.Get(sourceType, resultType);
            return source.Select(func);
        }
    }
}
