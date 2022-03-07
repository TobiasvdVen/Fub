using System.Collections.Generic;

namespace Fub.InternalTests
{
	internal class ObjectComparer : IEqualityComparer<object>
	{
		public new bool Equals(object? x, object? y)
		{
			if (x == null && y == null)
			{
				return true;
			}

			if (x == null || y == null)
			{
				return false;
			}

			return x.GetType() == typeof(object) && y.GetType() == typeof(object);
		}

		public int GetHashCode(object obj)
		{
			return obj.GetHashCode();
		}
	}
}
