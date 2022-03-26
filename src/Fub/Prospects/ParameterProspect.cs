using Fub.Validation;
using System.Reflection;

namespace Fub.Prospects
{
	internal class ParameterProspect : Prospect
	{
		public ParameterProspect(ParameterInfo parameterInfo, ConstructorInfo constructor, MemberInfo? matchingMember = null) : base(parameterInfo.ParameterType, parameterInfo.IsNullable())
		{
			ParameterInfo = parameterInfo;
			Constructor = constructor;
			MatchingMember = matchingMember;
		}

		public ParameterInfo ParameterInfo { get; }
		public ConstructorInfo Constructor { get; }
		public MemberInfo? MatchingMember { get; }

		public override bool Equals(object? obj)
		{
			if (obj is null)
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj is ParameterProspect other)
			{
				if (Constructor == other.Constructor && ParameterInfo.Name == other.ParameterInfo.Name)
				{
					return true;
				}
			}

			return false;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetHashCode();

			return new { hashCode, ParameterInfo.Name }.GetHashCode();
		}
	}
}
