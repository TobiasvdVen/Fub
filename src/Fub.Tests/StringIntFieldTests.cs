using Xunit;

namespace Fub.Tests
{
	public class StringIntFieldTests
	{
		public class Class
		{
			public string @string;
			public int integer;

			public Class(string @string, int integer)
			{
				this.@string = @string;
				this.integer = integer;
			}
		}

		public struct Struct
		{
			public string @string;
			public int integer;
		}

		public struct StructWithConstructor
		{
			public string @string;
			public int integer;

			public StructWithConstructor(string @string, int integer)
			{
				this.@string = @string;
				this.integer = integer;
			}
		}

		public class ClassWithoutConstructor
		{
			public string? @string;
			public int integer;
		}

		[Fact]
		public void Create_ClassWithNoOverrides_ReturnsDefault()
		{
			FubberBuilder<Class> builder = new();
			Fubber<Class> fubber = builder.Build();

			Class fub = fubber.Fub();

			Assert.NotNull(fub.@string);
			Assert.Equal(string.Empty, fub.@string);
			Assert.Equal(default, fub.integer);
		}

		[Fact]
		public void Create_StructWithNoOverrides_ReturnsDefault()
		{
			FubberBuilder<Struct> builder = new();
			Fubber<Struct> fubber = builder.Build();

			Struct fub = fubber.Fub();

			Assert.Null(fub.@string);
			Assert.Equal(default, fub.integer);
		}

		[Fact]
		public void Create_StructWithConstructorWithNoOverrides_ReturnsDefault()
		{
			FubberBuilder<StructWithConstructor> builder = new();
			Fubber<StructWithConstructor> fubber = builder.Build();

			StructWithConstructor fub = fubber.Fub();

			Assert.NotNull(fub.@string);
			Assert.Equal(string.Empty, fub.@string);
			Assert.Equal(default, fub.integer);
		}

		[Fact]
		public void Create_ClassWithoutConstructorWithNoOverrides_ReturnsDefault()
		{
			FubberBuilder<ClassWithoutConstructor> builder = new();
			Fubber<ClassWithoutConstructor> fubber = builder.Build();

			ClassWithoutConstructor fub = fubber.Fub();

			Assert.Null(fub.@string);
			Assert.Equal(default, fub.integer);
		}
	}
}
