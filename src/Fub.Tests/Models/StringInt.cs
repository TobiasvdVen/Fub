namespace Fub.Tests.Models
{
	public interface IStringInt
	{
		string String { get; }
		int Integer { get; }
	}

	public static class StringInt
	{
		public class Class : IStringInt
		{
			public Class(string @string, int integer)
			{
				String = @string;
				Integer = integer;
			}

			public string String { get; }
			public int Integer { get; }
		}

		public struct Struct : IStringInt
		{
			public Struct(string @string, int integer)
			{
				String = @string;
				Integer = integer;
			}

			public string String { get; }
			public int Integer { get; }
		}

#if NET5_0_OR_GREATER
		public record Record(string String, int Integer) : IStringInt
		{

		}

		public record struct RecordStruct(string String, int Integer) : IStringInt
		{

		}
#endif

		public struct StructWithNoConstructor : IStringInt
		{
			public string String { get; }
			public int Integer { get; }
		}
	}
}
