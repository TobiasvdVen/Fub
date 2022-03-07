namespace Fub.ValueProvisioning
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

	internal class FixedValueProvider<T> : IValueProvider, IValueProvider<T>
	{
		private readonly T value;

		public FixedValueProvider(T value)
		{
			this.value = value;
		}

		T IValueProvider<T>.GetValue()
		{
			return value;
		}

		object? IValueProvider.GetValue()
		{
			return ((IValueProvider<T>)this).GetValue();
		}
	}
}
