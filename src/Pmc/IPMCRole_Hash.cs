using System;

namespace ParrotSharp.Pmc
{
	public interface IPMCRole_Hash : IParrot_PMC
	{
		IParrot_PMC this[string arg] { get; set; }
		IParrot_PMC this[Parrot_String arg] { get; set; }
	}
}

