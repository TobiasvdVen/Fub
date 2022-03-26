using Fub.Creation;
using Fub.ValueProvisioning.ValueProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fub.ValueProvisioning
{
	internal class InterfaceValueProviderFactory
	{
		private readonly ICreator creator;

		public InterfaceValueProviderFactory(ICreator creator)
		{
			this.creator = creator;
		}

		public IValueProvider? Create(Type type)
		{
			if (!type.IsGenericType)
			{
				return null;
			}

			Type genericType = type.GetGenericTypeDefinition();
			if (genericType == typeof(IEnumerable<>))
			{
				object? emptyEnumerable = typeof(Enumerable)
					.GetMethod("Empty", BindingFlags.Static | BindingFlags.Public)!
					.MakeGenericMethod(type.GenericTypeArguments[0])
					.Invoke(null, null);

				return new FixedValueProvider(emptyEnumerable);
			}
			else if (genericType == typeof(ICollection<>))
			{
				Type list = typeof(List<>).MakeGenericType(type.GenericTypeArguments[0]);
				object? value = creator.Create(list);

				return new FixedValueProvider(value);
			}

			return null;
		}
	}
}
