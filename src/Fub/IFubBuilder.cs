using Fub.Creation;
using System;
using System.Linq.Expressions;

namespace Fub
{
	public interface IFubBuilder<T> where T : notnull
	{
		IFubBuilder<T> UseCreator(ICreator creator);
		IFubBuilder<T> WithDefault<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value);
		IFubBuilder<T> UseConstructor<TConstructor>(Expression<Func<T, TConstructor>> expression);
		IFubBuilder<T> UseConstructor<TOtherType, TConstructor>(Expression<Func<TOtherType, TConstructor>> expression);

		Fub<T> Build();
	}
}
