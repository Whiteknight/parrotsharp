using System;
using NUnit.Framework;
using ParrotSharp;
using ParrotSharp.Pmc;
using String = ParrotSharp.Pmc.String;

namespace ParrotSharpTest
{
	[TestFixture]
	public class Parrot_String_Test
	{
		[Test]
		public void ParseIntegerValue()
		{
			String s = String.GetFactory(TestManager.Interp).Instance();
			s.StringValue = "127";
			Integer intpmc = s.ToIntegerPMC(10);
			Assert.AreEqual(127, intpmc.IntegerValue);
		}
		
		[Test]
		public void Replace()
		{
			String s = String.GetFactory(TestManager.Interp).Instance();
			s.StringValue = "FooBarBooCar";
			s.Replace("oo", "X");
			Assert.AreEqual("FXBarBXCar", s.ToString());
		}		
	}
}

