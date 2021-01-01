namespace Xapu.Extensions.Selects.Tests
{
    public class NullableValues
    {
        public int? NullableInt { get; set; } = 1;
        public int NonNullableInt { get; set; } = 2;
    }

    public class ToNullableValues
    {
        public int? NullableInt { get; set; } = 1;
        public int? NonNullableInt { get; set; }
    }

    public class ToNonNullableValues
    {
        public int NullableInt { get; set; } = 1;
        public int NonNullableInt { get; set; }
    }
}
