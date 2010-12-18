using System;

namespace ParrotSharp.Pmc
{
	public class CallSignature : Parrot_PMC, IParrot_PMC
	{
		public CallSignature (Parrot parrot, IntPtr ptr) : base(parrot, ptr) {}
	}
}

