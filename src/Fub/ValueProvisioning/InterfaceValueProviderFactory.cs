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

					return new FactoryMethodProvider<object>(() => creator.Create(list));
				}
				else if (genericType == typeof(ISet<>))
				{
					Type set = typeof(HashSet<>).MakeGenericType(type.GenericTypeArguments[0]);

					return new FactoryMethodProvider<object>(() => creator.Create(set));
				}
				else if (genericType == typeof(IDictionary<,>) ||
						 genericType == typeof(IReadOnlyDictionary<,>))
				{
					Type dictionary = typeof(Dictionary<,>).MakeGenericType(type.GenericTypeArguments[0], type.GenericTypeArguments[1]);

					return new FactoryMethodProvider<object>(() => creator.Create(dictionary));
				}
#if NET5_0_OR_GREATER
				else if (genericType == typeof(IReadOnlySet<>))
				{
					Type set = typeof(HashSet<>).MakeGenericType(type.GenericTypeArguments[0]);

					return new FactoryMethodProvider<object>(() => creator.Create(set));
				}
#endif
			}

			return null;
		}
	}
}
