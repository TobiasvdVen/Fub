namespace Fub.ValueProvisioning.ValueProviders
{
	public abstract class GenericProvider<T> : IValueProvider<T>
	{
		public abstract T GetValue();

		object? IValueProvider.GetValue()
		{
			return ((IValueProvider<T>)this).GetValue();
		}
	}
}
