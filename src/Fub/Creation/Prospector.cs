using Fub.Prospects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fub.Creation
{
	internal class Prospector : IProspector
	{
		public IEnumerable<Prospect> GetProspects(Type type, ConstructorInfo constructor)
		{
			IEnumerable<ParameterInfo> parameters = constructor.GetParameters();

			IEnumerable<MemberProspect> memberProspects = GetProspects(type);

			IEnumerable<Prospect> parameterProspects = parameters.Select(p =>
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

			return parameterProspects.Concat(memberProspects);
		}

		public IEnumerable<MemberProspect> GetProspects(Type type)
		{
			IEnumerable<MemberInfo> members = type
				.GetMembers(BindingFlags.Public | BindingFlags.Instance)
				.Where(m => m.IsPropertyOrField());

			IEnumerable<MemberProspect> memberProspects = members.Select<MemberInfo, MemberProspect>(m =>
			{
				if (m is FieldInfo field)
				{
					return new FieldProspect(field);
				}

				if (m is PropertyInfo property)
				{
					return new PropertyProspect(property);
				}

				throw new Exception($"This should be guaranteed to be impossible.");
			});

			return memberProspects;
		}
	}
}
