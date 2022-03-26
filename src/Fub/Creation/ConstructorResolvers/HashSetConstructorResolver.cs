using System;
using System.Linq;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class HashSetConstructorResolver : IConstructorResolver
	{
		private readonly ConstructorInfo constructor;

		public HashSetConstructorResolver(Type hashSetType)
		{
			constructor = hashSetType.GetConstructors().First();
		}

		public ConstructorInfo? Resolve()
		{
			return constructor;
		}
	}
}
