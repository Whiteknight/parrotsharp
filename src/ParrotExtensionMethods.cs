using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ParrotSharp
{
	public static class ParrotExtensionMethods
	{
		#region Wrap String Array
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_wrap_string_array(IntPtr interp, int argc, IntPtr[] argv, out IntPtr array);

		public static Parrot_PMC ToParrotStringArray(this string[] args, Parrot parrot)
		{
			IntPtr[] argv = new IntPtr[args.Length];
			for (int i = 0; i < args.Length; i++) 
				argv[i] = Marshal.StringToHGlobalAnsi(args[i]);
			IntPtr pmc = IntPtr.Zero;
			int result = Parrot_api_pmc_wrap_string_array(parrot.RawPointer, args.Length, argv, out pmc);
			if (result != 1)
				parrot.GetErrorResult();
			return new Parrot_PMC(parrot, pmc);
		}
		
		#endregion
		
		#region Box C# String to Parrot String
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_box_string(IntPtr interp, IntPtr str, out IntPtr pmc);
		
		public static IParrot_PMC ToParrotStringPMC(this string arg, Parrot parrot)
		{
			IntPtr pmc_ptr = IntPtr.Zero;
			Parrot_String str = new Parrot_String(parrot, arg);
			int result = Parrot_api_pmc_box_string(parrot.RawPointer, str.RawPointer, out pmc_ptr);
			if (result != 1)
				parrot.GetErrorResult();
			return new Pmc.String(parrot, pmc_ptr);
		}
		
		public static Parrot_String ToParrotString(this string arg, Parrot parrot)
		{
			return new Parrot_String(parrot, arg);
		}
		
		#endregion
		
		#region Box C# Integer to Parrot Integer PMC
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_box_integer(IntPtr interp, int value, out IntPtr pmc);
		
		public static IParrot_PMC ToParrotIntegerPMC(this int arg, Parrot parrot)
		{
			IntPtr pmc_ptr = IntPtr.Zero;
			int result = Parrot_api_pmc_box_integer(parrot.RawPointer, arg, out pmc_ptr);
			if (result != 1)
				parrot.GetErrorResult();
			return new Pmc.Integer(parrot, pmc_ptr);
		}		
		
		#endregion
	}
}
