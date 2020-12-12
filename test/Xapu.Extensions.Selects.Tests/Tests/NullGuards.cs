using System;
using System.Linq;
using Xapu.Extensions.Selects.Tests.Base;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests
{
    public class NullGuards
    {
        [Fact]
        public void ObjectSelectGuardsNullObjects()
        {
            var source = new ObjectType
            {
                Id = Guid.NewGuid(),
                Parent = null
            };

            var result = AssertX.NotThrowing(() => source.ToNew<ShallowObjectView>());
            Assert.Null(result.Parent);
        }

        [Fact]
        public void ObjectSelectGuardsNullCollections()
        {
            var source = new CollectionsOfBasicValues
            {
                IntArray = null
            };

            var result = AssertX.NotThrowing(() => source.ToNew<CollectionsOfBasicValues>());
            Assert.Null(result.IntArray);
        }

        [Fact]
        public void EnumerableSelectGuardsNullObjects()
        {
            var source = new[]
            {
                new ObjectType
                {
                    Id = Guid.NewGuid(),
                    Parent = null
                }
            };

            var result = AssertX.NotThrowing(() => source.SelectNew<ShallowObjectView>().First());
            Assert.Null(result.Parent);
        }

        [Fact]
        public void EnumerableSelectGuardsNullCollections()
        {
            var source = new[]
            {
                new CollectionsOfBasicValues
                {
                    IntArray = null
                }
            };

            var result = AssertX.NotThrowing(() => source.SelectNew<CollectionsOfBasicValues>().First());
            Assert.Null(result.IntArray);
        }

        [Fact]
        public void QueryableSelectDoesNotGuardNullObjects()
        {
            var source = new[]
            {
                new ObjectType
                {
                    Id = Guid.NewGuid(),
                    Parent = null
                }
            };
            
            var query = source.AsQueryable();

            Assert.Throws<NullReferenceException>(() => query.SelectNew<ShallowObjectView>().First());
        }

        [Fact]
        public void QueryableSelectDoesNotGuardNullCollections()
        {
            var source = new[]
            {
                new CollectionsOfBasicValues
                {
                    IntArray = null
                }
            };

            var query = source.AsQueryable();

            Assert.Throws<ArgumentNullException>(() => query.SelectNew<CollectionsOfBasicValues>().First());
        }
    }
}
