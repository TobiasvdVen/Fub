using Fub.Creation.ConstructorResolvers;
using Fub.Prospects;
using Fub.ValueProvisioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fub.Creation
{
	internal class Creator : ICreator
	{
		private readonly IConstructorResolverFactory constructorResolverFactory;
		private readonly IProspector prospector;

		private readonly MembersInitializer membersInitializer;

		public Creator(IConstructorResolverFactory constructorResolverFactory, IProspector prospector)
		{
			this.constructorResolverFactory = constructorResolverFactory;
			this.prospector = prospector;

			membersInitializer = new();
		}

		public T Create<T>() where T : notnull
		{
			return (T)Create(typeof(T))!;
		}

		public object? Create(Type type)
		{
			return Create(type, new ProspectValues());
		}

		public T Create<T>(IProspectValues prospectValues) where T : notnull
		{
			return (T)Create(typeof(T), prospectValues)!;
		}

		public object? Create(Type type, IProspectValues prospectValues)
		{
			IConstructorResolver constructorResolver = constructorResolverFactory.CreateConstructorResolver(type);
			ConstructorInfo? constructor = constructorResolver.Resolve();

			if (constructor != null)
			{
				return CreateWithConstructor(type, prospectValues, constructor);
			}

			object? fub = Activator.CreateInstance(type);

			if (fub == null)
			{
				throw new FubException($"Failed to construct object of type {type}, {nameof(Activator.CreateInstance)} returned null.");
			}

			IEnumerable<Prospect> prospects = prospector.GetMutableMemberProspects(type);

			IDictionary<Prospect, object?> values = GetValues(prospectValues, prospects);

			membersInitializer.Initialize(type, fub, values);

			return fub;
		}

		private object CreateWithConstructor(Type type, IProspectValues prospectValues, ConstructorInfo constructor)
		{
			IEnumerable<ParameterProspect> parameterProspects = prospector.GetParameterProspects(type, constructor);

			IDictionary<Prospect, object?> parameterValues = GetValues(prospectValues, parameterProspects);

			List<object?> arguments = new();

			foreach (ParameterInfo parameter in constructor.GetParameters())
			{
				Prospect? prospect = parameterValues.Keys.FirstOrDefault(v => v is ParameterProspect prospect && prospect.ParameterInfo == parameter);

				if (prospect != null)
				{
					arguments.Add(parameterValues[prospect]);
				}
				else
				{
					arguments.Add(Create(parameter.ParameterType));
				}
			}

			object fub = constructor.Invoke(arguments.ToArray());

			IEnumerable<MemberProspect> memberProspects = prospector.GetMutableMemberProspects(type);
			IDictionary<Prospect, object?> memberValues = GetValues(prospectValues, memberProspects);

			membersInitializer.Initialize(type, fub, memberValues);

			return fub;
		}

		private IDictionary<Prospect, object?> GetValues(IProspectValues prospectValues, IEnumerable<Prospect> prospects)
		{
			Dictionary<Prospect, object?> values = new();

			foreach (Prospect prospect in prospects)
			{
				if (prospectValues.TryGetProvider(prospect, out IValueProvider? valueProvider))
				{
#if NET5_0_OR_GREATER
					values[prospect] = valueProvider.GetValue();
#else
					values[prospect] = valueProvider!.GetValue();
#endif
				}
				else
				{
					if (prospect.Nullable)
					{
						values[prospect] = null;
					}
					else
					{
						values[prospect] = Create(prospect.Type);
					}
				}
			}

			return values;
		}
	}
}
