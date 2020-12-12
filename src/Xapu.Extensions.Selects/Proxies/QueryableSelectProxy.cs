using System.Linq;
using Xapu.Extensions.Selects.Mappers;

namespace Xapu.Extensions.Selects.Proxies
{
    internal interface IQueryableSelectProxy
    {
        IQueryable<TResult> Select<TResult>(IQueryable<object> source);
    }

    internal class QueryableSelectProxy<TSource> : IQueryableSelectProxy
    {
        public IQueryable<TResult> Select<TResult>(IQueryable<object> source)
        {
            var expression = MapperExpressionBag.Get<TSource, TResult>();

            return ((IQueryable<TSource>)source).Select(expression);
        }
    }
}
