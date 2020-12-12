using System;
using System.Collections.Generic;
using System.Linq;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests
{
    public class HandlingIterators
    {
        [Fact]
        public void EnumerableLinqIterators()
        {
            var source = new[]
            {
                new ObjectType { Id = Guid.NewGuid() }
            };

            RunAssertions(source.Select(p => new { p.Id }));
            RunAssertions(source.Where(p => p.Parent == null));
            RunAssertions(source.OrderBy(p => p.Id));

            void RunAssertions(IEnumerable<object> query)
            {
                var result = query.SelectNew<WithIdView>().First();

                Assert.IsType<WithIdView>(result);
                Assert.Equal(source.First().Id, result.Id);
            };
        }

        [Fact]
        public void QueryableLinqIterators()
        {
            var source = new[]
            {
                new ObjectType { Id = Guid.NewGuid() }
            };
            var query = source.AsQueryable();

            RunAssertions(query.Select(p => new { p.Id }));
            RunAssertions(query.Where(p => p.Parent == null));
            RunAssertions(query.OrderBy(p => p.Id));

            void RunAssertions(IQueryable<object> query)
            {
                var result = query.SelectNew<WithIdView>().First();
                
                Assert.IsType<WithIdView>(result);
                Assert.Equal(source.First().Id, result.Id);
            };
        }
    }
}
