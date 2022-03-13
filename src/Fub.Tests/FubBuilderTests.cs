using Fub.Creation;
using Fub.ValueProvisioning;
using Moq;
using System;
using System.Reflection;
using Xunit;

namespace Fub.Tests
{
	public class FubBuilderTests
	{
		private class SimpleCreateable
		{

		}

		[Fact]
		public void Build_Default_ReturnsFub()
		{
			FubBuilder<SimpleCreateable> builder = new();

			Fub<SimpleCreateable> fub = builder.Build();

			SimpleCreateable created = fub.Create();

			Assert.NotNull(created);
		}

		[Fact]
		public void Build_WithCustomCreator_InjectsCreator()
		{
			FubBuilder<SimpleCreateable> builder = new();
			Mock<ICreator> creator = new();

			Fub<SimpleCreateable> fub = builder.UseCreator(creator.Object).Build();

			fub.Create();

			creator.Verify(c => c.Create<SimpleCreateable>(It.IsAny<IProspectValues>()));
		}

		public class SomeClass
		{
			public string Name { get; set; } = "Peter";
			public int Age { get; set; }
		}

		[Fact]
		public void Build_WithTwoDefaults_ReturnsFub()
		{
			FubBuilder<SomeClass> builder = new();

			string expectedName = "Sarah";
			int expectedAge = 256;

			Fub<SomeClass> fub = builder
				.WithDefault(s => s.Name, expectedName)
				.WithDefault(s => s.Age, expectedAge)
				.Build();

			SomeClass created = fub.Create();

			Assert.Equal(expectedName, created.Name);
			Assert.Equal(expectedAge, created.Age);
		}

		public class Goodbye
		{
			public void Method()
			{

			}

			public int Property { get; } = 128;
		}

		[Fact]
		public void WithDefault_InvalidExpression_Throws()
		{
			FubBuilder<Goodbye> builder = new();

			Assert.Throws<ArgumentException>(() => builder.WithDefault(g => "", ""));
		}

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
		public void Build_WithConstructor_InjectsConstructor()
		{
			FubBuilder<TwoConstructors> builder = new();

			ConstructorInfo intConstructor = typeof(TwoConstructors).GetConstructor(new Type[] { typeof(int) })!;
			Fub<TwoConstructors> fub = builder.UseConstructor(intConstructor).Build();

			TwoConstructors created = fub.Create();

			Assert.Equal(0, created.Value);
		}
	}
}
