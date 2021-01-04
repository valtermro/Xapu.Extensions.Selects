using System.Linq;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectNewTests
{
    public class NullableTypeMapping
    {
        [Fact]
        public void NonNullableToNullable()
        {
            var create = Creator.New(() => new NullableValues
            {
                NonNullableInt = 1
            });

            static void Assertions(ToNullableValues result)
            {
                Assert.True(result.NonNullableInt.HasValue);
                Assert.Equal(1, result.NonNullableInt.Value);
            };

            Assertions(create.Object().ToNew<ToNullableValues>());
            Assertions(create.Array().SelectNew<ToNullableValues>().First());
            Assertions(create.Queryable().SelectNew<ToNullableValues>().First());
        }

        [Fact]
        public void NullableToNonNullable()
        {
            var create = Creator.New(() => new NullableValues
            {
                NullableInt = 1
            });

            static void Assertions(ToNonNullableValues result)
            {
                Assert.Equal(1, result.NullableInt);
            };

            Assertions(create.Object().ToNew<ToNonNullableValues>());
            Assertions(create.Array().SelectNew<ToNonNullableValues>().First());
            Assertions(create.Queryable().SelectNew<ToNonNullableValues>().First());
        }
    }
}
