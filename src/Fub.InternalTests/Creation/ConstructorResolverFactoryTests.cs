using Fub.Creation;
using Fub.Creation.ConstructorResolvers;
using Xunit;

namespace Fub.InternalTests.Creation
{
	public class ConstructorResolverFactoryTests
	{
		private class UnknownType
		{

		}

		[Fact]
		public void CreateConstructorResolver_WhenNotFound_ReturnDefaultConstructorResolver()
		{
			ConstructorResolverFactory factory = new();

			IConstructorResolver resolver = factory.CreateConstructorResolver<UnknownType>();

			Assert.IsType<DefaultConstructorResolver>(resolver);
		}
	}
}
