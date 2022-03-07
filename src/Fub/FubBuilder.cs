using Fub.Creation;
using Fub.Prospects;
using Fub.ValueProvisioning;
using System;
using System.Linq.Expressions;

namespace Fub
{
	public class FubBuilder<T> : IFubBuilder<T> where T : notnull
	{
		public FubBuilder()
		{
			DefaultValues = new ProspectValues();
			ConstructorResolverFactory = new ConstructorResolverFactory();
		}

		public ICreator? Creator { get; private set; }

		public IProspectValues DefaultValues { get; }

		public IConstructorResolverFactory ConstructorResolverFactory { get; }

		public Fub<T> Build()
		{
			ICreator creator = Creator ?? new Creator(ConstructorResolverFactory, new Prospector());

			return new Fub<T>(creator, DefaultValues);
		}

		public IFubBuilder<T> WithDefault<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
		{
			if (expression.Body is MemberExpression memberExpression)
			{
				DefaultValues.SetProvider(Prospect.FromMember(memberExpression.Member), new FixedValueProvider(value));
			}
			else
			{
				throw new ArgumentException($"Expression must be of type {nameof(MemberExpression)} but was {expression.GetType()}.");
			}

			return this;
		}

		public IFubBuilder<T> UseCreator(ICreator creator)
		{
			Creator = creator;

			return this;
		}

		public IFubBuilder<T> UseConstructor<TConstructor>(Expression<Func<T, TConstructor>> expression)
		{
			throw new NotImplementedException();
		}

		public IFubBuilder<T> UseConstructor<TOtherType, TConstructor>(Expression<Func<TOtherType, TConstructor>> expression)
		{
			throw new NotImplementedException();
		}
	}
}
