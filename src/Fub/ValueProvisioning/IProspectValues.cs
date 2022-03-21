using Fub.Prospects;
using System.Diagnostics.CodeAnalysis;

namespace Fub.ValueProvisioning
{
	/// <summary>
	/// IProspectValues represents a collection of values to initialize a fub with during creation.
	/// A base set of values can be specified while building a Fubber, which can be further overridden
	/// through the Fubber or static Fub APIs.
	/// </summary>
	public interface IProspectValues
	{
#if NET5_0_OR_GREATER
		bool TryGetProvider(Prospect prospect, [NotNullWhen(true)] out IValueProvider? valueProvider);
#else
		bool TryGetProvider(Prospect prospect, out IValueProvider? valueProvider);
#endif
		bool HasProvider(Prospect prospect);

		void SetProvider(Prospect prospect, IValueProvider provider);

		IProspectValues Clone();

		bool IsEmpty { get; }
	}
}
