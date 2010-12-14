using System;
using System.Runtime.InteropServices;

namespace Parrot
{
    public class Parrot_PMC : ParrotPointer
	{
        public Parrot_PMC(Parrot parrot, IntPtr ptr) : base(parrot, ptr) { }

		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_get_string(IntPtr interp, IntPtr pmc, out IntPtr str);

		public Parrot_String GetParrotString()
		{
			IntPtr str = IntPtr.Zero;
			int result = Parrot_api_pmc_get_string(this.Parrot.RawPointer, this.RawPointer, out str);
			if (result != 1)
				this.Parrot.GetErrorResult();
			return new Parrot_String(this.Parrot, str);
		}

		public override string ToString()
		{
			return this.GetParrotString().ToString();
		}
	}
}
