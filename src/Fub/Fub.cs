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

		public static T Simple<TMember>(Expression<Func<T, TMember>> expression, TMember value)
		{
			return fubber.Fub(expression, value);
		}

		public static T Simple<TMember1, TMember2>(
			Expression<Func<T, TMember1>> expression1, TMember1 value1,
			Expression<Func<T, TMember2>> expression2, TMember2 value2)
		{
			return fubber.Fub(expression1, value1, expression2, value2);
		}
	}
}
