using System;
using System.Collections.Concurrent;
using Xapu.Extensions.Selects.Core.Base;
using Xapu.Extensions.Selects.Core.Selectors;

namespace Xapu.Extensions.Selects.Core
{
    internal static class QueryableSelectorBag
    {
        private static readonly ConcurrentDictionary<Type, IQueryableSelector> Instances = new ConcurrentDictionary<Type, IQueryableSelector>();

        public static IQueryableSelector GetForType(Type type)
        {
            if (!Instances.ContainsKey(type))
                Instances[type] = CreateForType(type);

            return Instances[type];
        }

        private static IQueryableSelector CreateForType(Type sourceType)
        {
            var selectorType = typeof(QueryableSelector<>);
            var elementType = sourceType.GetCollectionElementType();

            var instanceType = selectorType.MakeGenericType(elementType);
            var instance = Activator.CreateInstance(instanceType);

            return (IQueryableSelector)instance;
        }
    }
}
