using Xunit;

namespace Fub.Tests
{
	public class NestedTypesTests
	{
		public class Empty
		{

		}

		public class HasEmptyNestedType
		{
			public HasEmptyNestedType(Empty empty)
			{
				Empty = empty;
			}

			public Empty Empty { get; }
		}

		[Fact]
		public void Create_HasEmptyNestedType_ReturnsDefault()
		{
			FubBuilder<HasEmptyNestedType> builder = new();
			Fub<HasEmptyNestedType> fub = builder.Build();

			HasEmptyNestedType created = fub.Create();

			Assert.NotNull(created.Empty);
		}

		public class Simple
		{
			public Simple(string name, bool boolean, Empty empty)
			{
				Name = name;
				Boolean = boolean;
				Empty = empty;
			}

			public string Name { get; }
			public bool Boolean { get; }
			public Empty Empty { get; }
		}

		public class HasSimpleNestedType
		{
			public HasSimpleNestedType(Simple simple)
			{
				Simple = simple;
			}

			public Simple Simple { get; }
		}

		[Fact]
		public void Create_HasSimpleNestedType_WithNoOverrides_ReturnsDefault()
		{
			FubBuilder<HasSimpleNestedType> builder = new();
			Fub<HasSimpleNestedType> fub = builder.Build();

			HasSimpleNestedType created = fub.Create();

			Assert.Equal(string.Empty, created.Simple.Name);
			Assert.Equal(default, created.Simple.Boolean);
			Assert.NotNull(created.Simple.Empty);
		}
	}
}
