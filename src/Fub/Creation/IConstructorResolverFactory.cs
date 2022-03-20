using Fub.Creation.ConstructorResolvers;
using System;

namespace Fub.Creation
{
	/// <summary>
	/// The IConstructorResolverFactory provides an IConstructorResolver for any given type that may need to be constructed during
	/// fub creation. This includes resolvers for built-in types that require a specific constructor or resolvers that provide
	/// constructors specified by the user.
	/// </summary>
	public interface IConstructorResolverFactory
	{
		IConstructorResolver CreateConstructorResolver<T>();
		IConstructorResolver CreateConstructorResolver(Type type);
	}
}
