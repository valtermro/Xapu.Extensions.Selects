using System.Collections.Generic;
using System.Linq;
using Xapu.Extensions.Selects.Tests.Base;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectNewTests
{
    public class HandlingIterators
    {
        [Fact]
        public void EnumerableLinqIterators()
        {
            var array = Creator.New(() => new ObjectType { Id = 1 }).Array();

            static void Assertions(IEnumerable<object> enumerable)
            {
                var result = enumerable.SelectNew<WithIdView>().First();

                Assert.IsType<WithIdView>(result);
                Assert.Equal(1, result.Id);
            };

            Assertions(array.Select(p => new { p.Id }));
            Assertions(array.Where(p => p.Parent == null));
            Assertions(array.OrderBy(p => p.Id));
        }

        [Fact]
        public void QueryableLinqIterators()
        {
            var query = Creator.New(() => new ObjectType { Id = 1 }).Queryable();

            static void Assertions(IQueryable<object> enumerable)
            {
                var result = enumerable.SelectNew<WithIdView>().First();
                
                Assert.IsType<WithIdView>(result);
                Assert.Equal(1, result.Id);
            };

            Assertions(query.Select(p => new { p.Id }));
            Assertions(query.Where(p => p.Parent == null));
            Assertions(query.OrderBy(p => p.Id));
        }
    }
}
