using System;

namespace ParrotSharp.Pmc
{
	public class Sub : Parrot_PMC
	{
		public Sub (Parrot parent, IntPtr ptr) : base(parent, ptr) { }
		
		public IParrot_PMC[] Invoke(string sig, params IParrot_PMC args)
		{
		}
		
		public static IPMCFactory GetFactory(Parrot parrot)
		{
			return new PMCFactory<Sub>(parrot, "Sub");
		}		
	}
}

