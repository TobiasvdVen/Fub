using Xunit;

namespace Fub.Tests
{
	public class FubTests
	{
		public class BasicModel
		{
			public BasicModel(int integer, string text)
			{
				Integer = integer;
				Text = text;
			}

			public int Integer { get; }
			public string Text { get; }
		}

		[Fact]
		public void Fub_WithNoOverrides_ReturnsDefault()
		{
			BasicModel fub = Fub<BasicModel>.Simple();

			Assert.Equal(0, fub.Integer);
			Assert.Equal(string.Empty, fub.Text);
		}

		[Fact]
		public void Fub_WithStringOverride_ReturnsFub()
		{
			BasicModel fub = Fub<BasicModel>.Simple(b => b.Text, "Override");

			Assert.Equal(0, fub.Integer);
			Assert.Equal("Override", fub.Text);
		}

		[Fact]
		public void Fub_WithTwoOverrides_ReturnsFub()
		{
			BasicModel fub = Fub<BasicModel>.Simple(b => b.Text, "Apple", b => b.Integer, 16);

			Assert.Equal(16, fub.Integer);
			Assert.Equal("Apple", fub.Text);
		}
	}
}
