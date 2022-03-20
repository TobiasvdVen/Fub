using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests.Core
{
	public class StringIntTests
	{
		[Fact]
		public void Create_ClassWithNoOverrides_ReturnsDefault()
		{
			Create_WithNoOverrides_ReturnsDefault<StringInt.Class>();
		}

		[Fact]
		public void Create_StructWithNoOverrides_ReturnsDefault()
		{
			Create_WithNoOverrides_ReturnsDefault<StringInt.Struct>();
		}

#if NET5_0_OR_GREATER
		[Fact]
		public void Create_RecordWithNoOverrides_ReturnsDefault()
		{
			Create_WithNoOverrides_ReturnsDefault<StringInt.Record>();
		}

		[Fact]
		public void Create_RecordStructWithNoOverrides_ReturnsDefault()
		{
			Create_WithNoOverrides_ReturnsDefault<StringInt.RecordStruct>();
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

		[Fact]
		public void Create_ClassWithTwoOverrides_ReturnsDefault()
		{
			Create_WithTwoOverrides_ReturnsFub<StringInt.Class>();
		}

		[Fact]
		public void Create_StructWithTwoOverrides_ReturnsDefault()
		{
			Create_WithTwoOverrides_ReturnsFub<StringInt.Struct>();
		}

#if NET5_0_OR_GREATER
		[Fact]
		public void Create_RecordWithTwoOverrides_ReturnsDefault()
		{
			Create_WithTwoOverrides_ReturnsFub<StringInt.Record>();
		}

		[Fact]
		public void Create_RecordStructWithTwoOverrides_ReturnsDefault()
		{
			Create_WithTwoOverrides_ReturnsFub<StringInt.RecordStruct>();
		}
#endif

		private void Create_WithTwoOverrides_ReturnsFub<T>() where T : IStringInt
		{
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IStringInt fub = fubber.Fub(f => f.Integer, 64, f => f.String, "Hogwarts");

			Assert.Equal("Hogwarts", fub.String);
			Assert.Equal(64, fub.Integer);
		}

		[Fact]
		public void Create_StructWithNoConstructorWithNoOverrides_ReturnsDefault()
		{
			FubberBuilder<StringInt.StructWithNoConstructor> builder = new();
			Fubber<StringInt.StructWithNoConstructor> fubber = builder.Build();

			StringInt.StructWithNoConstructor fub = fubber.Fub();

			// Non-nullable reference types in structs are still initialized to null when not explicitly initialized, and this is not prevented by the nullability analyzer
			// This is a weird case where the properties are get only
			// It makes no sense to do in practice, as far as I know, but this test records the unexpected behavior
			// See: https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references#structs
			Assert.Null(fub.String);
			Assert.Equal(default, fub.Integer);
		}
	}
}
