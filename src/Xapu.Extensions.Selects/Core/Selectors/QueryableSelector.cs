using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xapu.Extensions.Selects
{
    internal interface IQueryableSelector
    {
        IQueryable<TResult> Select<TResult>(IQueryable<object> source);
    }

    internal interface IQueryableSelector<TSource>
    {
        public IQueryable<object> SelectFields(IQueryable<TSource> source, IEnumerable<string> fields);
    }

    internal class QueryableSelector<TSource> : IQueryableSelector<TSource>, IQueryableSelector
    {
        private static readonly MethodInfo SelectMethodInfo = typeof(Queryable).GetMethods().First(p => p.Name == "Select");

        public IQueryable<TResult> Select<TResult>(IQueryable<object> source)
        {
            var expression = MapperExpressionBag.Get<TSource, TResult>();

            return ((IQueryable<TSource>)source).Select(expression);
        }

        public IQueryable<object> SelectFields(IQueryable<TSource> source, IEnumerable<string> fields)
        {
            var sourceType = typeof(TSource);
            var resultType = WithFieldsTypeBag.Get(sourceType, fields);

            var expression = MapperExpressionBag.Get(sourceType, resultType);
            var selectMethod = SelectMethodInfo.MakeGenericMethod(sourceType, resultType);

            return (IQueryable<object>)selectMethod.Invoke(null, new object[] { source, expression });
        }
    }
}
