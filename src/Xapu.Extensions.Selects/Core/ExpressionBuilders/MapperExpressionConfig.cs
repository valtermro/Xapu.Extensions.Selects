namespace Xapu.Extensions.Selects.Core.ExpressionBuilders
{
    public class MapperExpressionConfig
    {
        public bool GuardNull { get; }

        public MapperExpressionConfig(bool guardNull)
        {
            GuardNull = guardNull;
        }
    }
}