namespace Fub.Tests.Models
{
	public interface INullableBuiltInTypes
	{
		bool? Bool { get; }
		byte? Byte { get; }
		sbyte? SByte { get; }
		char? Char { get; }
		decimal? Decimal { get; }
		double? Double { get; }
		float? Float { get; }
		int? Int { get; }
		uint? UInt { get; }
		nint? NInt { get; }
		nuint? NUInt { get; }
		long? Long { get; }
		ulong? ULong { get; }
		short? Short { get; }
		ushort? UShort { get; }
		object? Object { get; }
		string? String { get; }
	}

	public static class NullableBuiltInTypes
	{
		public class Class : INullableBuiltInTypes
		{
			public bool? Bool { get; }
			public byte? Byte { get; }
			public sbyte? SByte { get; }
			public char? Char { get; }
			public decimal? Decimal { get; }
			public double? Double { get; }
			public float? Float { get; }
			public int? Int { get; }
			public uint? UInt { get; }
			public nint? NInt { get; }
			public nuint? NUInt { get; }
			public long? Long { get; }
			public ulong? ULong { get; }
			public short? Short { get; }
			public ushort? UShort { get; }
			public object? Object { get; }
			public string? String { get; }
		}

		public class MutableClass : INullableBuiltInTypes
		{
			public bool? Bool { get; set; }
			public byte? Byte { get; set; }
			public sbyte? SByte { get; set; }
			public char? Char { get; set; }
			public decimal? Decimal { get; set; }
			public double? Double { get; set; }
			public float? Float { get; set; }
			public int? Int { get; set; }
			public uint? UInt { get; set; }
			public nint? NInt { get; set; }
			public nuint? NUInt { get; set; }
			public long? Long { get; set; }
			public ulong? ULong { get; set; }
			public short? Short { get; set; }
			public ushort? UShort { get; set; }
			public object? Object { get; set; }
			public string? String { get; set; }
		}

		public class ClassWithConstructor : INullableBuiltInTypes
		{
			public ClassWithConstructor(bool? @bool, byte? @byte, sbyte? sByte, char? @char, decimal? @decimal, double? @double, float? @float, int? @int, uint? uInt, nint? nInt, nuint? nUInt, long? @long, ulong? uLong, short? @short, ushort? uShort, object? @object, string? @string)
			{
				Bool = @bool;
				Byte = @byte;
				SByte = sByte;
				Char = @char;
				Decimal = @decimal;
				Double = @double;
				Float = @float;
				Int = @int;
				UInt = uInt;
				NInt = nInt;
				NUInt = nUInt;
				Long = @long;
				ULong = uLong;
				Short = @short;
				UShort = uShort;
				Object = @object;
				String = @string;
			}

			public bool? Bool { get; }
			public byte? Byte { get; }
			public sbyte? SByte { get; }
			public char? Char { get; }
			public decimal? Decimal { get; }
			public double? Double { get; }
			public float? Float { get; }
			public int? Int { get; }
			public uint? UInt { get; }
			public nint? NInt { get; }
			public nuint? NUInt { get; }
			public long? Long { get; }
			public ulong? ULong { get; }
			public short? Short { get; }
			public ushort? UShort { get; }
			public object? Object { get; }
			public string? String { get; }
		}

#if NET5_0_OR_GREATER
		public record Record(bool? Bool, byte? Byte, sbyte? SByte, char? Char, decimal? Decimal, double? Double, float? Float, int? Int, uint? UInt, nint? NInt, nuint? NUInt, long? Long, ulong? ULong, short? Short, ushort? UShort, object? Object, string? String) : INullableBuiltInTypes
		{

		}

		public record struct RecordStruct(bool? Bool, byte? Byte, sbyte? SByte, char? Char, decimal? Decimal, double? Double, float? Float, int? Int, uint? UInt, nint? NInt, nuint? NUInt, long? Long, ulong? ULong, short? Short, ushort? UShort, object? Object, string? String) : INullableBuiltInTypes
		{

		}
#endif
	}
}
