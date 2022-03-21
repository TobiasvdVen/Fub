using Fub.Prospects;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Fub.ValueProvisioning
{
	internal class ProspectValues : IProspectValues
	{
		public bool IsEmpty => !valueProviders.Any();

		private readonly IDictionary<Prospect, IValueProvider> valueProviders;

		public ProspectValues() : this(new Dictionary<Prospect, IValueProvider>())
		{
		}

		private ProspectValues(IDictionary<Prospect, IValueProvider> valueProviders)
		{
			this.valueProviders = valueProviders;
		}

		public IProspectValues Clone()
		{
			return new ProspectValues(new Dictionary<Prospect, IValueProvider>(valueProviders));
		}

		public void SetProvider(Prospect prospect, IValueProvider provider)
		{
			valueProviders[prospect] = provider;
		}

#if NET5_0_OR_GREATER
		public bool TryGetProvider(Prospect prospect, [NotNullWhen(true)] out IValueProvider? valueProvider)
#else
		public bool TryGetProvider(Prospect prospect, out IValueProvider? valueProvider)
#endif
		{
			if (prospect is ParameterProspect parameterProspect)
			{
				if (parameterProspect.MatchingMember != null)
				{
					return valueProviders.TryGetValue(Prospect.FromMember(parameterProspect.MatchingMember!), out valueProvider);
				}
			}

			return valueProviders.TryGetValue(prospect, out valueProvider);
		}

		public bool HasProvider(Prospect prospect)
		{
			return TryGetProvider(prospect, out IValueProvider? _);
		}
	}
}
