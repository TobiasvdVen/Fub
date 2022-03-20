using Fub.ValueProvisioning.Randomization;
using System.Collections.Generic;
using System.Linq;

namespace Fub.ValueProvisioning.ValueProviders
{
	public class RandomSelectionProvider<T> : IValueProvider<T>
	{
		private IEnumerable<T> values;
		private readonly IRandom random;

		private int limit;

		public RandomSelectionProvider(IEnumerable<T> values, IRandom random)
		{
			this.values = values;
			this.random = random;

			limit = values.Count() - 1;
		}

		public T GetValue()
		{
			int index = random.Next(limit);

			return values.ElementAt(index);
		}

		object? IValueProvider.GetValue()
		{
			return ((IValueProvider<T>)this).GetValue();
		}
	}
}
