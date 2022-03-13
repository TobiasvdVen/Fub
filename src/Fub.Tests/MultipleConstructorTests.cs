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
			FubBuilder<TwoConstructors> builder = new();

			ConstructorInfo intConstructor = typeof(TwoConstructors).GetConstructor(new Type[] { typeof(int) })!;
			Fub<TwoConstructors> fub = builder.UseConstructor(intConstructor).Build();

			TwoConstructors created = fub.Create();

			Assert.Equal(0, created.Value);
		}

		[Fact]
		public void Create_WithNoConstructorOverride_Throws()
		{
			FubBuilder<TwoConstructors> builder = new();
			Fub<TwoConstructors> fub = builder.Build();

			Assert.Throws<InvalidOperationException>(() => fub.Create());
		}
	}
}
