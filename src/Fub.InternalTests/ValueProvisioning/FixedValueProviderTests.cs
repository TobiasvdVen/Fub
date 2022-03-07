using Fub.ValueProvisioning;
using Xunit;

namespace Fub.InternalTests.ValueProvidersTests
{
	public class FixedValueProviderTests
	{
		[Fact]
		public void GivenIntValueThenReturnValue()
		{
			int value = 64;

			IValueProvider valueProvider = new FixedValueProvider(value);

			Assert.Equal(value, valueProvider.GetValue());
		}

		[Fact]
		public void GivenStringValueThenReturnValue()
		{
			string value = "Supercalifragilisticexpidalidocious";

			IValueProvider valueProvider = new FixedValueProvider(value);

			Assert.Equal(value, valueProvider.GetValue());
		}

		[Fact]
		public void GivenNullThenReturnNull()
		{
			IValueProvider valueProvider = new FixedValueProvider(null);

			Assert.Null(valueProvider.GetValue());
		}
	}
}
