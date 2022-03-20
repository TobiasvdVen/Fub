using Fub.Prospects;
using Fub.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fub.Creation
{
	internal class Prospector : IProspector
	{
		public IEnumerable<MemberProspect> GetMemberProspects(Type type)
		{
			return GetMemberProspects(type, skipImmutable: false);
		}

		public IEnumerable<MemberProspect> GetMutableMemberProspects(Type type)
		{
			return GetMemberProspects(type, skipImmutable: true);
		}

		public IEnumerable<ParameterProspect> GetParameterProspects(Type type, ConstructorInfo constructor)
		{
			IEnumerable<ParameterInfo> parameters = constructor.GetParameters();

			IEnumerable<MemberProspect> memberProspects = GetMemberProspects(type);

			IEnumerable<ParameterProspect> parameterProspects = parameters.Select(p =>
			{
				MemberProspect? associatedMember = memberProspects.FirstOrDefault(m => m.MemberInfo.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase));

				if (associatedMember != null)
				{
					return new ParameterProspect(p, associatedMember.MemberInfo);
				}
				else
				{
					return new ParameterProspect(p);
				}
			});

			return parameterProspects;
		}

		private IEnumerable<MemberProspect> GetMemberProspects(Type type, bool skipImmutable)
		{
			IEnumerable<MemberInfo> members = type
				.GetMembers(BindingFlags.Public | BindingFlags.Instance)
				.Where(m => m.IsPropertyOrField());

			List<MemberProspect> memberProspects = new();

			foreach (MemberInfo memberInfo in members)
			{
				if (memberInfo is FieldInfo field)
				{
					memberProspects.Add(new FieldProspect(field));
				}

				if (memberInfo is PropertyInfo property)
				{
					if (property.SetMethod == null && skipImmutable)
					{
						continue;
					}

					memberProspects.Add(new PropertyProspect(property));
				}
			}

			return memberProspects;
		}
	}
}
