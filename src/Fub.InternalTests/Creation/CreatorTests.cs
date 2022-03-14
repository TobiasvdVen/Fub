using Fub.Creation;
using Xunit;

namespace Fub.InternalTests.Creation
{
	public class CreatorTests
	{
		[Fact]
		public void Create_Object_ReturnsNotNull()
		{
			ICreator creator = new Creator(new ConstructorResolverFactory(), new Prospector());

			// Default comparison of a plain object to another plain object will be false if the references are different
			// Comparing GetHashCode() will also be different as it returns the sync block index for the instance.
			// We use ObjectComparer here just to validate that the created object is actually just of type 'object'
			// and consider them equal in that regard.
			Assert.Equal(new object(), creator.Create<object>(), new ObjectComparer());
		}

		[Fact]
		public void Create_String_ReturnsEmptyString()
		{
			ICreator creator = new Creator(new ConstructorResolverFactory(), new Prospector());

			// Non-nullable strings should not be null by default
			// Ensure that created strings are empty by default as an alternative
			Assert.Equal(string.Empty, creator.Create<string>());
		}
	}
}
