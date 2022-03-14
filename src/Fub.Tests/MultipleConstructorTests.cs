using System;
using System.Reflection;
using Xunit;

namespace Fub.Tests
{
	public class MultipleConstructorTests
	{
		public class TwoConstructors
		{
			public TwoConstructors()
			{
				Value = 1;
			}

			public TwoConstructors(int value)
			{
				Value = value;
			}

			public int Value { get; }
		}

		[Fact]
		public void Create_WithConstructor_UsesSpecifiedConstructor()
		{
			FubberBuilder<TwoConstructors> builder = new();

			ConstructorInfo intConstructor = typeof(TwoConstructors).GetConstructor(new Type[] { typeof(int) })!;
			Fubber<TwoConstructors> fubber = builder.UseConstructor(intConstructor).Build();

			TwoConstructors fub = fubber.Fub();

			Assert.Equal(0, fub.Value);
		}

		[Fact]
		public void Create_WithNoConstructorOverride_Throws()
		{
			FubberBuilder<TwoConstructors> builder = new();
			Fubber<TwoConstructors> fubber = builder.Build();

			Assert.Throws<InvalidOperationException>(() => fubber.Fub());
		}
	}
}
