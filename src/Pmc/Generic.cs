using System;

namespace ParrotSharp
{
	public class Generic : Parrot_PMC
	{
		public Generic (Parrot parrot, IntPtr pmc_ptr) : base(parrot, pmc_ptr) { }
	}
}

