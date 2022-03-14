using System;

namespace Fub
{
	public class FubException : Exception
	{
		public FubException(string? message) : base(message)
		{
		}

		public FubException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
