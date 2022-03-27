using System;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class FixedConstructorResolver : IConstructorResolver
	{
		private readonly ConstructorInfo constructor;

		public FixedConstructorResolver(ConstructorInfo constructor)
		{
			this.constructor = constructor;
		}

		public ConstructorInfo? Resolve(Type _)
		{
			return constructor;
		}
	}
}
