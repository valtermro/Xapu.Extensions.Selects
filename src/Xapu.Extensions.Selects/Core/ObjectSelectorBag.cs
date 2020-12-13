using System;
using System.Collections.Concurrent;
using Xapu.Extensions.Selects.Core.Selectors;

namespace Xapu.Extensions.Selects.Core
{
    internal static class ObjectSelectorBag
    {
        private static readonly ConcurrentDictionary<Type, IObjectSelector> Instances = new ConcurrentDictionary<Type, IObjectSelector>();

        public static IObjectSelector GetForType(Type type)
        {
            if (!Instances.ContainsKey(type))
                Instances[type] = CreateForType(type);

            return Instances[type];
        }

        private static IObjectSelector CreateForType(Type sourceType)
        {
            var selectorType = typeof(ObjectSelector<>);

            var instanceType = selectorType.MakeGenericType(sourceType);
            var instance = Activator.CreateInstance(instanceType);

            return (IObjectSelector)instance;
        }
    }
}
