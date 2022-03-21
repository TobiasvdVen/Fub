using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Fub.Validation
{
	internal static class FubAssert
	{
		/// <summary>
		/// Assert that type T is not an interface, Fub does not support creating instances of non-concrete types.
		/// </summary>
		internal static void ConcreteType<T>()
		{
			Type type = typeof(T);

			if (type.IsInterface)
			{
				throw new InvalidOperationException($"Only concrete types can be Fubbed, {type.Name} is an interface.");
			}
		}

		/// <summary>
		/// Assert that the expression provided refers to a type member.
		/// </summary>
		internal static MemberExpression MemberExpression<T, TMember>(Expression<Func<T, TMember>> expression)
		{
			if (expression.Body is MemberExpression memberExpression)
			{
				return memberExpression;
			}

			throw new ArgumentException($"Expression must be a {nameof(MemberExpression)}.");
		}

		/// <summary>
		/// Assert that the given value is legal for the given member expression in a nullable context.
		/// </summary>
		internal static void NullSafe<TMember>(MemberExpression memberExpression, TMember value)
		{
			if (!memberExpression.Member.IsNullableMember() && value is null)
			{
				throw new ArgumentException($"A value of {null} cannot be provided to non-nullable member {memberExpression.Member.Name} of type {memberExpression.Member.DeclaringType?.Name}.");
			}
		}

		/// <summary>
		/// Assert that the given constructor is actually for the given type.
		/// </summary>
		internal static void ValidConstructor<T>(ConstructorInfo constructor)
		{
			if (constructor.DeclaringType != typeof(T))
			{
				throw new ArgumentException($"Invalid constructor, expected declaring type {typeof(T).Name}, but was {constructor.DeclaringType}.");
			}
		}
	}
}
