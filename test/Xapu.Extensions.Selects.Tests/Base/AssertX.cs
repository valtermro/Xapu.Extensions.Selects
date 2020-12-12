using System;
using System.Linq;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.Base
{
    internal static class AssertX
    {
        public static T Type<T>(T value, Type expectedType)
        {
            Assert.IsType(expectedType, value);
            return value;
        }

        public static T NotThrowing<T>(Func<T> func)
        {
            T result = default;

            var ex = Record.Exception(() => result = func());
            Assert.True(ex == null, $"Unexpected {ex?.GetType()}");
            return result;
        }

        internal static void AssignableFrom(object subject, Type type)
        {
            Assert.True(type.IsAssignableFrom(subject.GetType()));
        }
    }
}
