using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Xapu.Extensions.Selects.Core
{
    internal class ObjectMapperExpressionBuilder : ITypeMapperExpressionBuilder
    {
        private readonly IMapperExpressionBuilderContext _ctx;

        public ObjectMapperExpressionBuilder(IMapperExpressionBuilderContext ctx)
        {
            _ctx = ctx;
        }

        public Expression Build(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            var sourceProps = sourceType.GetReadableProperties();
            var resultProps = resultType.GetWritableProperties();

            var newExpression = Expression.New(resultType);
            var memberInitList = BuildMemberInitList(sourceLocalName, sourceProps, resultProps);

            var memeberInitExpression = Expression.MemberInit(newExpression, memberInitList);
            return memeberInitExpression;
        }

        private IEnumerable<MemberBinding> BuildMemberInitList(Expression sourceLocalName, IEnumerable<PropertyInfo> sourceProps, IEnumerable<PropertyInfo> resultProps)
        {
            var sourcePropNames = sourceProps.Select(p => p.Name);
            var resultPropNames = resultProps.Select(p => p.Name);
            var commonPropNames = resultPropNames.Intersect(sourcePropNames);

            foreach (var propName in commonPropNames)
            {
                var sourceProp = sourceProps.First(p => p.Name == propName);
                var resultProp = resultProps.First(p => p.Name == propName);

                var sourceNameExpression = Expression.Property(sourceLocalName, sourceProp);
                var valueExpression = _ctx.CreateExpression(sourceNameExpression, sourceProp.PropertyType, resultProp.PropertyType);

                var bindExpression = Expression.Bind(resultProp, valueExpression);
                yield return bindExpression;
            }
        }
    }
}
