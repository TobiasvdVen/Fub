using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class NullableConstructorResolver : IConstructorResolver
	{
		public ConstructorInfo? Resolve()
		{
			return null;
		}
	}
}
