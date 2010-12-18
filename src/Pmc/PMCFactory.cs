using System;
namespace ParrotSharp
{
	public class PMCFactory<TPmc> where TPmc : class, IParrot_PMC, new()
	{
		private Parrot parrot;
		private Pmc.Class pmc_class;
		
		public PMCFactory (Parrot parrot, string key)
		{
			this.parrot = parrot;
			this.pmc_class = Pmc.Class.GetClassPMC(parrot, key.ToParrotStringPMC(parrot));
		}
		
		public PMCFactory (Parrot parrot, string[] key)
		{
			this.parrot = parrot;
			this.pmc_class = Pmc.Class.GetClassPMC(parrot, key.ToParrotStringArray(parrot));
		}
		
		public TPmc Instance()
		{
			IntPtr pmc_ptr = this.pmc_class.InstantiatePointer(this.parrot.PmcNull);
			return (TPmc)Activator.CreateInstance(typeof(TPmc), this.parrot, pmc_ptr);
		}
		
		public TPmc SpecifyType(Parrot_PMC pmc)
		{
			return (TPmc)Activator.CreateInstance(typeof(TPmc), this.parrot, pmc.RawPointer);
		}		
	}
}

