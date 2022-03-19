using System.Reflection;

namespace Fub.Prospects
{
	internal class ParameterProspect : Prospect
	{
		public ParameterProspect(ParameterInfo parameterInfo, MemberInfo? matchingMember = null) : base(ProspectInitialization.Constructor, parameterInfo.ParameterType, parameterInfo.IsNullable())
		{
			ParameterInfo = parameterInfo;
			MatchingMember = matchingMember;
		}

		public ParameterInfo ParameterInfo { get; }
		public MemberInfo? MatchingMember { get; }

		public override int GetHashCode()
		{
			return ParameterInfo.GetHashCode();
		}

		public override bool Equals(object? obj)
		{
			if (obj is ParameterProspect other)
			{
				return ParameterInfo.Equals(other.ParameterInfo);
			}

			return false;
		}
	}
}
