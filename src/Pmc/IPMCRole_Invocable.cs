using System;
namespace ParrotSharp
{
	public interface IPMCRole_Invocable
	{
		IParrot_PMC[] Invoke(IParrot_PMC signature);
		IParrot_PMC[] Invoke(string sig, params IParrot_PMC[] args);
	}
}

