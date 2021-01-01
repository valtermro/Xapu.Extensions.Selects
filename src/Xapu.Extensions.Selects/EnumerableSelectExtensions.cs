using System.Collections.Generic;

namespace Xapu.Extensions.Selects
{
    public static partial class SelectExtensions
    {
        public static IEnumerable<TResult> SelectNew<TResult>(this IEnumerable<object> source)
            where TResult : class, new()
        {
            var selector = EnumerableSelectorBag.GetForEnumerableType(source.GetType());

            return selector.Select<TResult>(source);
        }

        public static IEnumerable<object> SelectFields<TSource>(this IEnumerable<TSource> source, params string[] fields)
            where TSource : class
        {
            var selector = EnumerableSelectorBag.GetForElementType(source);

            return selector.SelectFields(source, fields);
        }

        public static IEnumerable<object> SelectFields<TSource>(this IEnumerable<TSource> source, IEnumerable<string> fields)
            where TSource : class
        {
            var selector = EnumerableSelectorBag.GetForElementType(source);

            return selector.SelectFields(source, fields);
        }
    }
}
