using System;
using System.Linq;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectNewTests
{
    public class NullGuards
    {
        [Fact]
        public void ObjectSelectGuardsNullObjects()
        {
            var create = Creator.New(() => new ObjectType
            {
                Id = 1,
                Parent = null
            });

            var result = AssertX.NotThrowing(() => create.Object().ToNew<ShallowObjectView>());
            Assert.Null(result.Parent);
        }

        [Fact]
        public void ObjectSelectGuardsNullCollections()
        {
            var create = Creator.New(() => new CollectionsOfBasicValues
            {
                IntArray = null
            });

            var result = AssertX.NotThrowing(() => create.Object().ToNew<CollectionsOfBasicValues>());
            Assert.Null(result.IntArray);
        }

        [Fact]
        public void EnumerableSelectGuardsNullObjects()
        {
            var create = Creator.New(() => new ObjectType
            {
                Id = 1,
                Parent = null
            });

            var result = AssertX.NotThrowing(() => create.Array().SelectNew<ShallowObjectView>().First());
            Assert.Null(result.Parent);
        }

        [Fact]
        public void EnumerableSelectGuardsNullCollections()
        {
            var create = Creator.New(() => new CollectionsOfBasicValues
            {
                IntArray = null
            });

            var result = AssertX.NotThrowing(() => create.Array().SelectNew<CollectionsOfBasicValues>().First());
            Assert.Null(result.IntArray);
        }

        [Fact]
        public void QueryableSelectDoesNotGuardNullObjects()
        {
            var create = Creator.New(() => new ObjectType
            {
                Id = 1,
                Parent = null
            });

            Assert.Throws<NullReferenceException>(() => create.Queryable().SelectNew<ShallowObjectView>().First());
        }

        [Fact]
        public void QueryableSelectDoesNotGuardNullCollections()
        {
            var create = Creator.New(() => new CollectionsOfBasicValues
            {
                IntArray = null
            });

            Assert.Throws<ArgumentNullException>(() => create.Queryable().SelectNew<CollectionsOfBasicValues>().First());
        }
    }
}
