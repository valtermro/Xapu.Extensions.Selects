using System;
using System.Collections.Generic;
using System.Linq;

namespace Xapu.Extensions.Selects.Tests.Base
{
    public static class Creator
    {
        public static Creator<T> New<T>(Func<T> func) => new Creator<T>(func);
    }

    public class Creator<T>
    {
        private readonly Func<T> _func;

        public Creator(Func<T> func) => _func = func;

        public T Object() => _func();
        public T[] Array() => new T[] { _func() };
        public IQueryable<T> Queryable() => Array().AsQueryable();
    }
}
