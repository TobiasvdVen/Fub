using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests
{
	public class NestedTypesTests
	{
		public class HasEmptyNestedType
		{
			public HasEmptyNestedType(Empty.Class empty)
			{
				Empty = empty;
			}

			public Empty.Class Empty { get; }
		}

		[Fact]
		public void Create_HasEmptyNestedType_ReturnsDefault()
		{
			FubberBuilder<HasEmptyNestedType> builder = new();
			Fubber<HasEmptyNestedType> fubber = builder.Build();

			HasEmptyNestedType fub = fubber.Fub();

			Assert.NotNull(fub.Empty);
		}

		public class Simple
		{
			public Simple(string name, bool boolean, Empty.Class empty)
			{
				Name = name;
				Boolean = boolean;
				Empty = empty;
			}

			public string Name { get; }
			public bool Boolean { get; }
			public Empty.Class Empty { get; }
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
			FubberBuilder<HasSimpleNestedType> builder = new();
			Fubber<HasSimpleNestedType> fubber = builder.Build();

			HasSimpleNestedType fub = fubber.Fub();

			Assert.Equal(string.Empty, fub.Simple.Name);
			Assert.Equal(default, fub.Simple.Boolean);
			Assert.NotNull(fub.Simple.Empty);
		}

		[Fact]
		public void Create_HasSimpleNestedType_WithSimpleOverride_ReturnsFub()
		{
			FubberBuilder<HasSimpleNestedType> builder = new();
			Fubber<HasSimpleNestedType> fubber = builder.Build();

			HasSimpleNestedType fub = fubber.Fub(h => h.Simple, new Simple("Barney", true, new Empty.Class()));

			Assert.Equal("Barney", fub.Simple.Name);
			Assert.Equal(true, fub.Simple.Boolean);
		}
	}
}
