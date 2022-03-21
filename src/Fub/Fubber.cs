using Fub.Creation;
using Fub.Prospects;
using Fub.Validation;
using Fub.ValueProvisioning;
using Fub.ValueProvisioning.ValueProviders;
using System;
using System.Linq.Expressions;

namespace Fub
{
	public class Fubber<T> where T : notnull
	{
		private readonly ICreator creator;
		private readonly IProspectValues defaultValues;

		public Fubber(ICreator creator, IProspectValues defaultValues)
		{
			FubAssert.ConcreteType<T>();

			this.creator = creator;
			this.defaultValues = defaultValues;
		}

		public T Fub()
		{
			return creator.Create<T>(defaultValues);
		}

		public T Fub<TMember>(Expression<Func<T, TMember>> expression, TMember value)
		{
			IProspectValues prospectValues = defaultValues.Clone();

			OverrideAccordingToExpression(prospectValues, expression, value);

			return creator.Create<T>(prospectValues);
		}

		public T Fub<TMember1, TMember2>(
			Expression<Func<T, TMember1>> expression1, TMember1 value1,
			Expression<Func<T, TMember2>> expression2, TMember2 value2)
		{
			IProspectValues prospectValues = defaultValues.Clone();

			OverrideAccordingToExpression(prospectValues, expression1, value1);
			OverrideAccordingToExpression(prospectValues, expression2, value2);

			return creator.Create<T>(prospectValues);
		}

		private void OverrideAccordingToExpression<TMember>(IProspectValues prospectValues, Expression<Func<T, TMember>> expression, TMember value)
		{
			MemberExpression memberExpression = FubAssert.MemberExpression(expression);
			FubAssert.NullSafe(memberExpression, value);

			prospectValues.SetProvider(Prospect.FromMember(memberExpression.Member), new FixedValueProvider<TMember>(value));
		}
	}
}
