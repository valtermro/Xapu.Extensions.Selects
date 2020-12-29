using Xapu.Extensions.Selects.Core;

namespace Xapu.Extensions.Selects
{
    public static partial class SelectExtensions
    {
        public static TResult ToNew<TResult>(this object source)
            where TResult : class, new()
        {
            var selector = ObjectSelectorBag.GetForObjectType(source.GetType());

            return selector.Select<TResult>(source);
        }
    }
}
