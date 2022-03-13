using System;
using System.Linq;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class StringConstructorResolver : IConstructorResolver
	{
		ConstructorInfo constructor;

		public StringConstructorResolver()
		{
			ConstructorInfo[] constructors = typeof(string).GetConstructors();

			constructor = constructors.FirstOrDefault() ?? typeof(string).GetConstructor(new Type[] { typeof(char[]) })!;
		}

		public ConstructorInfo? Resolve()
		{
			return constructor;
		}
	}
}
