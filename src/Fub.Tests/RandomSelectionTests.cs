using Fub.Tests.Models;
using Fub.ValueProvisioning.Randomization;
using Moq;
using Xunit;

namespace Fub.Tests
{
	public class RandomSelectionTests
	{
		[Theory]
		[InlineData(0, 1)]
		[InlineData(1, 2)]
		[InlineData(2, 3)]
		public void Fub_GivenSelection_ReturnsFubWithRandom(int index, int expected)
		{
			Mock<IRandom> random = new();
			random.Setup(random => random.Next(2)).Returns(index);

			FubberBuilder<StringInt.Class> builder = new();
			Fubber<StringInt.Class> fubber = builder
				.For(c => c.Integer, Provide.From(new int[] { 1, 2, 3 }).Randomly(random.Object))
				.Build();

			StringInt.Class fub = fubber.Fub();

			Assert.Equal(expected, fub.Integer);
		}
	}
}
