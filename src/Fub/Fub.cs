using System;
using System.Linq.Expressions;

namespace Fub
{
	public static class Fub<T> where T : notnull
	{
		private static Fubber<T> fubber = new FubberBuilder<T>().Build();

		public static T Simple()
		{
			return fubber.Fub();
		}

		public static T Simple<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
		{
			return fubber.Fub(expression, value);
		}

		public static T Simple<TProperty1, TProperty2>(
			Expression<Func<T, TProperty1>> expression1, TProperty1 value1,
			Expression<Func<T, TProperty2>> expression2, TProperty2 value2)
		{
			return fubber.Fub(expression1, value1, expression2, value2);
		}
	}
}
