using Fub.Creation;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Fub
{
	public interface IFubBuilder<T> where T : notnull
	{
		IFubBuilder<T> UseCreator(ICreator creator);
		IFubBuilder<T> WithDefault<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value);
		IFubBuilder<T> UseConstructor(ConstructorInfo constructor);
		IFubBuilder<T> UseConstructor<TOtherType>(ConstructorInfo constructor);

		Fub<T> Build();
	}
}
