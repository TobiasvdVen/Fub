using System;
using System.Reflection;

namespace Fub.Prospects
{
	public abstract class Prospect
	{
		public Prospect(ProspectInitialization initialization, bool nullable, Type type)
		{
			Initialization = initialization;
			Nullable = nullable;
			Type = type;
		}

		public ProspectInitialization Initialization { get; }
		public bool Nullable { get; }
		public Type Type { get; }

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
	}
}
