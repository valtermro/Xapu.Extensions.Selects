namespace Xapu.Extensions.Selects.Tests
{
    public class ObjectType
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public ObjectType Parent { get; set; }
    }

    public class WithIdView
    {
        public int Id { get; set; }
    }

    public class WithValueView : WithIdView
    {
        public string Value { get; set; }
    }

    public class ShallowObjectView : WithValueView
    {
        public WithValueView Parent { get; set; }
    }

    public class NestedObjectView : WithValueView
    {
        public ShallowObjectView Parent { get; set; }
    }
}
