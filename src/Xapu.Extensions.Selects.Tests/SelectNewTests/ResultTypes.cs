using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectNewTests
{
    public class ResultTypes
    {
        [Fact]
        public void EnumerableSelectReturnsIEnumerable()
        {
            var array = Creator.New(() => new BasicTypes()).Array();
            
            var result = array.SelectNew<BasicTypes>();

            AssertX.AssignableFrom(result, typeof(IEnumerable<BasicTypes>));
        }

        [Fact]
        public void QueryableSelectReturnsIQueryable()
        {
            var query = Creator.New(() => new BasicTypes()).Queryable();

            var result = query.SelectNew<BasicTypes>();

            AssertX.AssignableFrom(result, typeof(IQueryable<BasicTypes>));
        }
    }
}
