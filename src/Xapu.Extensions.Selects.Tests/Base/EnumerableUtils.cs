using System;
using System.Collections.Generic;
using System.Linq;

namespace Xapu.Extensions.Selects.Tests
{
    internal static class EnumerableUtils
    {
        public static Type GetArrayIteratorType<T>()
        {
            return new T[] { default }.Select(p => p).GetType();
        }

        public static Type GetListIteratorType<T>()
        {
            return new List<T> { default }.Select(p => p).GetType();
        }
    }
}
