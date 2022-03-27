using Fub.Creation;
using Fub.Prospects;
using Fub.ValueProvisioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fub.Validation
{
	internal class FubbableChecker
	{
		private readonly ConstructorResolverFactory constructorResolverFactory;
		private readonly Prospector prospector;
		private readonly ProspectValues defaultValues;
		private readonly InterfaceValueProviderFactory interfaceValueProviderFactory;

		public FubbableChecker(
			ConstructorResolverFactory constructorResolverFactory,
			Prospector prospector,
			ProspectValues defaultValues,
			InterfaceValueProviderFactory interfaceValueProviderFactory)
		{
			this.constructorResolverFactory = constructorResolverFactory;
			this.prospector = prospector;
			this.defaultValues = defaultValues;
			this.interfaceValueProviderFactory = interfaceValueProviderFactory;
		}

		public FubbableResult IsFubbable<T>()
		{
			return IsFubbable(typeof(T));
		}

		public FubbableResult IsFubbable(Type type)
		{
			return IsFubbable(type, new ProspectValues());
		}

		private FubbableResult IsFubbable(Type type, ProspectValues requiredDefaults)
		{
			IEnumerable<Prospect> prospects = prospector.GetMemberProspects(type);

			ConstructorInfo? constructor = constructorResolverFactory.CreateConstructorResolver(type).Resolve(type);

			if (constructor is not null)
			{
				prospects = prospects.Concat(prospector.GetParameterProspects(type, constructor));
			}

			foreach (Prospect prospect in prospects)
			{
				if (prospect.Type.IsInterface &&
					!defaultValues.HasProvider(prospect) &&
					!requiredDefaults.HasProvider(prospect))
				{
					IValueProvider? valueProvider = interfaceValueProviderFactory.Create(prospect.Type);

					if (valueProvider != null)
					{
						requiredDefaults.SetProvider(prospect, valueProvider);
					}
					else
					{
						return new FubbableError($"No default value can be generated for interface type {prospect.Type.Name}, and no default type was provided.");
					}
				}
			}

			foreach (Prospect prospect in prospects)
			{
				if (prospect.Type == type)
				{
					continue;
				}

				if (IsFubbable(prospect.Type, requiredDefaults) is FubbableError error)
				{
					return error;
				}
			}

			if (requiredDefaults.Any())
			{
				return new FubbableNeedsDefaults(requiredDefaults);
			}

			return new FubbableResult(ok: true);
		}
	}
}
