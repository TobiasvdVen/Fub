using System;
using System.Linq.Expressions;
using Xunit;

namespace Fub.Tests
{
	public class UserErrorTests
	{
		private class HiThere
		{
			public void MyMethod()
			{

			}
		}

		[Fact]
		public void Create_WithNonPropertyOrFieldExpression_Throws()
		{
			Fub<HiThere> fub = new FubBuilder<HiThere>().Build();

			InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => fub.Create(f => "", ""));

			Assert.Equal($"Expression must be a {nameof(MemberExpression)}.", ex.Message);
		}
	}
}
