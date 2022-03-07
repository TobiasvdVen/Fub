using Fub.Creation.ConstructorResolvers;
using System;
using System.Collections.Generic;

namespace Fub.Creation
{
	internal class ConstructorResolverFactory : IConstructorResolverFactory
	{
		private readonly IDictionary<Type, IConstructorResolver> resolvers;

		public ConstructorResolverFactory() : this(new Dictionary<Type, IConstructorResolver>())
		{
		}

		public ConstructorResolverFactory(IDictionary<Type, IConstructorResolver> resolvers)
		{
			this.resolvers = resolvers;
		}

		public IConstructorResolver CreateConstructorResolver<T>()
		{
			return CreateConstructorResolver(typeof(T));
		}

		public IConstructorResolver CreateConstructorResolver(Type type)
		{
			if (resolvers.TryGetValue(type, out IConstructorResolver? resolver))
			{
				return resolver;
			}

			if (type == typeof(string))
			{
				return new StringConstructorResolver();
			}

			return new HeuristicConstructorResolver(type);
		}
	}
}
