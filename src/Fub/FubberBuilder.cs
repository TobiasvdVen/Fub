using Fub.Creation;
using Fub.Creation.ConstructorResolvers;
using Fub.Prospects;
using Fub.Validation;
using Fub.ValueProvisioning;
using Fub.ValueProvisioning.ValueProviders;
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
			FubAssert.ConcreteType<T>();

			constructorResolverFactory = new ConstructorResolverFactory();

			DefaultValues = new ProspectValues();
		}

		public ICreator? Creator { get; private set; }

		public ProspectValues DefaultValues { get; }

		public Fubber<T> Build()
		{
			Prospector prospector = new();

			FubbableChecker fubbableChecker = new(constructorResolverFactory, prospector, DefaultValues);

			FubbableResult result = fubbableChecker.IsFubbable<T>();

			ProspectValues finalValues;

			switch (result)
			{
				case FubbableError error:
					throw new InvalidOperationException(error.Message);

				case FubbableNeedsDefaults needsDefaults:
					finalValues = ProspectValues.Combine(DefaultValues, needsDefaults.RequiredDefaults);
					break;

				default:
					finalValues = DefaultValues;
					break;
			}

			ICreator creator = Creator ?? new Creator(constructorResolverFactory, prospector);

			return new Fubber<T>(creator, finalValues);
		}

		public FubberBuilder<T> Make<TMember>(Expression<Func<T, TMember>> expression, TMember value)
		{
			MemberExpression memberExpression = FubAssert.MemberExpression(expression);
			FubAssert.NullSafe(memberExpression, value);

			DefaultValues.SetProvider(Prospect.FromMember(memberExpression.Member), new FixedValueProvider(value));

			return this;
		}

		public FubberBuilder<T> For<TMember>(Expression<Func<T, TMember>> expression, IValueProvider<TMember> valueProvider)
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
			return UseConstructor<T>(constructor);
		}

		public FubberBuilder<T> UseConstructor<TType>(ConstructorInfo constructor)
		{
			FubAssert.ValidConstructor<TType>(constructor);

			FixedConstructorResolver resolver = new(constructor);

			constructorResolverFactory.RegisterResolver<TType>(resolver);

			return this;
		}
	}
}
