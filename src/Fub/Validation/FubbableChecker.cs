﻿using Fub.Creation;
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
		private readonly IConstructorResolverFactory constructorResolverFactory;
		private readonly IProspectValues defaultValues;

		public FubbableChecker(IConstructorResolverFactory constructorResolverFactory, IProspectValues defaultValues)
		{
			this.constructorResolverFactory = constructorResolverFactory;
			this.defaultValues = defaultValues;
		}

		public FubbableResult IsFubbable(Type type, IProspector prospector)
		{
			IEnumerable<Prospect> prospects = prospector.GetMemberProspects(type);

			ConstructorInfo? constructor = constructorResolverFactory.CreateConstructorResolver(type).Resolve();

			if (constructor is not null)
			{
				prospects = prospects.Concat(prospector.GetParameterProspects(type, constructor));
			}

			Prospect? interfaceWithoutDefault = prospects.FirstOrDefault(p => p.Type.IsInterface && !defaultValues.HasProvider(p));

			if (interfaceWithoutDefault is not null)
			{
				return new FubbableError($"No default value can be generated for interface type {interfaceWithoutDefault.Type.Name}, and no default type was provided.");
			}

			foreach (Prospect prospect in prospects)
			{
				if (prospect.Type == type)
				{
					continue;
				}

				if (IsFubbable(prospect.Type, prospector) is FubbableError error)
				{
					return error;
				}
			}

			return new FubbableResult(ok: true);
		}
	}
}
