using Fub.Prospects;
using Fub.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fub.Creation
{
	/// <summary>
	/// The prospector finds aspects of a type that require initialization during construction of a fub.
	/// This means constructor parameters, when applicable, and any mutable fields and properties.
	/// The returned values are called 'prospects', prospective parameters/properties/fields.
	/// </summary>
	internal class Prospector
	{
		/// <summary>
		/// Finds all prospective properties and fields of the given type that should be initialized for the fub.
		/// This will only include fields and properties that are public and settable.
		/// </summary>
		public IEnumerable<MemberProspect> GetMemberProspects(Type type)
		{
			IEnumerable<MemberInfo> members = GetPropertiesAndFields(type);

			List<MemberProspect> memberProspects = new();

			foreach (MemberInfo memberInfo in members)
			{
				if (memberInfo is FieldInfo field)
				{
					memberProspects.Add(new FieldProspect(field));
				}

				if (memberInfo is PropertyInfo property)
				{
					if (property.SetMethod == null)
					{
						continue;
					}

					memberProspects.Add(new PropertyProspect(property));
				}
			}

			return memberProspects;
		}

		/// <summary>
		/// Finds all prospective parameters of the given constructor that should be initialized for the fub.
		/// </summary>
		public IEnumerable<ParameterProspect> GetParameterProspects(Type type, ConstructorInfo constructor)
		{
			IEnumerable<ParameterInfo> parameters = constructor.GetParameters();

			IEnumerable<MemberInfo> members = GetPropertiesAndFields(type);

			IEnumerable<ParameterProspect> parameterProspects = parameters.Select(p =>
			{
				MemberInfo? associatedMember = members.FirstOrDefault(m => m.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase));

				if (associatedMember != null)
				{
					return new ParameterProspect(p, associatedMember);
				}
				else
				{
					return new ParameterProspect(p);
				}
			});

			return parameterProspects;
		}

		private IEnumerable<MemberInfo> GetPropertiesAndFields(Type type)
		{
			return type
				.GetMembers(BindingFlags.Public | BindingFlags.Instance)
				.Where(m => m.IsPropertyOrField());
		}
	}
}
