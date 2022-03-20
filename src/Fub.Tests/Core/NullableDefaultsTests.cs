using Fub.Tests.Models;
using Xunit;

namespace Fub.Tests.Core
{
	public class NullableDefaultsTests
	{
		public class Constructor
		{
			public Constructor(StringInt.Class thing, StringInt.Class? nullableThing)
			{
				Thing = thing;
				NullableThing = nullableThing;
			}

			public StringInt.Class Thing { get; }
			public StringInt.Class? NullableThing { get; }
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
			public Mutable(StringInt.Class thing)
			{
				Thing = thing;
			}

			public StringInt.Class Thing { get; set; }
			public StringInt.Class? NullableThing { get; set; }
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
