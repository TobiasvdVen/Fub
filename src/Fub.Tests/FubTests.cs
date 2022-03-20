using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests
{
	public class FubTests
	{
		[Fact]
		public void Fub_WithNoOverrides_ReturnsDefault()
		{
			StringInt.Class fub = Fub<StringInt.Class>.Simple();

			Assert.Equal(0, fub.Integer);
			Assert.Equal(string.Empty, fub.String);
		}

		[Fact]
		public void Fub_WithStringOverride_ReturnsFub()
		{
			StringInt.Class fub = Fub<StringInt.Class>.Simple(b => b.String, "Override");

			Assert.Equal(0, fub.Integer);
			Assert.Equal("Override", fub.String);
		}

		[Fact]
		public void Fub_WithTwoOverrides_ReturnsFub()
		{
			StringInt.Class fub = Fub<StringInt.Class>.Simple(b => b.String, "Apple", b => b.Integer, 16);

			Assert.Equal(16, fub.Integer);
			Assert.Equal("Apple", fub.String);
		}
	}
}
