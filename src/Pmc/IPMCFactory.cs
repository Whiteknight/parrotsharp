using System;
namespace ParrotSharp
{
	// TODO: Provide an interface for Unity, or other factory/service-locator
	public interface IPMCFactory<TPmc> where TPmc : class, IParrot_PMC
	{
		TPmc Instance();
		TPmc Instance(IParrot_PMC intt);
		TPmc Cast(IParrot_PMC pmc);
		TPmc Cast(IntPtr pmc_ptr);
	}
}

