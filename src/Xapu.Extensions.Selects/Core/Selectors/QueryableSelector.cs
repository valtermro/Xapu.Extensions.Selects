using System.Linq;
using Xapu.Extensions.Selects.Core.Mappers;

namespace Xapu.Extensions.Selects.Core.Selectors
{
    internal interface IQueryableSelector
    {
        IQueryable<TResult> Select<TResult>(IQueryable<object> source);
    }

    internal class QueryableSelector<TSource> : IQueryableSelector
    {
        public IQueryable<TResult> Select<TResult>(IQueryable<object> source)
        {
            var expression = MapperExpressionBag.Get<TSource, TResult>();

            return ((IQueryable<TSource>)source).Select(expression);
        }
    }
}
