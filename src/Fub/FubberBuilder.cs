using Fub.Creation;
using Fub.Creation.ConstructorResolvers;
using Fub.Prospects;
using Fub.Utilities;
using Fub.ValueProvisioning;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Fub
{
	public class FubberBuilder<T> where T : notnull
	{
		private readonly ConstructorResolverFactory constructorResolverFactory;

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
			MemberExpression memberExpression = FubAssert.MemberExpression(expression);
			FubAssert.NullSafe(memberExpression, value);

			DefaultValues.SetProvider(Prospect.FromMember(memberExpression.Member), new FixedValueProvider(value));

			return this;
		}

		public FubberBuilder<T> For<TProperty>(Expression<Func<T, TProperty>> expression, IValueProvider<TProperty> valueProvider)
		{
			MemberExpression memberExpression = FubAssert.MemberExpression(expression);

			DefaultValues.SetProvider(Prospect.FromMember(memberExpression.Member), valueProvider);

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
