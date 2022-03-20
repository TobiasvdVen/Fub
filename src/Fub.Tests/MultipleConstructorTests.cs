﻿using Fub.Tests.Models;
using System;
using System.Reflection;
using Xunit;

namespace Fub.Tests
{
	public class MultipleConstructorTests
	{
		[Fact]
		public void Fub_WithConstructor_UsesSpecifiedConstructor()
		{
			FubberBuilder<TwoConstructors> builder = new();

			ConstructorInfo intConstructor = typeof(TwoConstructors).GetConstructor(new Type[] { typeof(int) })!;
			Fubber<TwoConstructors> fubber = builder.UseConstructor(intConstructor).Build();

			TwoConstructors fub = fubber.Fub();

			// The default constructor sets Value to 1, so if this is 0 we used the specified constructor instead
			Assert.Equal(0, fub.Value);
		}

		[Fact]
		public void Fub_WithNoConstructorOverride_Throws()
		{
			FubberBuilder<TwoConstructors> builder = new();
			Fubber<TwoConstructors> fubber = builder.Build();

			Assert.Throws<InvalidOperationException>(() => fubber.Fub());
		}

		public class HasNestedTwoConstructors
		{
			public HasNestedTwoConstructors(TwoConstructors twoConstructors)
			{
				TwoConstructors = twoConstructors;
			}

			public TwoConstructors TwoConstructors { get; }
		}

		[Fact]
		public void Fub_WithNoNestedConstructorOverride_Throws()
		{
			FubberBuilder<HasNestedTwoConstructors> builder = new();
			Fubber<HasNestedTwoConstructors> fubber = builder.Build();

			Assert.Throws<InvalidOperationException>(() => fubber.Fub());
		}

		[Fact]
		public void Fub_WithNestedConstructorOverride_UsesSpecifiedNestedConstructor()
		{
			FubberBuilder<HasNestedTwoConstructors> builder = new();

			ConstructorInfo intConstructor = typeof(TwoConstructors).GetConstructor(new Type[] { typeof(int) })!;

			Fubber<HasNestedTwoConstructors> fubber = builder
				.UseConstructor<TwoConstructors>(intConstructor)
				.Build();

			HasNestedTwoConstructors fub = fubber.Fub();

			// The default constructor sets Value to 1, so if this is 0 we used the specified constructor instead
			Assert.Equal(0, fub.TwoConstructors.Value);
		}
	}
}
