namespace Fub.Validation
{
	internal class FubbableError : FubbableResult
	{
		public FubbableError(string message) : base(ok: false)
		{
			Message = message;
		}

		public string Message { get; }
	}
}
