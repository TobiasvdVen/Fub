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

		private ConstructorResolverFactory(IDictionary<Type, IConstructorResolver> resolvers)
		{
			resolvers.Add(typeof(string), new StringConstructorResolver());
			resolvers.Add(typeof(decimal), new DecimalConstructorResolver());
			resolvers.Add(typeof(IntPtr), new IntPtrConstructorResolver());
			resolvers.Add(typeof(UIntPtr), new UIntPtrConstructorResolver());
			resolvers.Add(typeof(DateTime), new DateTimeConstructorResolver());

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

			return new HeuristicConstructorResolver(type);
		}

		public void RegisterResolver<T>(IConstructorResolver resolver)
		{
			RegisterResolver(typeof(T), resolver);
		}

		public void RegisterResolver(Type type, IConstructorResolver resolver)
		{
			resolvers[type] = resolver;
		}
	}
}
