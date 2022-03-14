using Xunit;

namespace Fub.Tests
{
	public class StringIntTests
	{
		public interface IStringInt
		{
			string String { get; }
			int Integer { get; }
		}

		public class Class : IStringInt
		{
			public Class(string @string, int integer)
			{
				String = @string;
				Integer = integer;
			}

			public string String { get; }
			public int Integer { get; }
		}

		public struct Struct : IStringInt
		{
			public Struct(string @string, int integer)
			{
				String = @string;
				Integer = integer;
			}

			public string String { get; }
			public int Integer { get; }
		}

		[Fact]
		public void Create_ClassWithNoOverrides_ReturnsDefault()
		{
			Create_WithNoOverrides_ReturnsDefault<Class>();
		}

		[Fact]
		public void Create_StructWithNoOverrides_ReturnsDefault()
		{
			Create_WithNoOverrides_ReturnsDefault<Struct>();
		}

#if NET5_0_OR_GREATER
		public record Record(string String, int Integer) : IStringInt
		{

		}

		public record struct RecordStruct(string String, int Integer) : IStringInt
		{

		}

		[Fact]
		public void Create_RecordWithNoOverrides_ReturnsDefault()
		{
			Create_WithNoOverrides_ReturnsDefault<Record>();
		}

		[Fact]
		public void Create_RecordStructWithNoOverrides_ReturnsDefault()
		{
			Create_WithNoOverrides_ReturnsDefault<RecordStruct>();
		}
#endif

		private void Create_WithNoOverrides_ReturnsDefault<T>() where T : IStringInt
		{
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IStringInt fub = fubber.Fub();

			Assert.NotNull(fub.String);
			Assert.Equal(string.Empty, fub.String);
			Assert.Equal(default, fub.Integer);
		}

		public struct StructWithNoConstructor : IStringInt
		{
			public string String { get; }
			public int Integer { get; }
		}

		[Fact]
		public void Create_StructWithNoConstructorWithNoOverrides_ReturnsDefault()
		{
			FubberBuilder<StructWithNoConstructor> builder = new();
			Fubber<StructWithNoConstructor> fubber = builder.Build();

			StructWithNoConstructor fub = fubber.Fub();

			// Non-nullable reference types in structs are still initialized to null when not explicitly initialized, and this is not prevented by the nullability analyzer
			// See: https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references#structs
			Assert.Null(fub.String);
			Assert.Equal(default, fub.Integer);
		}
	}
}
