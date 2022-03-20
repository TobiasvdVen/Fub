using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fub.ValueProvisioning.ValueProviders
{
	public class FactoryMethodProvider<T> : GenericProvider<T>
	{
		private readonly Func<T> factoryMethod;

		public FactoryMethodProvider(Func<T> factoryMethod)
		{
			this.factoryMethod = factoryMethod;
		}

		public override T GetValue()
		{
			return factoryMethod();
		}
	}
}
