using System.Collections.Generic;
using System.Linq;
using Xapu.Extensions.Selects.Tests.Base;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests
{
    public class ResultTypes
    {
        [Fact]
        public void EnumerableSelectReturnsIEnumerable()
        {
            var source = new BasicTypes[] { new BasicTypes() };
            var result = source.SelectNew<BasicTypes>();

            AssertX.AssignableFrom(result, typeof(IEnumerable<BasicTypes>));
        }

        [Fact]
        public void QueryableSelectReturnsIQueryable()
        {
            var query = new BasicTypes[] { new BasicTypes() }.AsQueryable();
            var result = query.SelectNew<BasicTypes>();

            AssertX.AssignableFrom(result, typeof(IQueryable<BasicTypes>));
        }
    }
}
