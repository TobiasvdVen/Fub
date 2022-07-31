using Fub.Creation.ConstructorResolvers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fub.Creation
{
	internal class DefaultConstructorRegistrar
	{
		public void Register(ConstructorResolverFactory factory)
		{
			factory.RegisterConstructor(typeof(string), typeof(string).GetConstructors().FirstOrDefault() ?? typeof(string).GetConstructor(new Type[] { typeof(char[]) })!);
			factory.RegisterConstructor(typeof(decimal), typeof(decimal).GetConstructor(new Type[] { typeof(Int32) })!);
			factory.RegisterConstructor(typeof(IntPtr), typeof(IntPtr).GetConstructor(new Type[] { typeof(Int32) })!);
			factory.RegisterConstructor(typeof(UIntPtr), typeof(UIntPtr).GetConstructor(new Type[] { typeof(UIntPtr) })!);
			factory.RegisterConstructor(typeof(DateTime), typeof(DateTime).GetConstructor(new Type[] { typeof(Int64) })!);
			factory.RegisterConstructor(typeof(Guid), typeof(Guid).GetConstructor(new Type[] { typeof(uint), typeof(ushort), typeof(ushort), typeof(byte), typeof(byte), typeof(byte), typeof(byte), typeof(byte), typeof(byte), typeof(byte), typeof(byte) })!);

			factory.RegisterResolver(typeof(List<>), new ListConstructorResolver());
			factory.RegisterResolver(typeof(HashSet<>), new HashSetConstructorResolver());
			factory.RegisterResolver(typeof(Dictionary<,>), new DictionaryConstructorResolver());
		}
	}
}
