using Fub.Creation;
using Fub.Creation.ConstructorResolvers;
using Fub.Prospects;
using Fub.ValueProvisioning;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Fub
{
	public class FubBuilder<T> : IFubBuilder<T> where T : notnull
	{
		private readonly IConstructorResolverFactory constructorResolverFactory;

		public FubBuilder()
		{
			Type type = typeof(T);

			if (type.IsInterface)
			{
				throw new InvalidOperationException($"Only concrete types can be Fubbed, {type.Name} is an interface.");
			}

			constructorResolverFactory = new ConstructorResolverFactory();

			DefaultValues = new ProspectValues();
		}

		public ICreator? Creator { get; private set; }

		public IProspectValues DefaultValues { get; }

		public Fub<T> Build()
		{
			ICreator creator = Creator ?? new Creator(constructorResolverFactory, new Prospector());

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

		public IFubBuilder<T> UseConstructor(ConstructorInfo constructor)
		{
			FixedConstructorResolver resolver = new(constructor);

			constructorResolverFactory.RegisterResolver<T>(resolver);

			return this;
		}

		public IFubBuilder<T> UseConstructor<TOtherType>(ConstructorInfo constructor)
		{
			throw new NotImplementedException();
		}
	}
}
