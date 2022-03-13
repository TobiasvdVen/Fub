using System;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class DateTimeConstructorResolver : IConstructorResolver
	{
		ConstructorInfo constructor;

		public DateTimeConstructorResolver()
		{
			constructor = typeof(DateTime).GetConstructor(new Type[] { typeof(Int64) })!;
		}

		public ConstructorInfo? Resolve()
		{
			return constructor;
		}
	}
}
