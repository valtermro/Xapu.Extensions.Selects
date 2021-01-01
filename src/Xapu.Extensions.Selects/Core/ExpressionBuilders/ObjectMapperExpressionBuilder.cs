using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects
{
    internal class ObjectMapperExpressionBuilder
    {
        private readonly IMapperExpressionBuilderContext _ctx;

        public ObjectMapperExpressionBuilder(IMapperExpressionBuilderContext ctx)
        {
            _ctx = ctx;
        }

        public Expression Build(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            var resultObject = BuildResultNewExpression(sourceLocalName, sourceType, resultType);

            return _ctx.ResolveNullGuard(sourceLocalName, resultObject, sourceType, resultType);
        }

        private Expression BuildResultNewExpression(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            var sourceMembers = sourceType.GetReadableMembers();
            var resultMembers = resultType.GetWritableMembers();

            var newExpression = Expression.New(resultType);
            var memberInitList = BuildMemberInitList(sourceLocalName, sourceMembers, resultMembers);
            return Expression.MemberInit(newExpression, memberInitList);
        }

        private IEnumerable<MemberBinding> BuildMemberInitList(Expression sourceLocalName, IEnumerable<ITypeMemberInfo> sourceMembers, IEnumerable<ITypeMemberInfo> resultMembers)
        {
            var sourceMemberNames = sourceMembers.Select(p => p.Name);
            var resultMemberNames = resultMembers.Select(p => p.Name);
            var commonMemberNames = resultMemberNames.Intersect(sourceMemberNames);

            foreach (var memberName in commonMemberNames)
            {
                var sourceMemberInfo = sourceMembers.First(p => p.Name == memberName);
                var sourceMemberExpr = ResolveSourceMemberExpression(sourceLocalName, sourceMemberInfo);

                var resultMemberInfo = resultMembers.First(p => p.Name == memberName);
                var resultValueExpr = _ctx.CreateExpression(sourceMemberExpr, sourceMemberInfo.Type, resultMemberInfo.Type);

                yield return Expression.Bind(resultMemberInfo.OriginalInfo, resultValueExpr);
            }
        }

        private static Expression ResolveSourceMemberExpression(Expression sourceLocalName, ITypeMemberInfo sourceMemberInfo)
        {
            return sourceMemberInfo.Kind switch
            {
                TypeMemberInfoKind.Field => Expression.Field(sourceLocalName, sourceMemberInfo.Name),
                TypeMemberInfoKind.Property => Expression.Property(sourceLocalName, sourceMemberInfo.Name),
                _ => default
            };
        }
    }
}
