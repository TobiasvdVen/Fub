namespace Fub.Tests.Models
{
	public interface ISimpleComposite
	{
		string Name { get; }
		bool Boolean { get; }
		Empty.Class Empty { get; }
	}

	public static class SimpleComposite
	{
		public class Class : ISimpleComposite
		{
			public Class(string name, bool boolean, Empty.Class empty)
			{
				Name = name;
				Boolean = boolean;
				Empty = empty;
			}

			public string Name { get; }
			public bool Boolean { get; }
			public Empty.Class Empty { get; }
		}

		public struct Struct : ISimpleComposite
		{
			public Struct(string name, bool boolean, Empty.Class empty)
			{
				Name = name;
				Boolean = boolean;
				Empty = empty;
			}

			public string Name { get; }
			public bool Boolean { get; }
			public Empty.Class Empty { get; }
		}
	}
}
