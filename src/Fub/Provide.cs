using Fub.ValueProvisioning;
using Fub.ValueProvisioning.Randomization;
using Fub.ValueProvisioning.ValueProviders;
using System;
using System.Collections.Generic;

namespace Fub
{
	public struct Provide
	{
		public static IValueProvider<T> From<T>(IEnumerable<T> values, IRandom? random = null)
		{
			return new RandomSelectionProvider<T>(values, random ?? new RandomAdapter(new Random()));
		}
	}
}
