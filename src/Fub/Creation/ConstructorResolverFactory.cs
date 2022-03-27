using Fub.Creation.ConstructorResolvers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fub.Creation
{
	/// <summary>
	/// The ConstructorResolverFactory provides an IConstructorResolver for any given type that may need to be constructed during
	/// fub creation. This includes resolvers for built-in types that require a specific constructor or resolvers that provide
	/// constructors specified by the user.
	/// </summary>
	internal class ConstructorResolverFactory
	{
		private readonly DefaultConstructorResolver defaultConstructorResolver;
		private readonly IDictionary<Type, IConstructorResolver> resolvers;

		public ConstructorResolverFactory() : this(new Dictionary<Type, IConstructorResolver>())
		{
		}

		public ConstructorResolverFactory(IDictionary<Type, IConstructorResolver> resolvers)
		{
			defaultConstructorResolver = new DefaultConstructorResolver();
			this.resolvers = resolvers;
		}

		public IConstructorResolver CreateConstructorResolver<T>()
		{
			return CreateConstructorResolver(typeof(T));
		}

		public IConstructorResolver CreateConstructorResolver(Type type)
		{
			if (type.IsGenericType)
			{
				type = type.GetGenericTypeDefinition();
			}

			if (resolvers.TryGetValue(type, out IConstructorResolver? resolver))
			{
				return resolver;
			}

			return defaultConstructorResolver;
		}

		public void RegisterResolver<T>(IConstructorResolver resolver)
		{
			RegisterResolver(typeof(T), resolver);
		}

		public void RegisterResolver(Type type, IConstructorResolver resolver)
		{
			resolvers[type] = resolver;
		}

		public void RegisterConstructor<T>(ConstructorInfo constructor)
		{
			RegisterConstructor(typeof(T), constructor);
		}

		public void RegisterConstructor(Type type, ConstructorInfo constructor)
		{
			RegisterResolver(type, new FixedConstructorResolver(constructor));
		}
	}
}
