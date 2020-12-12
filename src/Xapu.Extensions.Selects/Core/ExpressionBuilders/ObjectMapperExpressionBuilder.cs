using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xapu.Extensions.Selects.Core.Base;

namespace Xapu.Extensions.Selects.Core.ExpressionBuilders
{
    internal class ObjectMapperExpressionBuilder : IMapperExpressionBuilder
    {
        private readonly IMapperExpressionBuilderContext _ctx;

        public ObjectMapperExpressionBuilder(IMapperExpressionBuilderContext ctx)
        {
            _ctx = ctx;
        }

        public Expression Build(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            // We'll have a single instance of this class during the entire expression building process.
            // We cannot have instance variables and have to pass state down as arguments.

            var resultObject = BuildResultNewExpression(sourceLocalName, sourceType, resultType);

            return _ctx.ResolveNullGuard(sourceLocalName, resultObject, sourceType, resultType);
        }

        private Expression BuildResultNewExpression(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            var sourceProps = sourceType.GetReadableProperties();
            var resultProps = resultType.GetWritableProperties();

            var newExpression = Expression.New(resultType);
            var memberInitList = BuildMemberInitList(sourceLocalName, sourceProps, resultProps);
            return Expression.MemberInit(newExpression, memberInitList);
        }

        private IEnumerable<MemberBinding> BuildMemberInitList(Expression sourceLocalName, IEnumerable<PropertyInfo> sourceProps, IEnumerable<PropertyInfo> resultProps)
        {
            var sourcePropNames = sourceProps.Select(p => p.Name);
            var resultPropNames = resultProps.Select(p => p.Name);
            var commonPropNames = resultPropNames.Intersect(sourcePropNames);

            foreach (var propName in commonPropNames)
            {
                var sourcePropInfo = sourceProps.First(p => p.Name == propName);
                var sourcePropExpr = Expression.Property(sourceLocalName, sourcePropInfo);

                var resultPropInfo = resultProps.First(p => p.Name == propName);
                var resultValueExpr = _ctx.CreateExpression(sourcePropExpr, sourcePropInfo.PropertyType, resultPropInfo.PropertyType);

                yield return Expression.Bind(resultPropInfo, resultValueExpr);
            }
        }
    }
}
