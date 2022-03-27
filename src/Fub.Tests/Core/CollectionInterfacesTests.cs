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

		[Fact]
		public void Fub_MultipleTimes_CreatesMultipleDefaultInstances()
		{
			FubberBuilder<CollectionInterfaces.Class> builder = new();
			Fubber<CollectionInterfaces.Class> fubber = builder.Build();

			CollectionInterfaces.Class a = fubber.Fub();
			CollectionInterfaces.Class b = fubber.Fub();

			Assert.False(ReferenceEquals(a.Enumerable, b.Enumerable));
			Assert.False(ReferenceEquals(a.Collection, b.Collection));
			Assert.False(ReferenceEquals(a.List, b.List));
			Assert.False(ReferenceEquals(a.Set, b.Set));
			Assert.False(ReferenceEquals(a.ReadOnlyCollection, b.ReadOnlyCollection));
			Assert.False(ReferenceEquals(a.ReadOnlyList, b.ReadOnlyList));
			Assert.False(ReferenceEquals(a.Dictionary, b.Dictionary));
			Assert.False(ReferenceEquals(a.ReadOnlyDictionary, b.ReadOnlyDictionary));

#if NET5_0_OR_GREATER
			Assert.False(ReferenceEquals(a.ReadOnlySet, b.ReadOnlySet));
#endif
		}
	}
}
