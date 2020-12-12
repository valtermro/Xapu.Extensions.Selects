using Xapu.Extensions.Selects.Mappers;

namespace Xapu.Extensions.Selects.Proxies
{
    internal interface IObjectSelectProxy
    {
        TResult Select<TResult>(object source);
    }

    internal class ObjectSelectProxy<TSource> : IObjectSelectProxy
    {
        public TResult Select<TResult>(object source)
        {
            var func = MapperFuncBag.Get<TSource, TResult>();

            return func((TSource)source);
        }
    }
}
