namespace Xapu.Extensions.Selects.Core.Selectors
{
    internal interface IObjectSelector
    {
        TResult Select<TResult>(object source);
    }

    internal class ObjectSelector<TSource> : IObjectSelector
    {
        public TResult Select<TResult>(object source)
        {
            var func = MapperFuncBag.Get<TSource, TResult>();

            return func((TSource)source);
        }
    }
}
