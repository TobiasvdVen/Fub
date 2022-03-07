using System.Reflection;

namespace Fub.Prospects
{
	internal class PropertyProspect : MemberProspect
	{
		public PropertyProspect(PropertyInfo propertyInfo) : base(ProspectInitialization.Property, propertyInfo.PropertyType, propertyInfo)
		{
		}
	}
}
