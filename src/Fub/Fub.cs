using Fub.Creation;
using Fub.Prospects;
using Fub.ValueProvisioning;
using System;
using System.Linq.Expressions;

namespace Fub
{
	public class Fub<T> : IFub<T> where T : notnull
	{
		private readonly ICreator creator;
		private readonly IProspectValues defaultValues;

		public Fub(ICreator creator, IProspectValues defaultValues)
		{
			this.creator = creator;
			this.defaultValues = defaultValues;
		}

		public T Create()
		{
			return creator.Create<T>(defaultValues);
		}

		public T Create<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
		{
			IProspectValues prospectValues = defaultValues.Clone();

			OverrideAccordingToExpression(prospectValues, expression, value);

			return creator.Create<T>(prospectValues);
		}

		public T Create<TProperty1, TProperty2>(
			Expression<Func<T, TProperty1>> expression1, TProperty1 value1,
			Expression<Func<T, TProperty2>> expression2, TProperty2 value2)
		{
			IProspectValues prospectValues = defaultValues.Clone();

			OverrideAccordingToExpression(prospectValues, expression1, value1);
			OverrideAccordingToExpression(prospectValues, expression2, value2);

			return creator.Create<T>(prospectValues);
		}

		private void OverrideAccordingToExpression<TProperty>(IProspectValues prospectValues, Expression<Func<T, TProperty>> expression, TProperty value)
		{
			if (expression.Body is MemberExpression memberExpression)
			{
				prospectValues.SetProvider(Prospect.FromMember(memberExpression.Member), new FixedValueProvider<TProperty>(value));
			}
			else
			{
				throw new InvalidOperationException($"Expression must be a {nameof(MemberExpression)}.");
			}
		}
	}
}
