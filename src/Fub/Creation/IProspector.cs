using Fub.Prospects;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fub.Creation
{
	/// <summary>
	/// The prospector finds aspects of a type that require initialization during construction of a fub.
	/// This means constructor parameters, when applicable, and any mutable fields and properties.
	/// The returned values are called 'prospects', prospective parameters/properties/fields.
	/// </summary>
	internal interface IProspector
	{
		/// <summary>
		/// Finds all prospective properties and fields of the given type that should be initialized for the fub.
		/// This will only include fields and properties that are public and settable.
		/// </summary>
		IEnumerable<MemberProspect> GetMemberProspects(Type type);

		/// <summary>
		/// Finds all prospective parameters of the given constructor that should be initialized for the fub.
		/// </summary>
		IEnumerable<ParameterProspect> GetParameterProspects(Type type, ConstructorInfo constructor);
	}
}
