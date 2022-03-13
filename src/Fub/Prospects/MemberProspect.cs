using System;
using System.Reflection;

namespace Fub.Prospects
{
	internal abstract class MemberProspect : Prospect
	{
		public MemberProspect(ProspectInitialization initialization, Type type, MemberInfo memberInfo) : base(initialization, memberInfo.IsNullable(), type)
		{
			MemberInfo = memberInfo;
		}

		public MemberInfo MemberInfo { get; }

		public override int GetHashCode()
		{
			return new { MemberInfo.Name }.GetHashCode();
		}

		public override bool Equals(object? obj)
		{
			if (obj is MemberProspect other)
			{
				return other.GetHashCode() == GetHashCode();
			}

			return false;
		}
	}
}
