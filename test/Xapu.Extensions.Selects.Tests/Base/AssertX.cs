using System;
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
    }
}
