using System;
using System.Linq;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class ListConstructorResolver : IConstructorResolver
	{
		public ConstructorInfo? Resolve(Type type)
		{
			return type.GetConstructors().First();
		}
	}
}
