using System;

namespace Fub.ValueProvisioning.Randomization
{
	public class RandomAdapter : IRandom
	{
		private readonly Random random;

		public RandomAdapter(Random random)
		{
			this.random = random;
		}

		public int Next()
		{
			return random.Next();
		}

		public int Next(int maxValue)
		{
			return random.Next(maxValue);
		}

		public int Next(int minValue, int maxValue)
		{
			return random.Next(minValue, maxValue);
		}
	}
}
