namespace Xapu.Extensions.Selects.Core
{
    internal class MapperExpressionConfig
    {
        public bool GuardNull { get; }

        public MapperExpressionConfig(bool guardNull)
        {
            GuardNull = guardNull;
        }
    }
}
