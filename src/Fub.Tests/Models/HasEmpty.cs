namespace Fub.Tests.Models
{
	public interface IHasEmpty
	{
		public Empty.Class Empty { get; }
	}

	public static class HasEmpty
	{
		public class Class : IHasEmpty
		{
			public Class(Empty.Class empty)
			{
				Empty = empty;
			}

			public Empty.Class Empty { get; }
		}

		public struct Struct : IHasEmpty
		{
			public Struct(Empty.Class empty)
			{
				Empty = empty;
			}

			public Empty.Class Empty { get; }
		}
	}
}
