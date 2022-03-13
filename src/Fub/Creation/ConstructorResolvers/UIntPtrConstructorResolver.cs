using System;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class UIntPtrConstructorResolver : IConstructorResolver
	{
		private readonly ConstructorInfo constructor;

		public UIntPtrConstructorResolver()
		{
			constructor = typeof(UIntPtr).GetConstructor(new Type[] { typeof(UIntPtr) })!;
		}

		public ConstructorInfo? Resolve()
		{
			return constructor;
		}
	}
}
