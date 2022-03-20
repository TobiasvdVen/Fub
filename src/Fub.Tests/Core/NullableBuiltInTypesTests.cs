using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests.Core
{
	public class NullableBuiltInTypesTests
	{
		[Fact]
		public void Create_ClassWithNoOverrides_ReturnsDefault()
		{
			CreateAndAssertDefault<NullableBuiltInTypes.Class>();
		}

		[Fact]
		public void Create_MutableClassWithNoOverrides_ReturnsDefault()
		{
			CreateAndAssertDefault<NullableBuiltInTypes.MutableClass>();
		}

		[Fact]
		public void Create_ClassWithConstructorWithNoOverrides_ReturnsDefault()
		{
			CreateAndAssertDefault<NullableBuiltInTypes.ClassWithConstructor>();
		}

#if NET5_0_OR_GREATER
		[Fact]
		public void Create_RecordWithNoOverrides_ReturnsDefault()
		{
			CreateAndAssertDefault<NullableBuiltInTypes.Record>();
		}

		[Fact]
		public void Create_RecordStructWithNoOverrides_ReturnsDefault()
		{
			CreateAndAssertDefault<NullableBuiltInTypes.RecordStruct>();
		}
#endif

		private void CreateAndAssertDefault<T>() where T : INullableBuiltInTypes
		{
			FubberBuilder<T> builder = new();
			Fubber<T> fubber = builder.Build();

			INullableBuiltInTypes fub = fubber.Fub();

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

			Assert.Null(fub.Object);
			Assert.Null(fub.String);
		}
	}
}
