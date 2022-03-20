using Fub.ValueProvisioning;
using Fub.ValueProvisioning.ValueProviders;
using Xunit;

namespace Fub.InternalTests.ValueProvidersTests
{
	public class FixedValueProviderTests
	{
		[Fact]
		public void GetValue_WithFixedInteger_ReturnsInteger()
		{
			IValueProvider valueProvider = new FixedValueProvider(64);

			Assert.Equal(64, valueProvider.GetValue());
		}

		[Fact]
		public void GetValue_WithFixedString_ReturnsString()
		{
			IValueProvider valueProvider = new FixedValueProvider("Supercalifragilisticexpidalidocious");

			Assert.Equal("Supercalifragilisticexpidalidocious", valueProvider.GetValue());
		}

		[Fact]
		public void GetValue_WithFixedValueNull_ReturnsNull()
		{
			IValueProvider valueProvider = new FixedValueProvider(null);

			Assert.Null(valueProvider.GetValue());
		}
	}
}
