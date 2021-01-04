using System.Collections.Generic;
using System.Linq;

namespace Xapu.Extensions.Selects
{
    public static partial class SelectExtensions
    {
        public static IQueryable<TResult> SelectNew<TResult>(this IQueryable<object> source)
            where TResult : class, new()
        {
            var selector = QueryableSelectorBag.GetForQueryableType(source.GetType());

            return selector.Select<TResult>(source);
        }

        public static IQueryable<object> SelectFields<TSource>(this IQueryable<TSource> source, params string[] fields)
            where TSource : class
        {
            var selector = QueryableSelectorBag.GetForElementType(source);

            return selector.SelectFields(source, fields);
        }

        public static IQueryable<object> SelectFields<TSource>(this IQueryable<TSource> source, IEnumerable<string> fields)
            where TSource : class
        {
            var selector = QueryableSelectorBag.GetForElementType(source);

            return selector.SelectFields(source, fields);
        }
    }
}
