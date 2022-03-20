namespace Fub.ValueProvisioning.ValueProviders
{
	internal class FixedValueProvider : IValueProvider
	{
		private readonly object? value;

		public FixedValueProvider(object? value)
		{
			this.value = value;
		}

		public object? GetValue()
		{
			return value;
		}
	}

	internal class FixedValueProvider<T> : GenericProvider<T>
	{
		private readonly T value;

		public FixedValueProvider(T value)
		{
			this.value = value;
		}

		public override T GetValue()
		{
			return value;
		}
	}
}
