namespace Xapu.Extensions.Selects.Core.Mappers
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