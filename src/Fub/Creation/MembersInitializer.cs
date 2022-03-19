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

			foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
			{
				Prospect? prospect = values.Keys
					.OfType<FieldProspect>()
					.FirstOrDefault(f => f.MemberInfo == field);

				if (prospect != null)
				{
					field.SetValue(fub, values[prospect]);
				}
			}
		}
	}
}
