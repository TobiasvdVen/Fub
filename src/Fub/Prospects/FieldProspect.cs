using System.Reflection;

namespace Fub.Prospects
{
	internal class FieldProspect : MemberProspect
	{
		public FieldProspect(FieldInfo fieldInfo) : base(ProspectInitialization.Field, fieldInfo.FieldType, fieldInfo.IsNullable(), fieldInfo)
		{
		}
	}
}
