using System;
using System.Reflection;

namespace Fub.Prospects
{
	/// <summary>
	/// A prospect represents a parameter, field or property of a type that requires initialization during creation
	/// and may be overridden by the user through the Fubber or static Fub APIs.
	/// </summary>
	public abstract class Prospect
	{
		public Prospect(Type type, bool nullable)
		{
			Type = type;
			Nullable = nullable;
		}

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
			return new { Type, Nullable }.GetHashCode();
		}
	}
}
