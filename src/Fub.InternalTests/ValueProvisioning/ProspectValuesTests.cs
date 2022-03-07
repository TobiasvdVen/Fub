using Fub.Prospects;
using Fub.ValueProvisioning;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Fub.InternalTests.ValueProvidersTests
{
	public class ProspectValuesTests
	{
		public class Goodbye
		{
			public void Method()
			{

			}

			public int Property { get; } = 128;
		}

		[Fact]
		public void GivenEmptyProspectValuesWhenCheckedThenReturnEmpty()
		{
			ProspectValues memberValues = new ProspectValues();

			Assert.True(memberValues.IsEmpty);
		}

		[Fact]
		public void GivenMemberValuesWhenCheckedThenReturnNotEmpty()
		{
			ProspectValues memberValues = new ProspectValues();

			PropertyInfo propertyInfo = (PropertyInfo)typeof(Goodbye).GetMember(nameof(Goodbye.Property)).First();

			memberValues.SetProvider(new PropertyProspect(propertyInfo), new FixedValueProvider(2));

			Assert.False(memberValues.IsEmpty);
		}
	}
}
