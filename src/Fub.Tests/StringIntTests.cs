using Xunit;

namespace Fub.Tests
{
	public class StringIntTests
	{
		public interface IStringInt
		{
			string String { get; }
			int Integer { get; }
		}

		public class Class : IStringInt
		{
			public Class(string @string, int integer)
			{
				String = @string;
				Integer = integer;
			}

			public string String { get; }
			public int Integer { get; }
		}

		public struct Struct : IStringInt
		{
			public Struct(string @string, int integer)
			{
				String = @string;
				Integer = integer;
			}

			public string String { get; }
			public int Integer { get; }
		}

		[Fact]
		public void GivenClassWhenNoOverrideThenCreateDefault()
		{
			CreateAndAssertDefault<Class>();
		}

		[Fact]
		public void GivenStructWhenNoOverrideThenCreateDefault()
		{
			CreateAndAssertDefault<Struct>();
		}

#if NET5_0_OR_GREATER
		public record Record(string String, int Integer) : IStringInt
		{

		}

		public record struct RecordStruct(string String, int Integer) : IStringInt
		{

		}

		[Fact]
		public void GivenRecordWhenNoOverrideThenCreateDefault()
		{
			CreateAndAssertDefault<Record>();
		}

		[Fact]
		public void GivenRecordStructWhenNoOverrideThenCreateDefault()
		{
			CreateAndAssertDefault<RecordStruct>();
		}
#endif

		private void CreateAndAssertDefault<T>() where T : IStringInt
		{
			FubBuilder<T> builder = new();
			Fub<T> fub = builder.Build();

			IStringInt something = fub.Create();

			Assert.NotNull(something.String);
			Assert.Equal(string.Empty, something.String);
			Assert.Equal(default, something.Integer);
		}

		public struct StructWithNoConstructor : IStringInt
		{
			public string String { get; }
			public int Integer { get; }
		}

		[Fact]
		public void GivenStructWithNoConstructorWhenNoOverrideThenCreateDefault()
		{
			FubBuilder<StructWithNoConstructor> builder = new();
			Fub<StructWithNoConstructor> fub = builder.Build();

			StructWithNoConstructor something = fub.Create();

			// Non-nullable reference types in structs are still initialized to null when not explicitly initialized, and this is not prevented by the nullability analyzer
			// See: https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references#structs
			Assert.Null(something.String);
			Assert.Equal(default, something.Integer);
		}
	}
}
