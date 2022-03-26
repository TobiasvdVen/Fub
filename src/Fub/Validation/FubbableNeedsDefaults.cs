using Fub.ValueProvisioning;

namespace Fub.Validation
{
	internal class FubbableNeedsDefaults : FubbableResult
	{
		public FubbableNeedsDefaults(ProspectValues requiredDefaults) : base(true)
		{
			RequiredDefaults = requiredDefaults;
		}

		public ProspectValues RequiredDefaults { get; }
	}
}
