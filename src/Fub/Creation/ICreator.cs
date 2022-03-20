using Fub.ValueProvisioning;
using System;

namespace Fub.Creation
{
	public interface ICreator
	{
		T Create<T>() where T : notnull;
		object Create(Type type);
		T Create<T>(IProspectValues memberValues) where T : notnull;
		object Create(Type type, IProspectValues memberValues);
	}
}
