﻿using Fub.Creation;
using Fub.Tests.Models;
using Fub.ValueProvisioning;
using Moq;
using System;
using System.Reflection;
using Xunit;

namespace Fub.Tests.Core
{
	public class FubberBuilderTests
	{
		[Fact]
		public void Build_Default_ReturnsFub()
		{
			FubberBuilder<Empty.Class> builder = new();

			Fubber<Empty.Class> fubber = builder.Build();

			Empty.Class fub = fubber.Fub();

			Assert.NotNull(fub);
		}

		[Fact]
		public void Build_WithCustomCreator_InjectsCreator()
		{
			FubberBuilder<Empty.Class> builder = new();
			Mock<ICreator> creator = new();

			Fubber<Empty.Class> fubber = builder.UseCreator(creator.Object).Build();

			fubber.Fub();

			creator.Verify(c => c.Create<Empty.Class>(It.IsAny<ProspectValues>()));
		}

		[Fact]
		public void UseConstructor_WithIncorrectConstructorInfo_Throws()
		{
			FubberBuilder<Empty.Class> builder = new();

			ConstructorInfo invalidConstructor = typeof(TwoConstructors).GetConstructor(new Type[] { typeof(int) })!;

			Assert.Throws<ArgumentException>(() => builder.UseConstructor(invalidConstructor));
		}

		public class SomeClass
		{
			public string Name { get; set; } = "Peter";
			public int Age { get; set; }
		}

		[Fact]
		public void Build_WithTwoDefaults_ReturnsFub()
		{
			FubberBuilder<SomeClass> builder = new();

			string expectedName = "Sarah";
			int expectedAge = 256;

			Fubber<SomeClass> fubber = builder
				.Make(s => s.Name, expectedName)
				.Make(s => s.Age, expectedAge)
				.Build();

			SomeClass fub = fubber.Fub();

			Assert.Equal(expectedName, fub.Name);
			Assert.Equal(expectedAge, fub.Age);
		}

		public class Goodbye
		{
			public void Method()
			{

			}

			public int Property { get; } = 128;
		}

		[Fact]
		public void WithDefault_InvalidExpression_Throws()
		{
			FubberBuilder<Goodbye> builder = new();

			Assert.Throws<ArgumentException>(() => builder.Make(g => "", ""));
		}
	}
}
