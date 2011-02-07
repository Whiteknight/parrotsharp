using NUnit.Framework;
using ParrotSharp;
using ParrotSharp.Pmc;
using System;

namespace ParrotSharpTest
{
	[TestFixture()]  
	public class Parrot_PMC_Test
	{
		
		[Test()]
		public void InvokeMethod()
		{
			IParrot_PMC pmc_string = "This is a parrot string".ToParrotStringPMC(TestManager.Interp);
			IPMCFactory<CallContext> sign_factory = CallContext.GetFactory(TestManager.Interp);
			CallContext sign = sign_factory.Instance();
			sign.Signature = "PiSS->".ToParrotString(TestManager.Interp);
			sign.AddArgument(pmc_string);
			sign.AddArgument("string".ToParrotStringPMC(TestManager.Interp));
			sign.AddArgument("pmc".ToParrotStringPMC(TestManager.Interp));
			
			pmc_string.InvokeMethod("replace".ToParrotString(TestManager.Interp), sign);
			Assert.AreEqual("This is a parrot pmc", pmc_string.ToString(), "Error when invoking a method");
		}
		
		[Test()]
		[ExpectedException(typeof(ParrotException))]
		public void InvokeUnexistingMethod()
		{
			IParrot_PMC pmc_string = "This is a parrot string".ToParrotStringPMC(TestManager.Interp);
			IPMCFactory<CallContext> sign_factory = CallContext.GetFactory(TestManager.Interp);
			CallContext sign = sign_factory.Instance();
			sign.Signature = "Pi->".ToParrotString(TestManager.Interp);
			pmc_string.InvokeMethod("unexisting_method".ToParrotString(TestManager.Interp), sign);
		}		
	}	
}
