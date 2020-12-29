using System.Collections.Generic;

namespace Xapu.Extensions.Selects.Tests.FixtureTypes
{
    public class CollectionsOfObjects<T>
    {
        public T[] ObjectArray { get; set; }
        public IEnumerable<T> ObjectIEnumerable { get; set; }
    }

    public class CollectionsOfObjects : CollectionsOfObjects<ObjectType>
    {
    }

    public class CollectionsOfObjectViews
    {
        public NestedObjectView[] ObjectArray { get; set; }
    }
}
