using System;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class DecimalConstructorResolver : IConstructorResolver
	{
		private readonly ConstructorInfo constructor;

		public DecimalConstructorResolver()
		{
			constructor = typeof(decimal).GetConstructor(new Type[] { typeof(Int32) })!;
		}

		public ConstructorInfo? Resolve()
		{
			return constructor;
		}
	}
}
