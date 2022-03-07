using System;
using System.Linq;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class HeuristicConstructorResolver : IConstructorResolver
	{
		private readonly Type type;

		public HeuristicConstructorResolver(Type type)
		{
			this.type = type;
		}

		public virtual ConstructorInfo? Resolve()
		{
			ConstructorInfo[] constructors = type.GetConstructors();

			if (constructors.Length == 1)
			{
				return constructors.Single();
			}

			return null;
		}
	}
}
