using Fub.Validation;
using System.Reflection;

namespace Fub.Prospects
{
	internal class ParameterProspect : Prospect
	{
		public ParameterProspect(ParameterInfo parameterInfo, MemberInfo? matchingMember = null) : base(parameterInfo.ParameterType, parameterInfo.IsNullable())
		{
			ParameterInfo = parameterInfo;
			MatchingMember = matchingMember;
		}

		public ParameterInfo ParameterInfo { get; }
		public MemberInfo? MatchingMember { get; }
	}
}
