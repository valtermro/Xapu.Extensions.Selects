using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Xapu.Extensions.Selects.Core.Base;
using Xapu.Extensions.Selects.Core.Selectors;

namespace Xapu.Extensions.Selects.Core
{
    internal static class EnumerableSelectorBag
    {
        private static readonly ConcurrentDictionary<Type, object> Instances = new ConcurrentDictionary<Type, object>();
        
        public static IEnumerableSelector GetForEnumerableType(Type type)
        {
            if (!Instances.ContainsKey(type))
                Instances[type] = CreateForEnumerableType(type);

            return (IEnumerableSelector)Instances[type];
        }

        public static IEnumerableSelector<TElement> GetForElementType<TElement>(IEnumerable<TElement> source)
        {
            var type = source.GetType();

            if (!Instances.ContainsKey(type))
                Instances[type] = new EnumerableSelector<TElement>();

            return (IEnumerableSelector<TElement>)Instances[type];
        }

        private static IEnumerableSelector CreateForEnumerableType(Type sourceType)
        {
            var selectorType = typeof(EnumerableSelector<>);
            var elementType = sourceType.GetCollectionElementType();

            var instanceType = selectorType.MakeGenericType(elementType);
            var instance =  Activator.CreateInstance(instanceType);

            return (IEnumerableSelector)instance;
        }
    }
}
