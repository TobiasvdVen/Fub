namespace Fub.Tests.Models
{
	public interface IStringIntMutable
	{
		string? String { get; set; }
		int Integer { get; set; }
	}

	public static class StringIntMutable
	{
		public class Class : IStringIntMutable
		{
			public string? String { get; set; }
			public int Integer { get; set; }
		}

		public struct Struct : IStringIntMutable
		{
			public string? String { get; set; }
			public int Integer { get; set; }
		}
	}
}
