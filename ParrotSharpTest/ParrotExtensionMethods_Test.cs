using System;
using NUnit.Framework;
using ParrotSharp;

namespace ParrotSharpTest
{
	[TestFixture]
	public class ParrotExtensionMethods_Test
	{
		[Test()]
		public void String_ToParrotStringPMC()
		{			
			IParrot_PMC box = "This is a parrot string".ToParrotStringPMC(TestManager.Interp);
			Assert.AreEqual("This is a parrot string", box.ToString(), "Can't box string into a PMC");
		}
	}
}

