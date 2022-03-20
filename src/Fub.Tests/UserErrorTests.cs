using Fub.Creation;
using Fub.ValueProvisioning;
using Moq;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Fub.Tests
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

			InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => fubber.Fub(f => "", ""));

			Assert.Equal($"Expression must be a {nameof(MemberExpression)}.", ex.Message);
		}

		public interface IMyInterface
		{

		}

		[Fact]
		public void FubBuilder_ForInterface_Throws()
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
	}
}
