using Fub.Prospects;
using Fub.ValueProvisioning;
using Fub.ValueProvisioning.ValueProviders;
using System;
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
			ProspectValues prospectValues = new();

			PropertyInfo property = typeof(Base).GetProperty(nameof(Base.SomeBoolean))!;
			PropertyInfo otherProperty = typeof(OtherButSameInterface).GetProperty(nameof(OtherButSameInterface.SomeBoolean))!;

			Prospect prospect = new PropertyProspect(property);
			Prospect otherProspect = new PropertyProspect(otherProperty);

			prospectValues.SetProvider(prospect, new FixedValueProvider(true));

			Assert.False(prospectValues.TryGetProvider(otherProspect, out IValueProvider? valueProvider));
		}

		[Fact]
		public void Combine_WithEmptyValues_ReturnsUnchanged()
		{
			ProspectValues prospectValues = new();
			ProspectValues other = new();

			PropertyInfo property = typeof(Base).GetProperty(nameof(Base.SomeBoolean))!;
			Prospect prospect = new PropertyProspect(property);

			prospectValues.SetProvider(prospect, new FixedValueProvider(false));

			ProspectValues combined = ProspectValues.Combine(prospectValues, other);

			Assert.False(ReferenceEquals(prospectValues, combined));
			Assert.Equal(prospectValues, combined);
		}

		[Fact]
		public void Combine_WithIdenticalProspect_Throws()
		{
			ProspectValues prospectValues = new();
			ProspectValues other = new();

			PropertyInfo property = typeof(Base).GetProperty(nameof(Base.SomeBoolean))!;
			Prospect prospect = new PropertyProspect(property);
			Prospect sameProspect = new PropertyProspect(property);

			prospectValues.SetProvider(prospect, new FixedValueProvider(false));
			other.SetProvider(sameProspect, new FixedValueProvider(true));

			Assert.Throws<ArgumentException>(() => ProspectValues.Combine(prospectValues, other));
		}

		class Combine
		{
			public Combine(string text, Base @base)
			{
				Text = text;
				Base = @base;
			}

			public string Text { get; }
			public Base Base { get; }
		}

		[Fact]
		public void Combine_WithDistinctProspects_ReturnsCombined()
		{
			ProspectValues prospectValues = new();
			ProspectValues other = new();

			PropertyInfo textProperty = typeof(Combine).GetProperty(nameof(Combine.Text))!;
			PropertyInfo baseProperty = typeof(Combine).GetProperty(nameof(Combine.Base))!;

			Prospect textProspect = new PropertyProspect(textProperty);
			Prospect baseProspect = new PropertyProspect(baseProperty);

			prospectValues.SetProvider(textProspect, new FixedValueProvider("Text"));
			other.SetProvider(baseProspect, new FixedValueProvider(new Base()));

			ProspectValues combined = ProspectValues.Combine(prospectValues, other);

			Assert.True(combined.HasProvider(textProspect));
			Assert.True(combined.HasProvider(baseProspect));
		}
	}
}
