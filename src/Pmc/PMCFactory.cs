using System;
namespace ParrotSharp
{
	public class PMCFactory<TPmc> : IPMCFactory, IPMCFactory<TPmc>
		where TPmc : class, IParrot_PMC
	{
		#region Private Fields
		
		private Parrot parrot;
		private Pmc.Class pmc_class;
		
		#endregion
		
		#region Constructors
		
		public PMCFactory (Parrot parrot, string key)
		{
			this.parrot = parrot;
			this.pmc_class = Pmc.Class.GetClassPMC(this.parrot, key.ToParrotStringPMC(parrot));
		}
		
		public PMCFactory (Parrot parrot, string[] key)
		{
			this.parrot = parrot;
			this.pmc_class = Pmc.Class.GetClassPMC(parrot, key.ToParrotStringArray(parrot));
		}
		
		#endregion
		
		#region Methods
		
		// Create a new PMC and a new C# wrapper object of the correct type.
		public TPmc Instance()
		{
			return this.Instance(this.parrot.PmcNull);
		}
		
		public TPmc Instance(IParrot_PMC init)
		{
			IntPtr pmc_ptr = this.pmc_class.InstantiatePointer(init);
			return (TPmc)Activator.CreateInstance(typeof(TPmc), this.parrot, pmc_ptr);
		}		
		
		// Create an instance of a C# wrapper class for an existing generic PMC object
		public TPmc Cast(IParrot_PMC pmc)
		{
			return (TPmc)Activator.CreateInstance(typeof(TPmc), this.parrot, pmc.RawPointer);
		}
		
		// Create an instance of a C# wrapper class for an existing generic PMC pointer
		public TPmc Cast(IntPtr pmc_ptr)
		{
			return (TPmc)Activator.CreateInstance(typeof(TPmc), this.parrot, pmc_ptr);
		}		
		
		#endregion
	}
}

