using System;

namespace Xapu.Extensions.Selects.Tests.FixtureTypes
{
    public class ObjectType
    {
        public Guid Id { get; set; }
        public ObjectType Parent { get; set; }
    }

    public class WithIdView
    {
        public Guid Id { get; set; }
    }

    public class ShallowObjectView : WithIdView
    {
        public WithIdView Parent { get; set; }
    }

    public class NestedObjectView : WithIdView
    {
        public ShallowObjectView Parent { get; set; }
    }
}
