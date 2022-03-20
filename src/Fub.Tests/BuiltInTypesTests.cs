using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests
{
	public class BuiltInTypesTests
	{
		[Fact]
		public void Create_ClassWithNoOverrides_ReturnsDefault()
		{
			CreateAndAssertDefault<BuiltInTypes.Class>();
		}

		[Fact]
		public void Create_MostlyMutableClassWithNoOverrides_ReturnsDefault()
		{
			CreateAndAssertDefault<BuiltInTypes.MostlyMutableClass>();
		}

		private void CreateAndAssertDefault<T>() where T : IBuiltInTypes
		{
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			IBuiltInTypes fub = fubber.Fub();

			Assert.Equal(default, fub.Bool);
			Assert.Equal(default, fub.Byte);
			Assert.Equal(default, fub.SByte);
			Assert.Equal(default, fub.Char);
			Assert.Equal(default, fub.Decimal);
			Assert.Equal(default, fub.Double);
			Assert.Equal(default, fub.Float);
			Assert.Equal(default, fub.Int);
			Assert.Equal(default, fub.UInt);
			Assert.Equal(default, fub.NInt);
			Assert.Equal(default, fub.NUInt);
			Assert.Equal(default, fub.Long);
			Assert.Equal(default, fub.ULong);
			Assert.Equal(default, fub.Short);
			Assert.Equal(default, fub.UShort);

			Assert.NotNull(fub.Object);

			Assert.NotNull(fub.String);
			Assert.Equal(string.Empty, fub.String);

			Assert.Equal(default, fub.DateTime);
		}
	}
}
