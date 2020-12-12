using System;

namespace Xapu.Extensions.Selects.Tests.FixtureTypes
{
    public enum StubEnum
    {
        One = 1,
        Two = 2
    }

    public class BasicTypes
    {
        public Guid Guid { get; set; }
        public DateTime DateTime { get; set; }
        public StubEnum Enum { get; set; }
        public bool Bool { get; set; }
        public char Char { get; set; }
        public string String { get; set; }
        public byte Byte { get; set; }
        public decimal Decimal { get; set; }
        public double Double { get; set; }
        public float Float { get; set; }
        public int Int { get; set; }
        public long Long { get; set; }
        public object Object { get; set; }
        public sbyte Sbyte { get; set; }
        public short Short { get; set; }
        public uint Uint { get; set; }
        public ulong Ulong { get; set; }
        public ushort Ushort { get; set; }
    }

    public class BasicTypesView
    {
        public Guid Guid { get; set; }
        public DateTime DateTime { get; set; }
        public StubEnum Enum { get; set; }
        public bool Bool { get; set; }
        public char Char { get; set; }
        public string String { get; set; }
        public byte Byte { get; set; }
        public decimal Decimal { get; set; }
        public double Double { get; set; }
        public float Float { get; set; }
        public int Int { get; set; }
        public long Long { get; set; }
        public object Object { get; set; }
        public sbyte Sbyte { get; set; }
        public short Short { get; set; }
        public uint Uint { get; set; }
        public ulong Ulong { get; set; }
        public ushort Ushort { get; set; }
    }
}
