using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests
{
	public class SequentialSelectionTests
	{
		[Fact]
		public void Fub_GivenSelection_ReturnsFubsSequentially()
		{
			FubberBuilder<StringInt.Class> builder = new();
			Fubber<StringInt.Class> fubber = builder
				.For(c => c.Integer, Provide.From(new int[] { 1, 2, 3 }).Sequentially())
				.Build();

			StringInt.Class fub1 = fubber.Fub();
			StringInt.Class fub2 = fubber.Fub();
			StringInt.Class fub3 = fubber.Fub();

			Assert.Equal(1, fub1.Integer);
			Assert.Equal(2, fub2.Integer);
			Assert.Equal(3, fub3.Integer);
		}
	}
}
