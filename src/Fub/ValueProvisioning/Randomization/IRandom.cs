namespace Fub.ValueProvisioning.Randomization
{
	/// <summary>
	/// IRandom provides a basic abstraction based on System.Random, for the purposes of mocking.
	/// </summary>
	public interface IRandom
	{
		int Next();
		int Next(int maxValue);
		int Next(int minValue, int maxValue);
	}
}
