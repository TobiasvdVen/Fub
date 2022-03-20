using Fub.ValueProvisioning;
using Fub.ValueProvisioning.ValueProviders;
using System;
using System.Collections.Generic;

namespace Fub
{
	public struct Provide
	{
		public static IProvideFrom<T> From<T>(IEnumerable<T> values)
		{
			return new ProvideFrom<T>(values);
		}

		public static IValueProvider<T> FromFactory<T>(Func<T> factoryMethod)
		{
			return new FactoryMethodProvider<T>(factoryMethod);
		}
	}
}
