using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests
{
	public class StringIntMutableTests
	{
		[Fact]
		public void Create_ClassWithNoOverrides_ReturnsDefault()
		{
			Create_WithNoOverrides_ReturnsDefault<StringIntMutable.Class>();
		}

		[Fact]
		public void Create_StructWithNoOverrides_ReturnsDefault()
		{
			Create_WithNoOverrides_ReturnsDefault<StringIntMutable.Struct>();
		}

		private void Create_WithNoOverrides_ReturnsDefault<T>() where T : IStringIntMutable
		{
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IStringIntMutable fub = fubber.Fub();

			Assert.Null(fub.String);
			Assert.Equal(default, fub.Integer);
		}

		[Fact]
		public void Create_ClassWithNullableStringOverride_ReturnsFub()
		{
			Create_WithNullableStringOverride_ReturnsFub<StringIntMutable.Class>();
		}

		[Fact]
		public void Create_StructWithNullableStringOverride_ReturnsFub()
		{
			Create_WithNullableStringOverride_ReturnsFub<StringIntMutable.Struct>();
		}

		private void Create_WithNullableStringOverride_ReturnsFub<T>() where T : IStringIntMutable
		{
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IStringIntMutable fub = fubber.Fub(f => f.String, "Henry");

			Assert.Equal("Henry", fub.String);
		}

		[Fact]
		public void Create_ClassWithIntOverride_ReturnsFub()
		{
			Create_WithIntOverride_ReturnsFub<StringIntMutable.Class>();
		}

		[Fact]
		public void Create_StructWithIntOverride_ReturnsFub()
		{
			Create_WithIntOverride_ReturnsFub<StringIntMutable.Struct>();
		}

		public void Create_WithIntOverride_ReturnsFub<T>() where T : IStringIntMutable
		{
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IStringIntMutable fub = fubber.Fub(m => m.Integer, 32);

			Assert.Equal(32, fub.Integer);
		}

		[Fact]
		public void Create_ClassWithNullableStringOverrideToNull_ReturnsFub()
		{
			Create_WithNullableStringOverrideToNull_ReturnsFub<StringIntMutable.Class>();
		}

		[Fact]
		public void Create_StructWithNullableStringOverrideToNull_ReturnsFub()
		{
			Create_WithNullableStringOverrideToNull_ReturnsFub<StringIntMutable.Struct>();
		}

		private void Create_WithNullableStringOverrideToNull_ReturnsFub<T>() where T : IStringIntMutable
		{
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IStringIntMutable fub = fubber.Fub(m => m.String, null);

			Assert.Null(fub.String);
		}

		[Fact]
		public void Create_ClassWithIntAndNullableStringOverrides_ReturnsFub()
		{
			Create_WithIntAndNullableStringOverrides_ReturnsFub<StringIntMutable.Class>();
		}

		[Fact]
		public void Create_StructWithIntAndNullableStringOverrides_ReturnsFub()
		{
			Create_WithIntAndNullableStringOverrides_ReturnsFub<StringIntMutable.Struct>();
		}

		private void Create_WithIntAndNullableStringOverrides_ReturnsFub<T>() where T : IStringIntMutable
		{
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IStringIntMutable fub = fubber.Fub(
				m => m.String, "Thomas",
				m => m.Integer, 42);

			Assert.Equal("Thomas", fub.String);
			Assert.Equal(42, fub.Integer);
		}

		[Fact]
		public void Create_ClassWithIntAndNullableStringDefaults_ReturnsFub()
		{
			Create_WithIntAndNullableStringDefaults_ReturnsFub<StringIntMutable.Class>();
		}

		[Fact]
		public void Create_StructWithIntAndNullableStringDefaults_ReturnsFub()
		{
			Create_WithIntAndNullableStringDefaults_ReturnsFub<StringIntMutable.Struct>();
		}

		private void Create_WithIntAndNullableStringDefaults_ReturnsFub<T>() where T : IStringIntMutable
		{
			FubberBuilder<T> builder = new();

			Fubber<T> noCtorMutable = builder
				.Make(m => m.String, "Katie")
				.Make(m => m.Integer, 22)
				.Build();

			IStringIntMutable fub = noCtorMutable.Fub();

			Assert.Equal("Katie", fub.String);
			Assert.Equal(22, fub.Integer);
		}
	}
}
