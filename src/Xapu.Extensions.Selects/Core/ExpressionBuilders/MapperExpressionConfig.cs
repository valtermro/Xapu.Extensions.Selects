namespace Xapu.Extensions.Selects
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