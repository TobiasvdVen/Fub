using Fub.Creation.ConstructorResolvers;
using System;

namespace Fub.Creation
{
	public interface IConstructorResolverFactory
	{
		IConstructorResolver CreateConstructorResolver<T>();
		IConstructorResolver CreateConstructorResolver(Type type);
	}
}
