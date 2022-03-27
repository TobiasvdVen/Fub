using System.Collections.Generic;

namespace Fub.Tests.Models
{
	public interface ICollectionInterfaces
	{
		IEnumerable<int> Enumerable { get; }
		ICollection<string> Collection { get; }
		IList<float> List { get; }
		ISet<double> Set { get; }
		IReadOnlyCollection<byte> ReadOnlyCollection { get; }
		IReadOnlyList<short> ReadOnlyList { get; }
		IDictionary<string, bool> Dictionary { get; }
		IReadOnlyDictionary<long, StringInt.Class> ReadOnlyDictionary { get; }

#if NET5_0_OR_GREATER
		IReadOnlySet<uint> ReadOnlySet { get; }
#endif
	}

	public static class CollectionInterfaces
	{
		public class Class : ICollectionInterfaces
		{
			public Class(
				IEnumerable<int> enumerable,
				ICollection<string> collection,
				IList<float> list,
				ISet<double> set,
				IReadOnlyCollection<byte> readOnlyCollection,
				IReadOnlyList<short> readOnlyList,
				IDictionary<string, bool> dictionary,
				IReadOnlyDictionary<long, StringInt.Class> readOnlyDictionary
#if NET5_0_OR_GREATER
				, IReadOnlySet<uint> readOnlySet
#endif
				)
			{
				Enumerable = enumerable;
				Collection = collection;
				List = list;
				Set = set;
				ReadOnlyCollection = readOnlyCollection;
				ReadOnlyList = readOnlyList;
				Dictionary = dictionary;
				ReadOnlyDictionary = readOnlyDictionary;

#if NET5_0_OR_GREATER
				ReadOnlySet = readOnlySet;
#endif
			}

			public IEnumerable<int> Enumerable { get; }
			public ICollection<string> Collection { get; }
			public IList<float> List { get; }
			public ISet<double> Set { get; }
			public IReadOnlyCollection<byte> ReadOnlyCollection { get; }
			public IReadOnlyList<short> ReadOnlyList { get; }
			public IDictionary<string, bool> Dictionary { get; }
			public IReadOnlyDictionary<long, StringInt.Class> ReadOnlyDictionary { get; }

#if NET5_0_OR_GREATER

			public IReadOnlySet<uint> ReadOnlySet { get; }
#endif
		}
	}
}
