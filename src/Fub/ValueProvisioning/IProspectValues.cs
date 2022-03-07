using Fub.Prospects;
using System.Diagnostics.CodeAnalysis;

namespace Fub.ValueProvisioning
{
	public interface IProspectValues
	{
#if NET5_0_OR_GREATER
		bool TryGetProvider(Prospect prospect, [NotNullWhen(true)] out IValueProvider? valueProvider);
#else
		bool TryGetProvider(Prospect prospect, out IValueProvider? valueProvider);
#endif

		void SetProvider(Prospect prospect, IValueProvider provider);

		IProspectValues Clone();

		bool IsEmpty { get; }
	}
}
