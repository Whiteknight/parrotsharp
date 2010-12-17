using System;
using ParrotSharp;

namespace ParrotTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string exename = AppDomain.CurrentDomain.FriendlyName;
			if (args.Length <= 0) {
				Console.WriteLine("No PBC file specified");
				return;
			}
			string pbcfile = args[0];
			string[] pbcargs = new string[args.Length - 1];
			for (int i = 1; i < args.Length; i++)
				pbcargs[i - 1] = args[i];
			Parrot parrot = new Parrot(exename);
			try {
				Parrot_PMC pbc = parrot.LoadBytecodeFile(pbcfile);
				//Parrot_PMC mainargs = parrot.GetParrotArgArray(pbcargs);
				Parrot_PMC mainargs = parrot.PmcNull;
				parrot.RunBytecode(pbc, mainargs);
			} catch (ParrotException ex) {
				Console.WriteLine("Main: " + ex.Message + "\n" + ex.StackTrace);
			}
		}
	}
}

