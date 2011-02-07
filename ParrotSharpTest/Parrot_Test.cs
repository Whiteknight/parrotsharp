using System;
using NUnit.Framework;
using ParrotSharp;
using ParrotSharp.Pmc;

namespace ParrotSharpTest
{
	[TestFixture()]  
	public class Parrot_Test
	{
		
		private static Parrot ParrotParent = null;
		
		private static Parrot ParentInterpreter
		{
			get {
				if (ParrotParent != null)
					return ParrotParent;
				else {
					ParrotParent = new Parrot();
					return ParrotParent;
				}
			}
		}
		
		[Test]
		public void CreateParrot()
		{
			Parrot parrot = new Parrot();
			Assert.AreNotEqual(IntPtr.Zero, parrot.RawPointer);
		}
		
		[Test]
		public void CreateParrot_WithParentAndName()
		{
			string exename = AppDomain.CurrentDomain.FriendlyName;
			Parrot parrot = new Parrot(ParentInterpreter, exename);
			Assert.AreNotEqual(IntPtr.Zero, parrot.RawPointer, "Unable to create a new Parrot interpreter");
		}	
	}
}