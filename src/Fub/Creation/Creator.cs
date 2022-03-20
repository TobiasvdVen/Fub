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

			if (constructor != null)
			{
				return CreateWithConstructor(type, prospectValues, constructor);
			}

			object? fub = Activator.CreateInstance(type);

			if (fub is null)
			{
				throw new FubException($"Failed to construct object of type {type}, {nameof(Activator.CreateInstance)} returned null.");
			}

			IEnumerable<Prospect> prospects = prospector.GetMemberProspects(type);

			InitializeProspects(fub, prospects, prospectValues);

			return fub;
		}

		private object CreateWithConstructor(Type type, IProspectValues prospectValues, ConstructorInfo constructor)
		{
			IEnumerable<ParameterProspect> parameterProspects = prospector.GetParameterProspects(type, constructor);

			List<object?> arguments = new(parameterProspects.Count());

			foreach (ParameterProspect prospect in parameterProspects)
			{
				arguments.Add(GetValue(prospectValues, prospect));
			}

			object fub = constructor.Invoke(arguments.ToArray());

			IEnumerable<MemberProspect> memberProspects = prospector.GetMemberProspects(type);

			InitializeProspects(fub, memberProspects, prospectValues);

			return fub;
		}

		private void InitializeProspects(object fub, IEnumerable<Prospect> prospects, IProspectValues prospectValues)
		{
			foreach (Prospect prospect in prospects)
			{
				object? value = GetValue(prospectValues, prospect);

				if (prospect is PropertyProspect propertyProspect)
				{
					propertyProspect.PropertyInfo.SetValue(fub, value);
				}
				else if (prospect is FieldProspect fieldProspect)
				{
					fieldProspect.FieldInfo.SetValue(fub, value);
				}
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
