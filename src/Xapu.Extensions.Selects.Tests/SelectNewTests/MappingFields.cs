using System.Linq;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectNewTests
{
    public class MappingFields
    {
        [Fact]
        public void MappingPublicFieldsToPublicFields()
        {
            var create = Creator.New(() => new PublicFields
            {
                Bool = true,
                Int = 42
            });

            static void Assertions(PublicFieldsView result)
            {
                Assert.True(result.Bool);
                Assert.Equal(42, result.Int);
            };

            Assertions(create.Object().ToNew<PublicFieldsView>());
            Assertions(create.Array().SelectNew<PublicFieldsView>().First());
            Assertions(create.Queryable().SelectNew<PublicFieldsView>().First());
        }

        [Fact]
        public void MappingPublicFieldsToPrivateFields()
        {
            var create = Creator.New(() => new PublicFields
            {
                Bool = true,
                Int = 42
            });

            static void Assertions(PrivateFields result)
            {
                Assert.False(result.GetBool());
                Assert.Equal(0, result.GetInt());
            };

            Assertions(create.Object().ToNew<PrivateFields>());
            Assertions(create.Array().SelectNew<PrivateFields>().First());
            Assertions(create.Queryable().SelectNew<PrivateFields>().First());
        }

        [Fact]
        public void MappingPrivateFieldsToPublicFields()
        {
            var create = Creator.New(() => new PrivateFields(true, 5));

            static void Assertions(PublicFields result)
            {
                Assert.False(result.Bool);
                Assert.Equal(0, result.Int);
            };

            Assertions(create.Object().ToNew<PublicFields>());
            Assertions(create.Array().SelectNew<PublicFields>().First());
            Assertions(create.Queryable().SelectNew<PublicFields>().First());
        }

        [Fact]
        public void MappingPublicFieldsToProperties()
        {
            var create = Creator.New(() => new PublicFields
            {
                Bool = true,
                Int = 42
            });

            static void Assertions(BasicTypes result)
            {
                Assert.True(result.Bool);
                Assert.Equal(42, result.Int);
            };

            Assertions(create.Object().ToNew<BasicTypes>());
            Assertions(create.Array().SelectNew<BasicTypes>().First());
            Assertions(create.Queryable().SelectNew<BasicTypes>().First());
        }

        [Fact]
        public void MappingPropertiesToPublicFields()
        {
            var create = Creator.New(() => new BasicTypes
            {
                Bool = true,
                Int = 42
            });

            static void Assertions(PublicFields result)
            {
                Assert.True(result.Bool);
                Assert.Equal(42, result.Int);
            };

            Assertions(create.Object().ToNew<PublicFields>());
            Assertions(create.Array().SelectNew<PublicFields>().First());
            Assertions(create.Queryable().SelectNew<PublicFields>().First());
        }
    }
}
