using System;
using NUnit.Framework;
using ParrotSharp;
using ParrotSharp.Pmc;

namespace ParrotSharpTest
{
	[TestFixture]
	public class Integer_Test
	{
		[Test]
		public void CreateIntegerPMC()
		{
			Integer intpmc = Integer.GetFactory(TestManager.Interp).Instance();
			Assert.AreNotEqual(IntPtr.Zero, intpmc.RawPointer);
		}
		
		[Test]
		public void GetAndSetIntegerValue()
		{
			Integer intpmc = Integer.GetFactory(TestManager.Interp).Instance();
			//Assert.AreEqual(0, intpmc.IntegerValue);
			intpmc.IntegerValue = 7;
			Assert.AreEqual(7, intpmc.IntegerValue);
		}		
	}
}

