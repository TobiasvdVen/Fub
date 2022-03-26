using Fub.Prospects;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Fub.ValueProvisioning
{
	/// <summary>
	/// ProspectValues represents a collection of values to initialize a fub with during creation.
	/// A base set of values can be specified while building a Fubber, which can be further overridden
	/// through the Fubber or static Fub APIs.
	/// </summary>
	public class ProspectValues : IEnumerable<KeyValuePair<Prospect, IValueProvider>>
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

		public ProspectValues Clone()
		{
			return new ProspectValues(new Dictionary<Prospect, IValueProvider>(valueProviders));
		}

		public static ProspectValues Combine(ProspectValues a, ProspectValues b)
		{
			Dictionary<Prospect, IValueProvider> result = a.Union(b).ToDictionary(e => e.Key, e => e.Value);

			return new ProspectValues(result);
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
					if (valueProviders.TryGetValue(Prospect.FromMember(parameterProspect.MatchingMember!), out valueProvider))
					{
						return true;
					}

					return valueProviders.TryGetValue(parameterProspect, out valueProvider);
				}
			}

			return valueProviders.TryGetValue(prospect, out valueProvider);
		}

		public bool HasProvider(Prospect prospect)
		{
			return TryGetProvider(prospect, out IValueProvider? _);
		}

		public IEnumerator<KeyValuePair<Prospect, IValueProvider>> GetEnumerator()
		{
			return valueProviders.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)valueProviders).GetEnumerator();
		}
	}
}
