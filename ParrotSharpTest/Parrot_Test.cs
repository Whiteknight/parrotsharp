using System;
using NUnit.Framework;
using ParrotSharp;
using ParrotSharp.Pmc;

namespace ParrotSharpTest
{
	[TestFixture()]  
	public class Parrot_Test
	{
		[Test]
		public void CreateParrot_WithParentAndName()
		{
			string exename = AppDomain.CurrentDomain.FriendlyName;
			Parrot parrot = new Parrot(TestManager.Interp, exename);
			Assert.AreNotEqual(IntPtr.Zero, parrot.RawPointer, "Unable to create a new Parrot interpreter");
		}	
	}
}