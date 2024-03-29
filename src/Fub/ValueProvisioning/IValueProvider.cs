﻿namespace Fub.ValueProvisioning
{
	public interface IValueProvider
	{
		object? GetValue();
	}

	public interface IValueProvider<T> : IValueProvider
	{
		new T GetValue();
	}
}
