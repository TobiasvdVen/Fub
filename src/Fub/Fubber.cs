﻿using Fub.Creation;
using Fub.Prospects;
using Fub.Utilities;
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

		public T Fub<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
		{
			IProspectValues prospectValues = defaultValues.Clone();

			OverrideAccordingToExpression(prospectValues, expression, value);

			return creator.Create<T>(prospectValues);
		}

		public T Fub<TProperty1, TProperty2>(
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
			MemberExpression memberExpression = FubAssert.MemberExpression(expression);
			FubAssert.NullSafe(memberExpression, value);

			prospectValues.SetProvider(Prospect.FromMember(memberExpression.Member), new FixedValueProvider<TProperty>(value));
		}
	}
}
