using System;
namespace ParrotSharp
{
	public interface IPMCFactory<TPmc> where TPmc : class, IParrot_PMC, new()
	{
		TPmc Instance();
		TPmc SpecifyType(Parrot_PMC pmc);
	}
}

