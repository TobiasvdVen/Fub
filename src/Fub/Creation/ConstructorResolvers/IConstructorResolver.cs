using System;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	public interface IConstructorResolver
	{
		ConstructorInfo? Resolve(Type type);
	}
}
