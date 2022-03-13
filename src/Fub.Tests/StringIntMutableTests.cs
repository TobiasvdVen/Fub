using Xunit;

namespace Fub.Tests
{
	public class StringIntMutableTests
	{
		public interface IStringIntMutable
		{
			string? String { get; set; }
			int Integer { get; set; }
		}

		public class Class : IStringIntMutable
		{
			public string? String { get; set; }
			public int Integer { get; set; }
		}

		public struct Struct : IStringIntMutable
		{
			public string? String { get; set; }
			public int Integer { get; set; }
		}

		[Fact]
		public void Create_ClassWithNoOverrides_ReturnsDefault()
		{
			Create_WithNoOverrides_ReturnsDefault<Class>();
		}

		[Fact]
		public void Create_StructWithNoOverrides_ReturnsDefault()
		{
			Create_WithNoOverrides_ReturnsDefault<Struct>();
		}

		private void Create_WithNoOverrides_ReturnsDefault<T>() where T : IStringIntMutable
		{
			FubBuilder<T> builder = new();
			Fub<T> fub = builder.Build();

			IStringIntMutable created = fub.Create();

			Assert.Null(created.String);
			Assert.Equal(default, created.Integer);
		}

		[Fact]
		public void Create_ClassWithNullableStringOverride_ReturnsFub()
		{
			Create_WithNullableStringOverride_ReturnsFub<Class>();
		}

		[Fact]
		public void Create_StructWithNullableStringOverride_ReturnsFub()
		{
			Create_WithNullableStringOverride_ReturnsFub<Struct>();
		}

		private void Create_WithNullableStringOverride_ReturnsFub<T>() where T : IStringIntMutable
		{
			FubBuilder<T> builder = new();
			Fub<T> fub = builder.Build();

			IStringIntMutable created = fub.Create(f => f.String, "Henry");

			Assert.Equal("Henry", created.String);
		}

		[Fact]
		public void Create_ClassWithIntOverride_ReturnsFub()
		{
			Create_WithIntOverride_ReturnsFub<Class>();
		}

		[Fact]
		public void Create_StructWithIntOverride_ReturnsFub()
		{
			Create_WithIntOverride_ReturnsFub<Struct>();
		}

		public void Create_WithIntOverride_ReturnsFub<T>() where T : IStringIntMutable
		{
			FubBuilder<T> builder = new();
			Fub<T> fub = builder.Build();

			IStringIntMutable created = fub.Create(m => m.Integer, 32);

			Assert.Equal(32, created.Integer);
		}

		[Fact]
		public void Create_ClassWithNullableStringOverrideToNull_ReturnsFub()
		{
			Create_WithNullableStringOverrideToNull_ReturnsFub<Class>();
		}

		[Fact]
		public void Create_StructWithNullableStringOverrideToNull_ReturnsFub()
		{
			Create_WithNullableStringOverrideToNull_ReturnsFub<Struct>();
		}

		private void Create_WithNullableStringOverrideToNull_ReturnsFub<T>() where T : IStringIntMutable
		{
			FubBuilder<T> builder = new();
			Fub<T> fub = builder.Build();

			IStringIntMutable created = fub.Create(m => m.String, null);

			Assert.Null(created.String);
		}

		[Fact]
		public void Create_ClassWithIntAndNullableStringOverrides_ReturnsFub()
		{
			Create_WithIntAndNullableStringOverrides_ReturnsFub<Class>();
		}

		[Fact]
		public void Create_StructWithIntAndNullableStringOverrides_ReturnsFub()
		{
			Create_WithIntAndNullableStringOverrides_ReturnsFub<Struct>();
		}

		private void Create_WithIntAndNullableStringOverrides_ReturnsFub<T>() where T : IStringIntMutable
		{
			FubBuilder<T> builder = new();
			Fub<T> fub = builder.Build();

			IStringIntMutable created = fub.Create(
				m => m.String, "Thomas",
				m => m.Integer, 42);

			Assert.Equal("Thomas", created.String);
			Assert.Equal(42, created.Integer);
		}

		[Fact]
		public void Create_ClassWithIntAndNullableStringDefaults_ReturnsFub()
		{

		}

		private void Create_ClassWithIntAndNullableStringDefaults_ReturnsFub<T>() where T : IStringIntMutable
		{
			FubBuilder<T> builder = new();

			Fub<T> noCtorMutable = builder
				.WithDefault(m => m.String, "Katie")
				.WithDefault(m => m.Integer, 22)
				.Build();

			IStringIntMutable created = noCtorMutable.Create();

			Assert.Equal("Katie", created.String);
			Assert.Equal(22, created.Integer);
		}
	}
}
