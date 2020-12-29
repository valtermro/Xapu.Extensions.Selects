namespace Xapu.Extensions.Selects.Tests.FixtureTypes
{
    public class ObjectType
    {
        public int Id { get; set; }
        public ObjectType Parent { get; set; }
    }

    public class WithIdView
    {
        public int Id { get; set; }
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
