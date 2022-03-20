using System;
using System.Linq;
using System.Reflection;

namespace Fub.Prospects
{
	internal abstract class MemberProspect : Prospect
	{
		public MemberProspect(Type type, bool nullable, MemberInfo memberInfo) : base(type, nullable)
		{
			MemberInfo = memberInfo;
		}

		public MemberInfo MemberInfo { get; }

		public abstract void SetValue(object target, object? value);

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

			if (GetType() != obj.GetType())
			{
				return false;
			}

			if (obj is MemberProspect other)
			{
				if (MemberInfo.DeclaringType == other.MemberInfo.DeclaringType)
				{
					return true;
				}

				if (other.MemberInfo.DeclaringType != null && other.MemberInfo.DeclaringType.IsInterface)
				{
					if (MemberInfo.DeclaringType != null && MemberInfo.DeclaringType.GetInterfaces().Contains(other.MemberInfo.DeclaringType))
					{
						return true;
					}
				}

				if (MemberInfo.DeclaringType != null && MemberInfo.DeclaringType.IsInterface)
				{
					if (other.MemberInfo.DeclaringType != null)
					{
						if (other.MemberInfo.DeclaringType.GetInterfaces().Contains(MemberInfo.DeclaringType))
						{
							return true;
						}

						if (other.MemberInfo.DeclaringType.GetInterfaces().Any(i => i.GetGenericTypeDefinition() == MemberInfo.DeclaringType.GetGenericTypeDefinition()))
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetHashCode();

			return new { hashCode, MemberInfo.Name }.GetHashCode();
		}
	}
}
