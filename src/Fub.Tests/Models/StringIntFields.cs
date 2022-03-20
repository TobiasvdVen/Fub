namespace Fub.Tests.Models
{
	public static class StringIntFields
	{
		public class Class
		{
			public string @string;
			public int integer;

			public Class(string @string, int integer)
			{
				this.@string = @string;
				this.integer = integer;
			}
		}

		public struct Struct
		{
			public string @string;
			public int integer;
		}

		public struct StructWithConstructor
		{
			public string @string;
			public int integer;

			public StructWithConstructor(string @string, int integer)
			{
				this.@string = @string;
				this.integer = integer;
			}
		}

		public class ClassWithoutConstructor
		{
			public string? @string;
			public int integer;
		}
	}
}
