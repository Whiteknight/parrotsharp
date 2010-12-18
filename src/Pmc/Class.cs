using System;
using System.Runtime.InteropServices;
namespace ParrotSharp.Pmc
{
	public class Class : Parrot_PMC, IParrot_PMC
	{
		public Class (Parrot parrot, IntPtr ptr) : base(parrot, ptr) {}
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_get_class(IntPtr interp, IntPtr key, out IntPtr class_pmc);
		
		public static Class GetClassPMC(Parrot parrot, Parrot_PMC key)
		{
			IntPtr class_ptr = IntPtr.Zero;
			int result = Parrot_api_pmc_get_class(parrot.RawPointer, key.RawPointer, out class_ptr);
			if (result != 1)
				parrot.GetErrorResult();
			// This could be a Class or a PMCProxy, but for our purposes we treat them the same.
			return new Class(parrot, class_ptr);
		}
		
		public static IPMCFactory<Class> GetFactory()
		{
			return new PMCFactory<Class>(this.Parrot, "Class");
		}
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_new_from_class(IntPtr interp, IntPtr class_pmc, IntPtr init, out IntPtr pmc);
		
		public IntPtr InstantiatePointer(IParrot_PMC init)
		{
			IntPtr pmc_ptr = IntPtr.Zero;
			int result = Parrot_api_pmc_new_from_class(this.Parrot.RawPointer, this.RawPointer, init.RawPointer, out pmc_ptr);
			if (result != 1)
				this.Parrot.GetErrorResult();
			return pmc_ptr;
		}		
		
		public Parrot_PMC Instantiate()
		{
			return new Parrot_PMC(this.Parrot, this.InstantiatePointer(this.Parrot.PmcNull));
		}
		
		public Parrot_PMC Instantiate(IParrot_PMC init)
		{
			return new Parrot_PMC(this.Parrot, this.InstantiatePointer(init));
		}		
	}
}

