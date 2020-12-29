namespace Xapu.Extensions.Selects.Tests.FixtureTypes
{
    public class PublicFields
    {
        public bool Bool;
        public int Int;
    }

    public class PublicFieldsView
    {
        public bool Bool;
        public int Int;
    }

    public class PrivateFields
    {
        private bool Bool;
        private int Int;

        public PrivateFields() { }
        public PrivateFields(bool b, int i) => (Bool, Int) = (b, i);

        public bool GetBool() => Bool;
        public int GetInt() => Int;
    }
}
