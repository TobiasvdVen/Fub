namespace Fub.Tests.Models
{
	public interface IEmpty
	{

	}

	public static class Empty
	{
		public class Class : IEmpty
		{
		}

		public struct Struct : IEmpty
		{
		}

#if NET5_0_OR_GREATER
		public record Record(string String, int Integer) : IEmpty
		{
		}

		public record struct RecordStruct(string String, int Integer) : IEmpty
		{
		}
#endif
	}
}
