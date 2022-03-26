using Fub.ValueProvisioning.Randomization;
using Fub.ValueProvisioning.ValueProviders;
using System;
using System.Collections.Generic;

namespace Fub.ValueProvisioning
{
	/// <summary>
	/// ProvideFrom is used to specify whether the values passed in a <c>Provide.From()</c> call 
	/// should be selected randomly or sequentially.
	/// </summary>
	public class ProvideFrom<T>
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
