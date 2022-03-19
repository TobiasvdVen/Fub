using System;
using System.Reflection;

namespace Fub.Prospects
{
	public abstract class Prospect
	{
		public Prospect(ProspectInitialization initialization, Type type, bool nullable)
		{
			Initialization = initialization;
			Type = type;
			Nullable = nullable;
		}

		public ProspectInitialization Initialization { get; }
		public Type Type { get; }
		public bool Nullable { get; }

		public static Prospect FromMember(MemberInfo member)
		{
			if (member is PropertyInfo property)
			{
				return new PropertyProspect(property);
			}

			if (member is FieldInfo field)
			{
				return new FieldProspect(field);
			}

			throw new ArgumentException($"Member must be either {nameof(PropertyInfo)} or {nameof(FieldInfo)}, but was {member.GetType()}.");
		}

		public override int GetHashCode()
		{
			return new { Initialization, Type, Nullable }.GetHashCode();
		}
	}
}
