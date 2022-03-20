using Fub.ValueProvisioning.Randomization;

namespace Fub.ValueProvisioning
{
	/// <summary>
	/// IProvideFrom is used to specify whether the values passed in a <c>Provide.From()</c> call 
	/// should be selected randomly or sequentially.
	/// </summary>
	public interface IProvideFrom<T>
	{
		IValueProvider<T> Randomly(IRandom? random);
		IValueProvider<T> Sequentially();
	}
}
