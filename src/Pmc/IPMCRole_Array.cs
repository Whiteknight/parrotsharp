using System;

namespace ParrotSharp.Pmc
{
	public interface IPMCRole_Array : IParrot_PMC
	{
		IParrot_PMC this[int arg] { get; set; }
		//int ElementsCount { get; }
	}
}

