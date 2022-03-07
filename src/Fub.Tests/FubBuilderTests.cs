using Fub.Creation;
using Fub.ValueProvisioning;
using System;
using Xunit;

namespace Fub.Tests
{
	public class FubBuilderTests
	{
		private class SimpleCreateable
		{

		}

		[Fact]
		public void GivenBuilderWhenBuildThenReturnDefaultFub()
		{
			FubBuilder<SimpleCreateable> builder = new();

			Fub<SimpleCreateable> fub = builder.Build();

			SimpleCreateable created = fub.Create();

			Assert.NotNull(created);
		}

		private class CustomCreator : ICreator
		{
			public object? Create(Type type, IProspectValues memberValues)
			{
				throw new NotImplementedException("ShouldBuildCustomCreator");
			}

			public T Create<T>(IProspectValues memberValues) where T : notnull
			{
				throw new NotImplementedException("ShouldBuildCustomCreator");
			}

			public T Create<T>() where T : notnull
			{
				throw new NotImplementedException("ShouldBuildCustomCreator");
			}

			public object? Create(Type type)
			{
				throw new NotImplementedException("ShouldBuildCustomCreator");
			}
		}

		[Fact]
		public void GivenBuilderWithCustomCreatorWhenBuildThenReturnFub()
		{
			FubBuilder<SimpleCreateable> builder = new();

			Fub<SimpleCreateable> fub = builder.UseCreator(new CustomCreator()).Build();

			NotImplementedException ex = Assert.Throws<NotImplementedException>(() => fub.Create());
			Assert.Equal("ShouldBuildCustomCreator", ex.Message);
		}

		public class SomeClass
		{
			public string Name { get; set; } = "Peter";
			public int Age { get; set; }
		}

		[Fact]
		public void GivenBuilderWithSpecifiedDefaultsWhenBuildThenReturnFub()
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
		public void GivenExpressionWhenNotPropertyOrFieldThenThrow()
		{
			FubBuilder<Goodbye> builder = new();

			Assert.Throws<ArgumentException>(() => builder.WithDefault(g => "", ""));
		}
	}
}
