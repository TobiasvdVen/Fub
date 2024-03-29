﻿using Fub.ValueProvisioning;
using Fub.ValueProvisioning.ValueProviders;
using System;
using System.Collections.Generic;

namespace Fub
{
	/// <summary>
	/// Helpers for use with <c>FubberBuilder.For()</c>.
	/// </summary>
	public struct Provide
	{
		public static ProvideFrom<T> From<T>(IEnumerable<T> values)
		{
			return new ProvideFrom<T>(values);
		}

		public static IValueProvider<T> FromFactory<T>(Func<T> factoryMethod)
		{
			return new FactoryMethodProvider<T>(factoryMethod);
		}
	}
}
