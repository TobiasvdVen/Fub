﻿namespace Fub.ValueProvisioning.Randomization
{
	public interface IRandom
	{
		int Next();
		int Next(int maxValue);
		int Next(int minValue, int maxValue);
	}
}