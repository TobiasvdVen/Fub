using System.Linq;
using System.Reflection;
using Xunit;

namespace Fub.InternalTests
{
	public class AssumptionsTests
	{
		public class Hello
		{
			public int SomeMember { get; } = 8;
		}

		[Fact]
		public void GivenMembersFromDifferentInstancesThenBeEqual()
		{
			Hello hello1 = new();
			Hello hello2 = new();

			MemberInfo info1 = hello1.GetType().GetMembers().First();
			MemberInfo info2 = hello2.GetType().GetMembers().First();

			Assert.Equal(info1, info2);
		}

		public class NullableProperty
		{
			public NullableProperty(Hello? nullable)
			{
				Nullable = nullable;
			}

			public Hello? Nullable { get; }
		}

		[Fact]
		public void GivenNullablePropertyThenDetermineIsNullable()
		{
			PropertyInfo propertyInfo = typeof(NullableProperty).GetProperty(nameof(NullableProperty.Nullable))!;

			Assert.True(propertyInfo.IsNullable());
		}

		public class NonNullableProperty
		{
			public NonNullableProperty(Hello notNullable)
			{
				NotNullable = notNullable;
			}

			public Hello NotNullable { get; }
		}

		[Fact]
		public void GivenNonNullablePropertyThenDetermineIsNotNullable()
		{
			PropertyInfo propertyInfo = typeof(NonNullableProperty).GetProperty(nameof(NonNullableProperty.NotNullable))!;

			Assert.False(propertyInfo.IsNullable());
		}

		public class NullableField
		{
			public NullableField(Hello? nullable)
			{
				this.nullable = nullable;
			}

			public readonly Hello? nullable;
		}

		[Fact]
		public void GivenNullableFieldThenDetermineIsNullable()
		{
			FieldInfo fieldInfo = typeof(NullableField).GetField(nameof(NullableField.nullable))!;

			Assert.True(fieldInfo.IsNullable());
		}

		public class NonNullableField
		{
			public NonNullableField(Hello notNullable)
			{
				this.notNullable = notNullable;
			}

			public readonly Hello notNullable;
		}

		[Fact]
		public void GivenNonNullableFieldThenDetermineIsNotNullable()
		{
			FieldInfo fieldInfo = typeof(NonNullableField).GetField(nameof(NonNullableField.notNullable))!;

			Assert.False(fieldInfo.IsNullable());
		}

		[Fact]
		public void GivenNullableParameterThenDetermineIsNullable()
		{
			ConstructorInfo constructor = typeof(NullableField).GetConstructors().Single();

			ParameterInfo parameterInfo = constructor.GetParameters().Where(p => p.Name == "nullable").Single();

			Assert.True(parameterInfo.IsNullable());
		}

		[Fact]
		public void GivenNonNullableParameterThenDetermineIsNotNullable()
		{
			ConstructorInfo constructor = typeof(NonNullableField).GetConstructors().Single();

			ParameterInfo parameterInfo = constructor.GetParameters().Where(p => p.Name == "notNullable").Single();

			Assert.False(parameterInfo.IsNullable());
		}

		public class BothProperties
		{
			public BothProperties(Hello notNullable, Hello? nullable)
			{
				NotNullable = notNullable;
				Nullable = nullable;
			}

			public Hello NotNullable { get; }
			public Hello? Nullable { get; }
		}

		[Fact]
		public void GivenBothNullableAndNotPropertyThenDetermineNullability()
		{
			PropertyInfo nullable = typeof(BothProperties).GetProperty(nameof(BothProperties.Nullable))!;
			PropertyInfo notNullable = typeof(BothProperties).GetProperty(nameof(BothProperties.NotNullable))!;

			Assert.True(nullable.IsNullable());
			Assert.False(notNullable.IsNullable());
		}
	}
}
