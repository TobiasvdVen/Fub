using Fub.Prospects;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fub.Creation
{
	internal interface IProspector
	{
		IEnumerable<MemberProspect> GetProspects(Type type);
		IEnumerable<Prospect> GetProspects(Type type, ConstructorInfo constructor);
	}
}
