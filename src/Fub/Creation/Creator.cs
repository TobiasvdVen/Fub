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

		public Creator(IConstructorResolverFactory constructorResolverFactory, IProspector prospector)
		{
			this.constructorResolverFactory = constructorResolverFactory;
			this.prospector = prospector;
		}

		public T Create<T>() where T : notnull
		{
			return (T)Create(typeof(T));
		}

		public object Create(Type type)
		{
			return Create(type, new ProspectValues());
		}

		public T Create<T>(IProspectValues prospectValues) where T : notnull
		{
			return (T)Create(typeof(T), prospectValues);
		}

		public object Create(Type type, IProspectValues prospectValues)
		{
			IConstructorResolver constructorResolver = constructorResolverFactory.CreateConstructorResolver(type);
			ConstructorInfo? constructor = constructorResolver.Resolve();

			object fub;

			if (constructor != null)
			{
				fub = CreateWithConstructor(type, prospectValues, constructor);
			}
			else
			{
				fub = Activator.CreateInstance(type) ?? throw new FubException($"Failed to construct object of type {type}, {nameof(Activator.CreateInstance)} returned null.");
			}

			IEnumerable<MemberProspect> prospects = prospector.GetMemberProspects(type);

			InitializeMembers(fub, prospects, prospectValues);

			return fub;
		}

		private object CreateWithConstructor(Type type, IProspectValues prospectValues, ConstructorInfo constructor)
		{
			IEnumerable<ParameterProspect> parameterProspects = prospector.GetParameterProspects(type, constructor);

			List<object?> arguments = new(parameterProspects.Count());

			foreach (ParameterProspect prospect in parameterProspects)
			{
				object? value = GetValue(prospectValues, prospect);

				arguments.Add(value);
			}

			return constructor.Invoke(arguments.ToArray());
		}

		private void InitializeMembers(object fub, IEnumerable<MemberProspect> prospects, IProspectValues prospectValues)
		{
			foreach (MemberProspect prospect in prospects)
			{
				object? value = GetValue(prospectValues, prospect);

				prospect.SetValue(fub, value);
			}
		}

		private object? GetValue(IProspectValues prospectValues, Prospect prospect)
		{
			if (prospectValues.TryGetProvider(prospect, out IValueProvider? valueProvider))
			{
#if NET5_0_OR_GREATER
				return valueProvider.GetValue();
#else
				return valueProvider!.GetValue();
#endif
			}

			if (prospect.Nullable)
			{
				return null;
			}

			return Create(prospect.Type);
		}
	}
}
