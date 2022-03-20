using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests.Core
{
	public class StringIntFieldsTests
	{
		[Fact]
		public void Create_ClassWithNoOverrides_ReturnsDefault()
		{
			FubberBuilder<StringIntFields.Class> builder = new();
			Fubber<StringIntFields.Class> fubber = builder.Build();

			StringIntFields.Class fub = fubber.Fub();

			Assert.NotNull(fub.@string);
			Assert.Equal(string.Empty, fub.@string);
			Assert.Equal(default, fub.integer);
		}

		[Fact]
		public void Create_StructWithNoOverrides_ReturnsDefault()
		{
			FubberBuilder<StringIntFields.Struct> builder = new();
			Fubber<StringIntFields.Struct> fubber = builder.Build();

			StringIntFields.Struct fub = fubber.Fub();

			Assert.Equal(string.Empty, fub.@string);
			Assert.Equal(default, fub.integer);
		}

		[Fact]
		public void Create_StructWithConstructorWithNoOverrides_ReturnsDefault()
		{
			FubberBuilder<StringIntFields.StructWithConstructor> builder = new();
			Fubber<StringIntFields.StructWithConstructor> fubber = builder.Build();

			StringIntFields.StructWithConstructor fub = fubber.Fub();

			Assert.NotNull(fub.@string);
			Assert.Equal(string.Empty, fub.@string);
			Assert.Equal(default, fub.integer);
		}

		[Fact]
		public void Create_ClassWithoutConstructorWithNoOverrides_ReturnsDefault()
		{
			FubberBuilder<StringIntFields.ClassWithoutConstructor> builder = new();
			Fubber<StringIntFields.ClassWithoutConstructor> fubber = builder.Build();

			StringIntFields.ClassWithoutConstructor fub = fubber.Fub();

			Assert.Null(fub.@string);
			Assert.Equal(default, fub.integer);
		}

		[Fact]
		public void Create_ClassWithTwoOverrides_ReturnsFub()
		{
			FubberBuilder<StringIntFields.Class> builder = new();
			Fubber<StringIntFields.Class> fubber = builder.Build();

			StringIntFields.Class fub = fubber.Fub(f => f.@string, "Falafel", f => f.integer, 4096);

			Assert.Equal("Falafel", fub.@string);
			Assert.Equal(4096, fub.integer);
		}

		[Fact]
		public void Create_StructWithTwoOverrides_ReturnsFub()
		{
			FubberBuilder<StringIntFields.Struct> builder = new();
			Fubber<StringIntFields.Struct> fubber = builder.Build();

			StringIntFields.Struct fub = fubber.Fub(f => f.@string, "Falafel", f => f.integer, 4096);

			Assert.Equal("Falafel", fub.@string);
			Assert.Equal(4096, fub.integer);
		}
	}
}
