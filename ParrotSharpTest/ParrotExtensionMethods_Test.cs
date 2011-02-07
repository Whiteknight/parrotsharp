using System;
using NUnit.Framework;

namespace ParrotSharpTest
{
	[TestFixture]
	public class ParrotExtensionMethods_Test
	{
		[Test()]
		public void String_ToParrotStringPMC()
		{			
			string exename = AppDomain.CurrentDomain.FriendlyName;
			Parrot parrot = new Parrot(Parrot_Test.ParentInterpreter, exename);
			
			IParrot_PMC box = "This is a parrot string".ToParrotStringPMC(parrot);
			Assert.AreEqual("This is a parrot string", box.ToString(), "Can't box string into a PMC");
		}
	}
}

