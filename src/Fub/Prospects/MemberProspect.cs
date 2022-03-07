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
			int h = MemberInfo.GetHashCode();

			return h;
		}

		public override bool Equals(object? obj)
		{
			if (obj is MemberProspect other)
			{
				return MemberInfo.Equals(other.MemberInfo);
			}

			return false;
		}
	}
}
