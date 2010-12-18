using System;

namespace ParrotSharp.Pmc
{
	public class CallContext : Parrot_PMC, IParrot_PMC
	{
		public CallContext (Parrot parrot, IntPtr ptr) : base(parrot, ptr) {}
		
		public static IPMCFactory<CallContext> GetFactory(Parrot parrot)
		{
			return new PMCFactory<CallContext>(parrot, "CallContext");
		}
	}
}

