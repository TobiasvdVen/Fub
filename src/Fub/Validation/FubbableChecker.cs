using Fub.Creation;
using Fub.Prospects;
using Fub.ValueProvisioning;
using Fub.ValueProvisioning.ValueProviders;
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

		public FubbableChecker(ConstructorResolverFactory constructorResolverFactory, Prospector prospector, ProspectValues defaultValues)
		{
			this.constructorResolverFactory = constructorResolverFactory;
			this.prospector = prospector;
			this.defaultValues = defaultValues;
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

			ConstructorInfo? constructor = constructorResolverFactory.CreateConstructorResolver(type).Resolve();

			if (constructor is not null)
			{
				prospects = prospects.Concat(prospector.GetParameterProspects(type, constructor));
			}

			foreach (Prospect prospect in prospects)
			{
				if (prospect.Type.IsInterface)
				{
					if (!TryGetInterfaceDefault(prospect, requiredDefaults))
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

		private bool TryGetInterfaceDefault(Prospect prospect, ProspectValues requiredDefaults)
		{
			if (defaultValues.HasProvider(prospect) || requiredDefaults.HasProvider(prospect))
			{
				return true;
			}

			if (!prospect.Type.IsGenericType)
			{
				return false;
			}

			Type genericType = prospect.Type.GetGenericTypeDefinition();
			if (genericType == typeof(IEnumerable<>))
			{
				object? emptyEnumerable = typeof(Enumerable)
					.GetMethod("Empty", BindingFlags.Static | BindingFlags.Public)!
					.MakeGenericMethod(prospect.Type.GenericTypeArguments[0])
					.Invoke(null, null);

				requiredDefaults.SetProvider(prospect, new FixedValueProvider(emptyEnumerable));

				return true;
			}

			return false;
		}
	}
}
