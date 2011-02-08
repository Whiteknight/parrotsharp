using System;

namespace ParrotSharp.Pmc
{
	public class Interpreter : Parrot_PMC
	{
		public Interpreter (Parrot parrot, IntPtr ptr) : base (parrot, ptr) { }
	}
}

