using System.Collections.Generic;

namespace Fub.Tests.Models
{
	public interface ICollectionInterfaces
	{
		IEnumerable<int> Enumerable { get; }
		//		ICollection<string> Collection { get; }
		//		IList<float> List { get; }
		//		ISet<double> Set { get; }
		//		IDictionary<string, bool> Dictionary { get; }
		//		IReadOnlyCollection<byte> ReadOnlyCollection { get; }
		//		IReadOnlyList<short> ReadOnlyList { get; }
		//		IReadOnlyDictionary<long, StringInt.Class> ReadOnlyDictionary { get; }

		//#if NET5_0_OR_GREATER
		//		IReadOnlySet<uint> ReadOnlySet { get; }
		//#endif
	}

	public static class CollectionInterfaces
	{
		public class Class : ICollectionInterfaces
		{
			public Class(
				IEnumerable<int> enumerable
				//				ICollection<string> collection,
				//				IList<float> list,
				//				ISet<double> set,
				//				IDictionary<string, bool> dictionary,
				//				IReadOnlyCollection<byte> readOnlyCollection,
				//				IReadOnlyList<short> readOnlyList,
				//				IReadOnlyDictionary<long, StringInt.Class> readOnlyDictionary
				//#if NET5_0_OR_GREATER
				//				, IReadOnlySet<uint> readOnlySet
				//#endif
				)
			{
				Enumerable = enumerable;
				//				Collection = collection;
				//				List = list;
				//				Set = set;
				//				Dictionary = dictionary;
				//				ReadOnlyCollection = readOnlyCollection;
				//				ReadOnlyList = readOnlyList;
				//				ReadOnlyDictionary = readOnlyDictionary;

				//#if NET5_0_OR_GREATER
				//				ReadOnlySet = readOnlySet;
				//#endif
			}

			public IEnumerable<int> Enumerable { get; }
			//			public ICollection<string> Collection { get; }
			//			public IList<float> List { get; }
			//			public ISet<double> Set { get; }
			//			public IDictionary<string, bool> Dictionary { get; }
			//			public IReadOnlyCollection<byte> ReadOnlyCollection { get; }
			//			public IReadOnlyList<short> ReadOnlyList { get; }
			//			public IReadOnlyDictionary<long, StringInt.Class> ReadOnlyDictionary { get; }

			//#if NET5_0_OR_GREATER
			//			public IReadOnlySet<uint> ReadOnlySet { get; }
			//#endif
		}
	}
}
