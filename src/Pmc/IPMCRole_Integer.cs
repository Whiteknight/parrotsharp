using System;

namespace ParrotSharp.Pmc
{
	public interface IPMCRole_Integer : IParrot_PMC
	{
		int IntegerValue { get; set; }
	}
}

