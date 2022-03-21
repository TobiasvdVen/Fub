using Fub.ValueProvisioning;
using System;

namespace Fub.Creation
{
	/// <summary>
	/// The ICreator does the heavy lifting for the public Fubber or static Fub APIs, creating an instance 
	/// of the required type and ensuring that overrided members are initialized properly.
	/// </summary>
	public interface ICreator
	{
		/// <summary>
		/// Create an instance of type T with all prospects initialized to default values.
		/// </summary>
		T Create<T>() where T : notnull;

		/// <summary>
		/// Create an instance of the given type with all prospects initialized to default values.
		/// </summary>
		object Create(Type type);

		/// <summary>
		/// Create an instance of type T, providing specific values for all or some prospects.
		/// </summary>
		T Create<T>(ProspectValues prospectValues) where T : notnull;

		/// <summary>
		/// Create an instance of the given type, providing specific values for all or some prospects.
		/// </summary>
		object Create(Type type, ProspectValues prospectValues);
	}
}
