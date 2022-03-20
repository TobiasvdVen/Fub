using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests.Core
{
	public class FactoryMethodProviderTests
	{
		[Fact]
		public void Fub_GivenFactoryMethod_ReturnsFub()
		{
			FubberBuilder<StringInt.Class> builder = new();
			Fubber<StringInt.Class> fubber = builder
				.For(c => c.Integer, Provide.FromFactory(() => 8192))
				.Build();

			StringInt.Class fub = fubber.Fub();

			Assert.Equal(8192, fub.Integer);
		}
	}
}
