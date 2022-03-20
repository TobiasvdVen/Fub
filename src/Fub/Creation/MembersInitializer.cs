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
			foreach (Prospect prospect in values.Keys)
			{
				if (prospect is PropertyProspect propertyProspect)
				{
					propertyProspect.PropertyInfo.SetValue(fub, values[prospect]);
				}
				else if (prospect is FieldProspect fieldProspect)
				{
					fieldProspect.FieldInfo.SetValue(fub, values[prospect]);
				}
			}
		}
	}
}
