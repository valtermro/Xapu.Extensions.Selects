using System.Collections.Generic;
using System.Linq;
using Xapu.Extensions.Selects.Core;

namespace Xapu.Extensions.Selects
{
    public static class SelectExtensions
    {
        public static TResult ToNew<TResult>(this object source)
            where TResult : class, new()
        {
            var proxy = ObjectSelectProxyBag.GetForType(source.GetType());
            
            return proxy.Select<TResult>(source);
        }

        public static IEnumerable<TResult> SelectNew<TResult>(this IEnumerable<object> source)
            where TResult : class, new()
        {
            var proxy = EnumerableSelectProxyBag.GetForType(source.GetType());

            return proxy.Select<TResult>(source);
        }

        public static IQueryable<TResult> SelectNew<TResult>(this IQueryable<object> source)
            where TResult : class, new()
        {
            var proxy = QueryableSelectProxyBag.GetForType(source.GetType());

            return proxy.Select<TResult>(source);
        }
    }
}
