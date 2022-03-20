namespace Fub.Tests.Models
{
	public class TwoConstructors
	{
		public TwoConstructors()
		{
			Value = 1;
		}

		public TwoConstructors(int value)
		{
			Value = value;
		}

		public int Value { get; }
	}
}
