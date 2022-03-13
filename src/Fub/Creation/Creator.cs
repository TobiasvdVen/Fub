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

			object? created;

			IDictionary<Prospect, object?> values;

			if (constructor != null)
			{
				IEnumerable<Prospect> prospects = prospector.GetProspects(type, constructor);

				values = GetValues(prospectValues, prospects);

				List<object?> arguments = new();

				foreach (ParameterInfo parameter in constructor.GetParameters())
				{
					Prospect? prospect = values.Keys.FirstOrDefault(v => v is ParameterProspect prospect && prospect.ParameterInfo == parameter);

					if (prospect != null)
					{
						arguments.Add(values[prospect]);
					}
					else
					{
						arguments.Add(Create(parameter.ParameterType));
					}
				}

				created = constructor.Invoke(arguments.ToArray());
			}
			else
			{
				IEnumerable<Prospect> prospects = prospector.GetProspects(type);

				values = GetValues(prospectValues, prospects);

				created = Activator.CreateInstance(type);
			}

			if (created != null)
			{
				foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
				{
					// If the property has no getter, or the getter requires any parameters, skip the property
					if (property.GetGetMethod()?.GetParameters().Any() ?? true)
					{
						continue;
					}

					if (property.GetSetMethod() == null)
					{
						continue;
					}

					Prospect? prospect = values.Keys.FirstOrDefault(v => v is PropertyProspect prospect && prospect.MemberInfo == property);

					if (prospect != null)
					{
						property.SetValue(created, values[prospect]);
					}
					else
					{
						property.SetValue(created, Create(property.PropertyType));
					}
				}
			}

			return created;
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
