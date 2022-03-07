using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	public interface IConstructorResolver
	{
		ConstructorInfo? Resolve();
	}
}
