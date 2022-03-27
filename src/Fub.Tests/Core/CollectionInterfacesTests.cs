using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests.Core
{
	public class CollectionInterfacesTests
	{
		[Fact]
		public void Fub_WithInterfaceCollections_ReturnsDefaults()
		{
			CollectionInterfaces.Class fub = Fub<CollectionInterfaces.Class>.Simple();

			Assert.NotNull(fub.Enumerable);
			Assert.NotNull(fub.Collection);
			Assert.NotNull(fub.List);
			Assert.NotNull(fub.Set);
			Assert.NotNull(fub.ReadOnlyCollection);
			Assert.NotNull(fub.ReadOnlyList);
			Assert.NotNull(fub.Dictionary);
			Assert.NotNull(fub.ReadOnlyDictionary);

#if NET5_0_OR_GREATER
			Assert.NotNull(fub.ReadOnlySet);
#endif
		}
	}
}
