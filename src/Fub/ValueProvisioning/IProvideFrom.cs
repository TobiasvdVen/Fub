using Fub.ValueProvisioning.Randomization;

namespace Fub.ValueProvisioning
{
	public interface IProvideFrom<T>
	{
		IValueProvider<T> Randomly(IRandom? random);
	}
}
