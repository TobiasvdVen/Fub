﻿using Xunit;

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
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IStringIntMutable fub = fubber.Fub();

			Assert.Null(fub.String);
			Assert.Equal(default, fub.Integer);
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
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IStringIntMutable fub = fubber.Fub(f => f.String, "Henry");

			Assert.Equal("Henry", fub.String);
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
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IStringIntMutable fub = fubber.Fub(m => m.Integer, 32);

			Assert.Equal(32, fub.Integer);
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
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IStringIntMutable fub = fubber.Fub(m => m.String, null);

			Assert.Null(fub.String);
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
			Create_WithIntAndNullableStringDefaults_ReturnsFub<Class>();
		}

		[Fact]
		public void Create_StructWithIntAndNullableStringDefaults_ReturnsFub()
		{
			Create_WithIntAndNullableStringDefaults_ReturnsFub<Struct>();
		}

		private void Create_WithIntAndNullableStringDefaults_ReturnsFub<T>() where T : IStringIntMutable
		{
			FubberBuilder<T> builder = new();

			Fubber<T> noCtorMutable = builder
				.WithDefault(m => m.String, "Katie")
				.WithDefault(m => m.Integer, 22)
				.Build();

			IStringIntMutable fub = noCtorMutable.Fub();

			Assert.Equal("Katie", fub.String);
			Assert.Equal(22, fub.Integer);
		}
	}
}
