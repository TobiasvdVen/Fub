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
			FubBuilder<Class> builder = new();
			Fub<Class> fub = builder.Build();

			Class created = fub.Create();

			Assert.NotNull(created.@string);
			Assert.Equal(string.Empty, created.@string);
			Assert.Equal(default, created.integer);
		}

		[Fact]
		public void Create_StructWithNoOverrides_ReturnsDefault()
		{
			FubBuilder<Struct> builder = new();
			Fub<Struct> fub = builder.Build();

			Struct created = fub.Create();

			Assert.Null(created.@string);
			Assert.Equal(default, created.integer);
		}

		[Fact]
		public void Create_StructWithConstructorWithNoOverrides_ReturnsDefault()
		{
			FubBuilder<StructWithConstructor> builder = new();
			Fub<StructWithConstructor> fub = builder.Build();

			StructWithConstructor created = fub.Create();

			Assert.NotNull(created.@string);
			Assert.Equal(string.Empty, created.@string);
			Assert.Equal(default, created.integer);
		}

		[Fact]
		public void Create_ClassWithoutConstructorWithNoOverrides_ReturnsDefault()
		{
			FubBuilder<ClassWithoutConstructor> builder = new();
			Fub<ClassWithoutConstructor> fub = builder.Build();

			ClassWithoutConstructor created = fub.Create();

			Assert.Null(created.@string);
			Assert.Equal(default, created.integer);
		}
	}
}
