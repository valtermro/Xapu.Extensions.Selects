using System;
using Xunit;

namespace Xapu.Extensions.Selects.Tests
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

        public static void AssignableFrom(object subject, Type type)
        {
            Assert.True(type.IsAssignableFrom(subject.GetType()));
        }

        public static void ObjectFieldEqual(object expectedValue, object subject, string fieldName)
        {
            var type = subject.GetType();
            var field = type.GetField(fieldName);
            var value = field?.GetValue(subject);
            var casted = Convert.ChangeType(value, expectedValue.GetType());

            Assert.Equal(expectedValue, casted);
        }
    }
}
