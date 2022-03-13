using Xunit;

namespace Fub.Tests
{
	public class NoCtorMutableTests
	{
		private class NoCtorMutable
		{
			public string? Name { get; set; }
			public int Age { get; set; }
		}

		private readonly Fub<NoCtorMutable> noCtorMutable = new FubBuilder<NoCtorMutable>().Build();

		[Fact]
		public void Create_ClassWithNoOverrides_ReturnsDefault()
		{
			NoCtorMutable created = noCtorMutable.Create();

			Assert.NotNull(created);
			Assert.Null(created.Name);
			Assert.Equal(0, created.Age);
		}

		[Fact]
		public void Create_ClassWithNullableStringOverride_ReturnsFub()
		{
			string expectedName = "Henry";

			NoCtorMutable created = noCtorMutable.Create(m => m.Name, expectedName);

			Assert.Equal(expectedName, created.Name);
		}

		[Fact]
		public void Create_ClassWithIntOverride_ReturnsFub()
		{
			int expectedAge = 32;

			NoCtorMutable created = noCtorMutable.Create(m => m.Age, expectedAge);

			Assert.Equal(expectedAge, created.Age);
		}

		[Fact]
		public void Create_ClassWithNullableStringOverrideToNull_ReturnsFub()
		{
			NoCtorMutable created = noCtorMutable.Create(m => m.Name, null);

			Assert.Null(created.Name);
		}

		[Fact]
		public void Create_ClassWithIntAndNullableStringOverrides_ReturnsFub()
		{
			string expectedName = "Thomas";
			int expectedAge = 42;

			NoCtorMutable created = noCtorMutable.Create(
				m => m.Name, expectedName,
				m => m.Age, expectedAge);

			Assert.Equal(expectedName, created.Name);
			Assert.Equal(expectedAge, created.Age);
		}

		[Fact]
		public void Create_ClassWithIntAndNullableStringDefaults_ReturnsFub()
		{
			FubBuilder<NoCtorMutable> builder = new();

			string expectedName = "Katie";
			int expectedAge = 22;

			Fub<NoCtorMutable> noCtorMutable = builder
				.WithDefault(m => m.Name, expectedName)
				.WithDefault(m => m.Age, expectedAge)
				.Build();

			NoCtorMutable created = noCtorMutable.Create();

			Assert.Equal(expectedName, created.Name);
			Assert.Equal(expectedAge, created.Age);
		}
	}
}
