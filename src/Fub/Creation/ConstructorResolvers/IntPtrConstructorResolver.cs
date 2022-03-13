using System;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class IntPtrConstructorResolver : IConstructorResolver
	{
		private readonly ConstructorInfo constructor;

		public IntPtrConstructorResolver()
		{
			constructor = typeof(IntPtr).GetConstructor(new Type[] { typeof(Int32) })!;
		}

		public ConstructorInfo? Resolve()
		{
			return constructor;
		}
	}
}
