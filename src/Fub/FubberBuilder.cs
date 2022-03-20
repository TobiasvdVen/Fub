using Fub.Creation;
using Fub.Creation.ConstructorResolvers;
using Fub.Prospects;
using Fub.ValueProvisioning;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Fub
{
	public class FubberBuilder<T> where T : notnull
	{
		private readonly IConstructorResolverFactory constructorResolverFactory;

		public FubberBuilder()
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

		public Fubber<T> Build()
		{
			ICreator creator = Creator ?? new Creator(constructorResolverFactory, new Prospector());

			return new Fubber<T>(creator, DefaultValues);
		}

		public FubberBuilder<T> Make<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
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

		public FubberBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, IValueProvider<TProperty> valueProvider)
		{
			if (expression.Body is MemberExpression memberExpression)
			{
				DefaultValues.SetProvider(Prospect.FromMember(memberExpression.Member), valueProvider);
			}
			else
			{
				throw new ArgumentException($"Expression must be of type {nameof(MemberExpression)} but was {expression.GetType()}.");
			}

			return this;
		}

		public FubberBuilder<T> UseCreator(ICreator creator)
		{
			Creator = creator;

			return this;
		}

		public FubberBuilder<T> UseConstructor(ConstructorInfo constructor)
		{
			FixedConstructorResolver resolver = new(constructor);

			constructorResolverFactory.RegisterResolver<T>(resolver);

			return this;
		}

		public FubberBuilder<T> UseConstructor<TOtherType>(ConstructorInfo constructor)
		{
			throw new NotImplementedException();
		}
	}
}
