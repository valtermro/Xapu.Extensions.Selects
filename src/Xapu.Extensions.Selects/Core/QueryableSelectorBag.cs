using System;
using System.Collections.Concurrent;
using System.Linq;
using Xapu.Extensions.Selects.Core.Base;
using Xapu.Extensions.Selects.Core.Selectors;

namespace Xapu.Extensions.Selects.Core
{
    internal static class QueryableSelectorBag
    {
        private static readonly ConcurrentDictionary<Type, object> Instances = new ConcurrentDictionary<Type, object>();

        public static IQueryableSelector GetForQueryableType(Type type)
        {
            if (!Instances.ContainsKey(type))
                Instances[type] = CreateForQueryableType(type);

            return (IQueryableSelector)Instances[type];
        }

        public static IQueryableSelector<TElement> GetForElementType<TElement>(IQueryable<TElement> source)
        {
            var type = source.GetType();

            if (!Instances.ContainsKey(type))
                Instances[type] = new QueryableSelector<TElement>();

            return (IQueryableSelector<TElement>)Instances[type];
        }

        private static IQueryableSelector CreateForQueryableType(Type sourceType)
        {
            var selectorType = typeof(QueryableSelector<>);
            var elementType = sourceType.GetCollectionElementType();

            var instanceType = selectorType.MakeGenericType(elementType);
            var instance = Activator.CreateInstance(instanceType);

            return (IQueryableSelector)instance;
        }
    }
}
