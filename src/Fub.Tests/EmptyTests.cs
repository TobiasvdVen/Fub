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
		public void Create_Class_ReturnsDefault()
		{
			CreateAndAssert<Class>();
		}

		[Fact]
		public void Create_Struct_ReturnsDefault()
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
		public void Create_Record_ReturnsDefault()
		{
			CreateAndAssert<Record>();
		}



		[Fact]
		public void Create_RecordStruct_ReturnsDefault()
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
