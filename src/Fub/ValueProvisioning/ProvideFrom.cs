using Fub.ValueProvisioning.Randomization;
using Fub.ValueProvisioning.ValueProviders;
using System;
using System.Collections.Generic;

namespace Fub.ValueProvisioning
{
	internal class ProvideFrom<T> : IProvideFrom<T>
	{
		private readonly IEnumerable<T> values;

		public ProvideFrom(IEnumerable<T> values)
		{
			this.values = values;
		}

		public IValueProvider<T> Randomly(IRandom? random)
		{
			return new RandomSelectionProvider<T>(values, random ?? new RandomAdapter(new Random()));
		}

		public IValueProvider<T> Sequentially()
		{
			return new SequentialSelectionProvider<T>(values);
		}
	}
}
