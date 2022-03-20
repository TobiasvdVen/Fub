using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests
{
	public class EmptyTests
	{
		[Fact]
		public void Create_Class_ReturnsDefault()
		{
			CreateAndAssert<Empty.Class>();
		}

		[Fact]
		public void Create_Struct_ReturnsDefault()
		{
			CreateAndAssert<Empty.Struct>();
		}

#if NET5_0_OR_GREATER
		[Fact]
		public void Create_Record_ReturnsDefault()
		{
			CreateAndAssert<Empty.Record>();
		}

		[Fact]
		public void Create_RecordStruct_ReturnsDefault()
		{
			CreateAndAssert<Empty.RecordStruct>();
		}
#endif

		private void CreateAndAssert<T>() where T : IEmpty
		{
			FubberBuilder<T> builder = new FubberBuilder<T>();
			Fubber<T> fubber = builder.Build();

			IEmpty fub = fubber.Fub();

			Assert.IsType<T>(fub);
		}
	}
}
