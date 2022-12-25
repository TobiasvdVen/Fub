using Fub.Creation;
using Fub.Creation.ConstructorResolvers;
using Fub.Prospects;
using Fub.Validation;
using Fub.ValueProvisioning;
using Fub.ValueProvisioning.ValueProviders;
using System;
using System.Linq;
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
			DefaultConstructorRegistrar constructorRegistrar = new();
			constructorRegistrar.Register(constructorResolverFactory);

			Prospector prospector = new();
			ICreator creator = Creator ?? new Creator(constructorResolverFactory, prospector);

			FubbableChecker fubbableChecker = new(constructorResolverFactory, prospector, DefaultValues, new InterfaceValueProviderFactory(creator));

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

		public FubberBuilder<T> UseConstructor(params Type[] constructorParameterTypes)
		{
			return UseConstructor<T>(constructorParameterTypes);
		}

		public FubberBuilder<T> UseConstructor<TType>(params Type[] constructorParameterTypes)
		{
			ConstructorInfo? constructor = typeof(TType).GetConstructor(constructorParameterTypes);

			if (constructor == null)
			{
				bool multiple = constructorParameterTypes.Length > 1;
				string parameterOrPlural = multiple ? "parameters" : "parameter";
				string parameterTypes = string.Join(", ", constructorParameterTypes.Select(t => t.Name));
				throw new ArgumentException($"No constructor could be found on type {typeof(TType).Name} with {parameterOrPlural} of type {parameterTypes}");
			}

			return UseConstructor<T>(constructor);
		}
	}
}
