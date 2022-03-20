using Fub.ValueProvisioning;
using System.Collections.Generic;

namespace Fub
{
	public struct Provide
	{
		public static IProvideFrom<T> From<T>(IEnumerable<T> values)
		{
			return new ProvideFrom<T>(values);
		}
	}
}
