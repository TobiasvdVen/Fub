using Fub.Creation;
using Fub.Creation.ConstructorResolvers;
using Fub.Prospects;
using Fub.Utilities;
using Fub.ValueProvisioning;
using Fub.ValueProvisioning.ValueProviders;
using System;
using System.Collections.Generic;
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

		public IProspectValues DefaultValues { get; }

		public Fubber<T> Build()
		{
			IProspector prospector = new Prospector();

			CheckForUnspecifiedInterfaceProspects(typeof(T), prospector);

			ICreator creator = Creator ?? new Creator(constructorResolverFactory, new Prospector());

			return new Fubber<T>(creator, DefaultValues);
		}

		private void CheckForUnspecifiedInterfaceProspects(Type type, IProspector prospector)
		{
			IEnumerable<Prospect> prospects = prospector.GetMemberProspects(type);

			ConstructorInfo? constructor = constructorResolverFactory.CreateConstructorResolver(type).Resolve();

			if (constructor is not null)
			{
				prospects = prospects.Concat(prospector.GetParameterProspects(type, constructor));
			}

			if (prospects.Any(p => p.Type.IsInterface && !DefaultValues.HasProvider(p)))
			{
				throw new InvalidOperationException();
			}

			foreach (Prospect prospect in prospects)
			{
				if (prospect.Type == type)
				{
					continue;
				}

				CheckForUnspecifiedInterfaceProspects(prospect.Type, prospector);
			}
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
