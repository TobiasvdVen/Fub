using System;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class StringConstructorResolver : HeuristicConstructorResolver
	{
		public StringConstructorResolver() : base(typeof(string))
		{
		}

		public override ConstructorInfo? Resolve()
		{
			ConstructorInfo? constructor = base.Resolve();

			return constructor ?? typeof(string).GetConstructor(new Type[] { typeof(char[]) });
		}
	}
}
