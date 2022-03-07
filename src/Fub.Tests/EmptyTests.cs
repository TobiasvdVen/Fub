using Xunit;

namespace Fub.Tests
{
	public class EmptyTests
	{
		private interface IEmpty
		{

		}

		private class Class : IEmpty
		{
		}

		private struct Struct : IEmpty
		{
		}

		[Fact]
		public void GivenClassThenShouldCreate()
		{
			CreateAndAssert<Class>();
		}

		[Fact]
		public void GivenStructThenShouldCreate()
		{
			CreateAndAssert<Struct>();
		}

#if NET5_0_OR_GREATER
		public record Record(string String, int Integer) : IEmpty
		{

		}

		public record struct RecordStruct(string String, int Integer) : IEmpty
		{

		}

		[Fact]
		public void GivenRecordThenCreate()
		{
			CreateAndAssert<Record>();
		}



		[Fact]
		public void GivenRecordStructThenCreate()
		{
			CreateAndAssert<RecordStruct>();
		}
#endif

		private void CreateAndAssert<T>() where T : IEmpty
		{
			FubBuilder<T> builder = new FubBuilder<T>();
			Fub<T> fub = builder.Build();

			IEmpty created = fub.Create();

			Assert.IsType<T>(created);
		}
	}
}
