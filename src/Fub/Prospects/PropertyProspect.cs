using Fub.Utilities;
using System.Reflection;

namespace Fub.Prospects
{
	internal class PropertyProspect : MemberProspect
	{
		public PropertyProspect(PropertyInfo propertyInfo) : base(propertyInfo.PropertyType, propertyInfo.IsNullable(), propertyInfo)
		{
			PropertyInfo = propertyInfo;
		}

		public PropertyInfo PropertyInfo { get; }

		public override void SetValue(object target, object? value)
		{
			PropertyInfo.SetValue(target, value);
		}
	}
}
