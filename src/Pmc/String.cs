using System;
using System.Runtime.InteropServices;

namespace ParrotSharp.Pmc
{
	public class String : Parrot_PMC, IParrot_PMC
	{
		public String (Parrot parrot, IntPtr ptr) : base(parrot, ptr) {}
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_box_string(IntPtr interp, IntPtr str, out IntPtr pmc);
		
		public static String BoxParrotString(Parrot parrot, Parrot_String str)
		{
			IntPtr pmc_ptr = IntPtr.Zero;
			int result = Parrot_api_pmc_box_string(parrot.RawPointer, str.RawPointer, out pmc_ptr);
			if (result != 1)
				parrot.GetErrorResult();
			return new String(parrot, pmc_ptr);
		}		
		
		public static IPMCFactory<String> GetFactory(Parrot parrot)
		{
			return new PMCFactory<String>(parrot, "String");
		}
		
		public IParrot_PMC ToIntegerPMC(int int_base)
		{
			CallContext cc = CallContext.GetFactory(this.parrot);
			cc[0] = this;
			cc[1] = int_base.ToParrotIntegerPMC(this.Parrot);
			cc.SetParrotString("PiI->P");
			// TODO: get the method
			// TODO: Invoke it
			// TODO: get the returns
		}		
	}
}

