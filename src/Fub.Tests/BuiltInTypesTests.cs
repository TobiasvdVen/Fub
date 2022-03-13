using System;
using Xunit;

namespace Fub.Tests
{
	public class BuiltInTypesTests
	{
		public interface IBuiltInTypes
		{
			bool Bool { get; }
			byte Byte { get; }
			sbyte SByte { get; }
			char Char { get; }
			decimal Decimal { get; }
			double Double { get; }
			float Float { get; }
			int Int { get; }
			uint UInt { get; }
			nint NInt { get; }
			nuint NUInt { get; }
			long Long { get; }
			ulong ULong { get; }
			short Short { get; }
			ushort UShort { get; }
			object Object { get; }
			string String { get; }
			DateTime DateTime { get; }
		}

		public class Class : IBuiltInTypes
		{
			public Class(bool @bool, byte @byte, sbyte sByte, char @char, decimal @decimal, double @double, float @float, int @int, uint uInt, nint nInt, nuint nUInt, long @long, ulong uLong, short @short, ushort uShort, object @object, string @string, DateTime dateTime)
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
				DateTime = dateTime;
			}

			public bool Bool { get; }
			public byte Byte { get; }
			public sbyte SByte { get; }
			public char Char { get; }
			public decimal Decimal { get; }
			public double Double { get; }
			public float Float { get; }
			public int Int { get; }
			public uint UInt { get; }
			public nint NInt { get; }
			public nuint NUInt { get; }
			public long Long { get; }
			public ulong ULong { get; }
			public short Short { get; }
			public ushort UShort { get; }
			public object Object { get; }
			public string String { get; }
			public DateTime DateTime { get; }
		}

		public class MostlyMutableClass : IBuiltInTypes
		{
			public MostlyMutableClass(object @object, string @string)
			{
				Object = @object;
				String = @string;
			}

			public bool Bool { get; set; }
			public byte Byte { get; set; }
			public sbyte SByte { get; set; }
			public char Char { get; set; }
			public decimal Decimal { get; set; }
			public double Double { get; set; }
			public float Float { get; set; }
			public int Int { get; set; }
			public uint UInt { get; set; }
			public nint NInt { get; set; }
			public nuint NUInt { get; set; }
			public long Long { get; set; }
			public ulong ULong { get; set; }
			public short Short { get; set; }
			public ushort UShort { get; set; }
			public object Object { get; }
			public string String { get; }
			public DateTime DateTime { get; }
		}

		[Fact]
		public void Create_ClassWithNoOverrides_ReturnsDefault()
		{
			CreateAndAssertDefault<Class>();
		}

		[Fact]
		public void Create_MostlyMutableClassWithNoOverrides_ReturnsDefault()
		{
			CreateAndAssertDefault<MostlyMutableClass>();
		}

		private void CreateAndAssertDefault<T>() where T : IBuiltInTypes
		{
			FubBuilder<T> builder = new();
			Fub<T> fub = builder.Build();

			IBuiltInTypes created = fub.Create();

			Assert.Equal(default, created.Bool);
			Assert.Equal(default, created.Byte);
			Assert.Equal(default, created.SByte);
			Assert.Equal(default, created.Char);
			Assert.Equal(default, created.Decimal);
			Assert.Equal(default, created.Double);
			Assert.Equal(default, created.Float);
			Assert.Equal(default, created.Int);
			Assert.Equal(default, created.UInt);
			Assert.Equal(default, created.NInt);
			Assert.Equal(default, created.NUInt);
			Assert.Equal(default, created.Long);
			Assert.Equal(default, created.ULong);
			Assert.Equal(default, created.Short);
			Assert.Equal(default, created.UShort);

			Assert.NotNull(created.Object);

			Assert.NotNull(created.String);
			Assert.Equal(string.Empty, created.String);

			Assert.Equal(default, created.DateTime);
		}
	}
}
