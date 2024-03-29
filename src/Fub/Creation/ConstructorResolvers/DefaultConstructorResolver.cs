﻿using System;
using System.Linq;
using System.Reflection;

namespace Fub.Creation.ConstructorResolvers
{
	internal class DefaultConstructorResolver : IConstructorResolver
	{
		public virtual ConstructorInfo? Resolve(Type type)
		{
			ConstructorInfo[] constructors = type.GetConstructors();

			if (constructors.Length == 1)
			{
				return constructors.Single();
			}

			if (constructors.Length > 1)
			{
				throw new InvalidOperationException($"Unable to create {type.Name}, multiple constructor options found. Call UseConstructor() during Build to specify which constructor to use.");
			}

			return null;
		}
	}
}
