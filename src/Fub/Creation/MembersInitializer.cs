using Fub.Prospects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fub.Creation
{
	internal class MembersInitializer
	{
		public void Initialize(Type type, object fub, IDictionary<Prospect, object?> values)
		{
			foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				if (property.GetSetMethod() == null)
				{
					continue;
				}

				Prospect? prospect = values.Keys
					.OfType<PropertyProspect>()
					.FirstOrDefault(p => p.MemberInfo == property);

				if (prospect != null)
				{
					property.SetValue(fub, values[prospect]);
				}
			}
		}
	}
}
