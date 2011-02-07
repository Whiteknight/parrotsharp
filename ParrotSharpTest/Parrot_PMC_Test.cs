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
		public void BoxString()
		{			
			string exename = AppDomain.CurrentDomain.FriendlyName;
			Parrot parrot = new Parrot(Parrot_Test.ParentInterpreter, exename);
			
			IParrot_PMC box = "This is a parrot string".ToParrotStringPMC(parrot);
			Assert.AreEqual("This is a parrot string", box.ToString(), "Can't box string into a PMC");
		}
		
		[Test()]
		public void InvokeMethod()
		{
			string exename = AppDomain.CurrentDomain.FriendlyName;
			Parrot parrot = new Parrot(Parrot_Test.ParentInterpreter, exename);
			
			IParrot_PMC pmc_string = "This is a parrot string".ToParrotStringPMC(parrot);
			IPMCFactory<CallContext> sign_factory = CallContext.GetFactory(parrot);
			CallContext sign = sign_factory.Instance();
			sign.Signature = "PiSS->".ToParrotString(parrot);
			sign.AddArgument(pmc_string);
			sign.AddArgument("string".ToParrotStringPMC(parrot));
			sign.AddArgument("pmc".ToParrotStringPMC(parrot));
			
			pmc_string.InvokeMethod("replace".ToParrotString(parrot), sign);
			Assert.AreEqual("This is a parrot pmc", pmc_string.ToString(), "Error when invoking a method");
		}
		
		[Test()]
		[ExpectedException(typeof(ParrotException))]
		public void InvokeUnexistingMethod()
		{
			string exename = AppDomain.CurrentDomain.FriendlyName;
			Parrot parrot = new Parrot(Parrot_Test.ParentInterpreter, exename);
			
			IParrot_PMC pmc_string = "This is a parrot string".ToParrotStringPMC(parrot);
			IPMCFactory<CallContext> sign_factory = CallContext.GetFactory(parrot);
			CallContext sign = sign_factory.Instance();
			sign.Signature = "Pi->".ToParrotString(parrot);

			pmc_string.InvokeMethod("unexisting_method".ToParrotString(parrot), sign);
		}		
	}	
}
