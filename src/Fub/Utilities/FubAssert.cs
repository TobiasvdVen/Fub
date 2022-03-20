using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Fub.Utilities
{
	internal static class FubAssert
	{
		internal static void ConcreteType<T>()
		{
			Type type = typeof(T);

			if (type.IsInterface)
			{
				throw new InvalidOperationException($"Only concrete types can be Fubbed, {type.Name} is an interface.");
			}
		}

		internal static MemberExpression MemberExpression<T, TProperty>(Expression<Func<T, TProperty>> expression)
		{
			if (expression.Body is MemberExpression memberExpression)
			{
				return memberExpression;
			}

			throw new ArgumentException($"Expression must be a {nameof(MemberExpression)}.");
		}

		internal static void NullSafe<TProperty>(MemberExpression memberExpression, TProperty value)
		{
			if (!memberExpression.Member.IsNullableMember() && value is null)
			{
				throw new ArgumentException($"A value of {null} cannot be provided to non-nullable member {memberExpression.Member.Name} of type {memberExpression.Member.DeclaringType?.Name}.");
			}
		}

		internal static void ValidConstructor<T>(ConstructorInfo constructor)
		{
			if (constructor.DeclaringType != typeof(T))
			{
				throw new ArgumentException($"Invalid constructor, expected declaring type {typeof(T).Name}, but was {constructor.DeclaringType}.");
			}
		}
	}
}
