using System;
using System.Collections.Concurrent;
using Xapu.Extensions.Selects.Core.Base;
using Xapu.Extensions.Selects.Core.Selectors;

namespace Xapu.Extensions.Selects.Core
{
    internal static class EnumerableSelectProxyBag
    {
        private static readonly ConcurrentDictionary<Type, IEnumerableSelector> Instances = new ConcurrentDictionary<Type, IEnumerableSelector>();
        
        public static IEnumerableSelector GetForType(Type type)
        {
            if (!Instances.ContainsKey(type))
                Instances[type] = CreateForType(type);

            return Instances[type];
        }

        private static IEnumerableSelector CreateForType(Type sourceType)
        {
            var proxyType = typeof(EnumerableSelector<>);
            var elementType = sourceType.GetCollectionElementType();

            var instanceType = proxyType.MakeGenericType(elementType);
            var instance =  Activator.CreateInstance(instanceType);

            return (IEnumerableSelector)instance;
        }
    }
}
