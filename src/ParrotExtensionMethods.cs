using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ParrotSharp
{
	public static class ParrotExtensionMethods
	{
		[DllImport("parrot")]
		private static extern int Parrot_api_build_argv_array(IntPtr interp, int argc, IntPtr[] argv, out IntPtr array);

		public static Parrot_PMC ToParrotStringArray(this string[] args, Parrot parrot)
		{
			IntPtr[] argv = new IntPtr[args.Length];
			for (int i = 0; i < args.Length; i++) 
				argv[i] = Marshal.StringToHGlobalAnsi(args[i]);
			IntPtr pmc = IntPtr.Zero;
			int result = Parrot_api_build_argv_array(parrot.RawPointer, args.Length, argv, out pmc);
			if (result != 1)
				parrot.GetErrorResult();
			return new Parrot_PMC(parrot, pmc);
		}
	}
}
