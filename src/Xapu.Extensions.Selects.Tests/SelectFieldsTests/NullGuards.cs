using System;
using System.Linq;
using Xapu.Extensions.Selects.Tests.Base;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectFieldsTests
{
    public class NullGuards
    {
        [Fact]
        public void EnumerableSelectGuardsNullObjects()
        {
            var source = Creator.New(() => new ObjectType
            {
                Id = 1,
                Parent = null
            });

            AssertX.NotThrowing(() => source.Array().SelectFields("Parent.Id").First());
        }

        [Fact]
        public void EnumerableSelectGuardsNullCollections()
        {
            var source =  Creator.New(() => new CollectionsOfObjects
            {
                ObjectArray = null
            });

            AssertX.NotThrowing(() => source.Array().SelectFields("ObjectArray.Id").First());
        }

        [Fact]
        public void QueryableSelectDoesNotGuardNullObjects()
        {
            var create = Creator.New(() => new ObjectType
            {
                Id = 1,
                Parent = null
            });
            
            Assert.Throws<NullReferenceException>(() => create.Queryable().SelectFields("Parent.Id").First());
        }

        [Fact]
        public void QueryableSelectDoesNotGuardNullCollections()
        {
            var create = Creator.New(() => new CollectionsOfObjects
            {
                ObjectArray = null
            });

            Assert.Throws<ArgumentNullException>(() => create.Queryable().SelectFields("ObjectArray.Id").First());
        }
    }
}
