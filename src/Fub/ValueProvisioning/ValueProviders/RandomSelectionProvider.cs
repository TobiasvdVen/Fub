using Fub.ValueProvisioning.Randomization;
using System.Collections.Generic;
using System.Linq;

namespace Fub.ValueProvisioning.ValueProviders
{
	public class RandomSelectionProvider<T> : GenericProvider<T>
	{
		private IEnumerable<T> values;
		private readonly IRandom random;

		public RandomSelectionProvider(IEnumerable<T> values, IRandom random)
		{
			this.values = values;
			this.random = random;
		}

		public override T GetValue()
		{
			int index = random.Next(values.Count() - 1);

			return values.ElementAt(index);
		}
	}
}
