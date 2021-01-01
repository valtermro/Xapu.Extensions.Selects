namespace Xapu.Extensions.Selects.Tests
{
    public class NoAccessibleSetters
    {
        public bool Bool { get; }
        public int Int { get; protected set; }
        public float Float { get; private set; }
    }

    public class NoAccessibleGetters
    {
        public int Int { protected get; set; }
        public float Float { private get; set; }
    }
}
