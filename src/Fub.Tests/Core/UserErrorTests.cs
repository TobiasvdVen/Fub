using Fub.Creation;
using Fub.Tests.Models;
using Fub.ValueProvisioning;
using Moq;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Fub.Tests.Core
{
	public class UserErrorTests
	{
		private class HasMethod
		{
			public void MyMethod()
			{

			}
		}

		[Fact]
		public void Create_WithNonPropertyOrFieldExpression_Throws()
		{
			Fubber<HasMethod> fubber = new FubberBuilder<HasMethod>().Build();

			ArgumentException ex = Assert.Throws<ArgumentException>(() => fubber.Fub(f => "", ""));

			Assert.Equal($"Expression must be a {nameof(MemberExpression)}.", ex.Message);
		}

		public interface IMyInterface
		{

		}

		[Fact]
		public void FubberBuilder_ForInterface_Throws()
		{
			Assert.Throws<InvalidOperationException>(() => new FubberBuilder<IMyInterface>());
		}

		[Fact]
		public void Fub_ForInterface_Throws()
		{
			Mock<ICreator> creator = new();
			Mock<IProspectValues> prospectValues = new();

			Assert.Throws<InvalidOperationException>(() => new Fubber<IMyInterface>(creator.Object, prospectValues.Object));
		}

		[Fact]
		public void Fub_NullForNonNullable_Throws()
		{
			FubberBuilder<StringInt.Class> builder = new();
			Fubber<StringInt.Class> fubber = builder.Build();

			Assert.Throws<ArgumentException>(() => fubber.Fub(f => f.String, null));
		}

		[Fact]
		public void Make_NullForNonNullable_Throws()
		{
			FubberBuilder<StringInt.Class> builder = new();

			Assert.Throws<ArgumentException>(() =>
			{
				builder.Make(f => f.String, null);
			});
		}
	}
}
