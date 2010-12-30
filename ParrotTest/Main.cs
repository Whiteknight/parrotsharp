using System;
using ParrotSharp;
using ParrotSharp.Pmc;
using System.Runtime.InteropServices;

namespace ParrotTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string exename = AppDomain.CurrentDomain.FriendlyName;
			Parrot parrot = new Parrot(exename);
			if (args.Length <= 0) {
				Console.WriteLine("No PBC file specified, run tests using test.pbc");
				Test(parrot);
				return;
			}
			string pbcfile = args[0];
			string[] pbcargs = new string[args.Length - 1];
			for (int i = 1; i < args.Length; i++)
				pbcargs[i - 1] = args[i];
			try {
				Parrot_PMC pbc = parrot.LoadBytecodeFile(pbcfile);
				Parrot_PMC mainargs = parrot.GetParrotArgArray(pbcargs);
				
				//Parrot_PMC mainargs = parrot.PmcNull;
				parrot.RunBytecode(pbc, mainargs);
			} catch (ParrotException ex) {
				Console.WriteLine("Main: " + ex.Message + "\n" + ex.StackTrace);
			}
		}
		
		//[DllImport("parrot")]
		//private static extern int Parrot_api_ready_bytecode(IntPtr interp_pmc, IntPtr pbc, out IntPtr main_sub);
		
		static private void Test(Parrot parrot)
		{
			try {
				Parrot_PMC pbc = parrot.LoadBytecodeFile("test.pbc");
				parrot.RunBytecode(pbc, parrot.PmcNull);
				//IntPtr main_sub;
				//Parrot_api_ready_bytecode(parrot.RawPointer, pbc.RawPointer, out main_sub);
				Class testClass = Class.GetClassPMC(parrot, "ClassTest".ToParrotStringPMC(parrot));
				Parrot_PMC testPmc = testClass.Instantiate();
				CallContext sign = CallContext.GetFactory(parrot).Instance();
				sign.Signature = "Pi->".ToParrotString(parrot);
				sign.AddArgument(testPmc);
				testPmc.InvokeMethod("test2".ToParrotString(parrot), sign);
			} catch (ParrotException ex) {
				Console.WriteLine("Test: " + ex.Message + "\n" + ex.StackTrace);
			}
		}
	}
}

