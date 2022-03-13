using Fub.Prospects;
using Fub.ValueProvisioning;
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
		public void IsEmpty_WhenNothingSet_ReturnsTrue()
		{
			ProspectValues prospectValues = new ProspectValues();

			Assert.True(prospectValues.IsEmpty);
		}

		[Fact]
		public void IsEmpty_WhenProviderSet_ReturnsFalse()
		{
			ProspectValues prospectValues = new ProspectValues();

			PropertyInfo propertyInfo = typeof(Goodbye).GetProperty(nameof(Goodbye.Property))!;

			prospectValues.SetProvider(new PropertyProspect(propertyInfo), new FixedValueProvider(2));

			Assert.False(prospectValues.IsEmpty);
		}

		public interface IBase
		{
			public bool SomeBoolean { get; }
		}

		public class Base : IBase
		{
			public bool SomeBoolean { get; }
		}

		[Fact]
		public void TryGetProvider_WithConcreteProspect_WhenSetWithInterfaceProspect_ReturnsTrue()
		{
			ProspectValues prospectValues = new ProspectValues();

			PropertyInfo interfaceProperty = typeof(IBase).GetProperty(nameof(IBase.SomeBoolean))!;
			PropertyInfo classProperty = typeof(Base).GetProperty(nameof(Base.SomeBoolean))!;

			Prospect interfaceProspect = new PropertyProspect(interfaceProperty);
			Prospect classProspect = new PropertyProspect(classProperty);

			prospectValues.SetProvider(interfaceProspect, new FixedValueProvider(true));

			Assert.True(prospectValues.TryGetProvider(classProspect, out IValueProvider? valueProvider));
		}

		public class Other
		{
			public bool SomeBoolean { get; }
		}

		[Fact]
		public void TryGetProvider_WithProspect_WhenSetWithOtherTypeProspect_ReturnsFalse()
		{
			ProspectValues prospectValues = new ProspectValues();

			PropertyInfo property = typeof(Base).GetProperty(nameof(Base.SomeBoolean))!;
			PropertyInfo otherProperty = typeof(Other).GetProperty(nameof(Other.SomeBoolean))!;

			Prospect prospect = new PropertyProspect(property);
			Prospect otherProspect = new PropertyProspect(otherProperty);

			prospectValues.SetProvider(prospect, new FixedValueProvider(true));

			Assert.False(prospectValues.TryGetProvider(otherProspect, out IValueProvider? valueProvider));
		}

		public class OtherButSameInterface : IBase
		{
			public bool SomeBoolean { get; }
		}

		[Fact]
		public void TryGetProvider_WithProspect_WhenSetWithOtherTypeProspectButSameInterface_ReturnsFalse()
		{
			ProspectValues prospectValues = new ProspectValues();

			PropertyInfo property = typeof(Base).GetProperty(nameof(Base.SomeBoolean))!;
			PropertyInfo otherProperty = typeof(OtherButSameInterface).GetProperty(nameof(OtherButSameInterface.SomeBoolean))!;

			Prospect prospect = new PropertyProspect(property);
			Prospect otherProspect = new PropertyProspect(otherProperty);

			prospectValues.SetProvider(prospect, new FixedValueProvider(true));

			Assert.False(prospectValues.TryGetProvider(otherProspect, out IValueProvider? valueProvider));
		}
	}
}
