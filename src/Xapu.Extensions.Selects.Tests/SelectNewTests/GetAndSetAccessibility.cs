using System.Linq;
using Xapu.Extensions.Selects.Tests.Base;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectNewTests
{
    public class GetAndSetAccessibility
    {
        [Fact]
        public void IgnoresResultPropertiesWithNoAccessibleSetter()
        {
            var create = Creator.New(() => new BasicTypes
            {
                Bool = true,
                Int = 42,
                Float = 42.42f
            });

            static void Assertions(NoAccessibleSetters result)
            {
                Assert.False(result.Bool);
                Assert.Equal(0, result.Int);
                Assert.Equal(0, result.Float);
            };

            Assertions(create.Object().ToNew<NoAccessibleSetters>());
            Assertions(create.Array().SelectNew<NoAccessibleSetters>().First());
            Assertions(create.Queryable().SelectNew<NoAccessibleSetters>().First());
        }

        [Fact]
        public void IgnoresSourcePropertiesWithNoAccessibleGetter()
        {
            var create = Creator.New(() => new NoAccessibleGetters
            {
                Int = 42,
                Float = 42.42f
            });

            static void Assertions(BasicTypes result)
            {
                Assert.Equal(0, result.Int);
                Assert.Equal(0, result.Float);
            };

            Assertions(create.Object().ToNew<BasicTypes>());
            Assertions(create.Array().SelectNew<BasicTypes>().First());
            Assertions(create.Queryable().SelectNew<BasicTypes>().First());
        }
    }
}
