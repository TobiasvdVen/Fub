using System.Reflection;

namespace Fub.Prospects
{
	internal class FieldProspect : MemberProspect
	{
		public FieldProspect(FieldInfo fieldInfo) : base(fieldInfo.FieldType, fieldInfo.IsNullable(), fieldInfo)
		{
		}
	}
}
