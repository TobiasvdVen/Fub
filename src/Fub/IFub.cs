using System;
using System.Linq.Expressions;

namespace Fub
{
	public interface IFub<T> where T : notnull
	{
		T Create();
		T Create<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value);
		T Create<TProperty1, TProperty2>(Expression<Func<T, TProperty1>> expression1, TProperty1 value1, Expression<Func<T, TProperty2>> expression2, TProperty2 value2);
	}
}
