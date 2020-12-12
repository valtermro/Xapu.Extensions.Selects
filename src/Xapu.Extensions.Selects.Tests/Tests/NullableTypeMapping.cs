using System.Linq;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests
{
    public class NullableTypeMapping
    {
        [Fact]
        public void NonNullableToNullable()
        {
            var source = new NullableValues
            {
                NonNullableInt = 1
            };

            RunAssertions(source.ToNew<ToNullableValues>());
            RunAssertions(new[] { source }.SelectNew<ToNullableValues>().First());
            RunAssertions(new[] { source }.AsQueryable().SelectNew<ToNullableValues>().First());

            static void RunAssertions(ToNullableValues result)
            {
                Assert.True(result.NonNullableInt.HasValue);
                Assert.Equal(1, result.NonNullableInt.Value);
            };
        }

        [Fact]
        public void NullableToNonNullable()
        {
            var source = new NullableValues
            {
                NullableInt = 1
            };

            RunAssertions(source.ToNew<ToNonNullableValues>());
            RunAssertions(new[] { source }.SelectNew<ToNonNullableValues>().First());
            RunAssertions(new[] { source }.AsQueryable().SelectNew<ToNonNullableValues>().First());

            static void RunAssertions(ToNonNullableValues result)
            {
                Assert.Equal(1, result.NullableInt);
            };
        }
    }
}
