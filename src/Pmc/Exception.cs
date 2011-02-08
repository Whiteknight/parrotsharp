using System;

namespace ParrotSharp.Pmc
{
	public class Exception : Parrot_PMC
	{
		public Exception (Parrot parrot, IntPtr ptr) : base(parrot, ptr) { }
	}
}

