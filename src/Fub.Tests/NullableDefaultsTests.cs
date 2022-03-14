using Xunit;

namespace Fub.Tests
{
	public class NullableDefaultsTests
	{
		public class Thing
		{
			public Thing(int value)
			{
				Value = value;
			}

			public int Value { get; }
		}

		public class Constructor
		{
			public Constructor(Thing thing, Thing? nullableThing)
			{
				Thing = thing;
				NullableThing = nullableThing;
			}

			public Thing Thing { get; }
			public Thing? NullableThing { get; }
		}

		[Fact]
		public void Create_ClassWithConstructor_ReturnsFubWithMembersAccordingToNullability()
		{
			Fubber<Constructor> fubber = new FubberBuilder<Constructor>().Build();

			Constructor fub = fubber.Fub();

			Assert.NotNull(fub.Thing);
			Assert.Null(fub.NullableThing);
		}

		public class Mutable
		{
			public Mutable(Thing thing)
			{
				Thing = thing;
			}

			public Thing Thing { get; set; }
			public Thing? NullableThing { get; set; }
		}

		[Fact]
		public void Create_ClassWithNoConstructor_ReturnsFubWithMembersAccordingToNullability()
		{
			Fubber<Mutable> fubber = new FubberBuilder<Mutable>().Build();

			Mutable fub = fubber.Fub();

			Assert.NotNull(fub.Thing);
			Assert.Null(fub.NullableThing);
		}
	}
}
