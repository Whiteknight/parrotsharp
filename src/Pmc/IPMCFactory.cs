using System;
namespace ParrotSharp
{
	public interface IPMCFactory<TPmc> where TPmc : class, IParrot_PMC
	{
		TPmc Instance();
		TPmc SpecifyType(Parrot_PMC pmc);
	}
}

