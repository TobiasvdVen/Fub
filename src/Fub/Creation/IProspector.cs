using Fub.Prospects;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fub.Creation
{
	internal interface IProspector
	{
		IEnumerable<MemberProspect> GetMemberProspects(Type type);
		IEnumerable<MemberProspect> GetMutableMemberProspects(Type type);

		IEnumerable<Prospect> GetProspects(Type type, ConstructorInfo constructor);
		IEnumerable<ParameterProspect> GetParameterProspects(Type type, ConstructorInfo constructor);
	}
}
