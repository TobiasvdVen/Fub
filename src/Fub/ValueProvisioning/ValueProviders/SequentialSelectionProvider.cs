using System.Collections.Generic;
using System.Linq;

namespace Fub.ValueProvisioning.ValueProviders
{
	public class SequentialSelectionProvider<T> : GenericProvider<T>
	{
		private readonly IEnumerable<T> values;
		private int next;

		public SequentialSelectionProvider(IEnumerable<T> values)
		{
			this.values = values;

			next = 0;
		}

		public override T GetValue()
		{
			T value = values.ElementAt(next);

			next = (next + 1) % values.Count();

			return value;
		}
	}
}
