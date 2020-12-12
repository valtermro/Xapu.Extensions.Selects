using System;
using System.Linq;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests
{
    public class ObjectTypeMapping
    {
        [Fact]
        public void ShallowObject()
        {
            var source = new ObjectType
            {
                Id = Guid.NewGuid(),
                Parent = new ObjectType
                {
                    Id = Guid.NewGuid(),
                    Parent = new ObjectType()
                }
            };

            RunAssertions(source.ToNew<ShallowObjectView>());
            RunAssertions(new[] { source }.SelectNew<ShallowObjectView>().First());
            RunAssertions(new[] { source }.AsQueryable().SelectNew<ShallowObjectView>().First());

            void RunAssertions(ShallowObjectView result)
            {
                Assert.Equal(source.Id, result.Id);
                Assert.Equal(source.Parent.Id, result.Parent.Id);
            };
        }

        [Fact]
        public void NestedObject()
        {
            var source = new ObjectType
            {
                Id = Guid.NewGuid(),
                Parent = new ObjectType
                {
                    Id = Guid.NewGuid(),
                    Parent = new ObjectType
                    {
                        Id = Guid.NewGuid(),
                        Parent = new ObjectType()
                    }
                }
            };

            RunAssertions(source.ToNew<NestedObjectView>());
            RunAssertions(new[] { source }.SelectNew<NestedObjectView>().First());
            RunAssertions(new[] { source }.AsQueryable().SelectNew<NestedObjectView>().First());

            void RunAssertions(NestedObjectView result)
            {
                Assert.Equal(source.Id, result.Id);
                Assert.Equal(source.Parent.Id, result.Parent.Id);
                Assert.Equal(source.Parent.Parent.Id, result.Parent.Parent.Id);
            };
        }
    }
}
