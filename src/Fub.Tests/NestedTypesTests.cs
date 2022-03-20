using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests
{
	public class NestedTypesTests
	{
		[Fact]
		public void Create_ClassHasEmpty_ReturnsDefault()
		{
			Create_HasEmpty_ReturnsDefault<HasEmpty.Class>();
		}

		[Fact]
		public void Create_StructHasEmpty_ReturnsDefault()
		{
			Create_HasEmpty_ReturnsDefault<HasEmpty.Struct>();
		}

		private void Create_HasEmpty_ReturnsDefault<T>() where T : IHasEmpty
		{
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IHasEmpty fub = fubber.Fub();

			Assert.NotNull(fub.Empty);
		}

		public class HasSimpleComposite
		{
			public HasSimpleComposite(SimpleComposite.Class simple)
			{
				Simple = simple;
			}

			public SimpleComposite.Class Simple { get; }
		}

		[Fact]
		public void Create_HasSimpleNestedType_WithNoOverrides_ReturnsDefault()
		{
			FubberBuilder<HasSimpleComposite> builder = new();
			Fubber<HasSimpleComposite> fubber = builder.Build();

			HasSimpleComposite fub = fubber.Fub();

			Assert.Equal(string.Empty, fub.Simple.Name);
			Assert.Equal(default, fub.Simple.Boolean);
			Assert.NotNull(fub.Simple.Empty);
		}

		[Fact]
		public void Create_HasSimpleNestedType_WithSimpleOverride_ReturnsFub()
		{
			FubberBuilder<HasSimpleComposite> builder = new();
			Fubber<HasSimpleComposite> fubber = builder.Build();

			HasSimpleComposite fub = fubber.Fub(h => h.Simple, new SimpleComposite.Class("Barney", true, new Empty.Class()));

			Assert.Equal("Barney", fub.Simple.Name);
			Assert.Equal(true, fub.Simple.Boolean);
		}
	}
}
