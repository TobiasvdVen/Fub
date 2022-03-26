using Fub.Creation;
using Fub.ValueProvisioning.ValueProviders;
using System;
using System.Collections.Generic;

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
			if (type.IsGenericType)
			{
				Type genericType = type.GetGenericTypeDefinition();

				if (genericType == typeof(IEnumerable<>) ||
					genericType == typeof(ICollection<>) ||
					genericType == typeof(IList<>) ||
					genericType == typeof(IReadOnlyCollection<>) ||
					genericType == typeof(IReadOnlyList<>))
				{
					Type list = typeof(List<>).MakeGenericType(type.GenericTypeArguments[0]);
					object? value = creator.Create(list);

					return new FixedValueProvider(value);
				}
				else if (genericType == typeof(ISet<>))
				{
					Type set = typeof(HashSet<>).MakeGenericType(type.GenericTypeArguments[0]);
					object? value = creator.Create(set);

					return new FixedValueProvider(value);
				}
#if NET5_0_OR_GREATER
				else if (genericType == typeof(IReadOnlySet<>))
				{
					Type set = typeof(HashSet<>).MakeGenericType(type.GenericTypeArguments[0]);
					object? value = creator.Create(set);

					return new FixedValueProvider(value);
				}
#endif
			}

			return null;
		}
	}
}
