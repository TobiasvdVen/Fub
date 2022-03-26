using System;
using System.Linq;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class ListConstructorResolver : IConstructorResolver
	{
		private readonly ConstructorInfo constructor;

		public ListConstructorResolver(Type listType)
		{
			constructor = listType.GetConstructors().First();
		}

		public ConstructorInfo? Resolve()
		{
			return constructor;
		}
	}
}
