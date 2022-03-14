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
				// If the property has no getter, or the getter requires any parameters, skip the property
				if (property.GetGetMethod()?.GetParameters().Any() ?? true)
				{
					continue;
				}

				if (property.GetSetMethod() == null)
				{
					continue;
				}

				Prospect? prospect = values.Keys.FirstOrDefault(v => v is PropertyProspect prospect && prospect.MemberInfo == property);

				if (prospect != null)
				{
					property.SetValue(fub, values[prospect]);
				}
				else
				{
					throw new NotImplementedException();
					//property.SetValue(fub, Create(property.PropertyType));
				}
			}
		}
	}
}
