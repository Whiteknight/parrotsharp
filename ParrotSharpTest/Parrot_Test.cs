using System;
using NUnit.Framework;
using ParrotSharp;
using ParrotSharp.Pmc;

namespace ParrotSharpTest
{
	[TestFixture()]  
	public class Parrot_Test
	{
		
		static private Parrot ParrotParent = null;
		
		static public Parrot ParentInterpreter
		{
			get {
				if(ParrotParent != null) return ParrotParent;
				else {
					ParrotParent = new Parrot();
					return ParrotParent;
				}
			}
		}
		
		[Test()]
		public void CreateParrot()
		{
			string exename = AppDomain.CurrentDomain.FriendlyName;
			Parrot parrot = new Parrot(ParentInterpreter, exename);
			Assert.AreNotEqual(IntPtr.Zero, parrot.RawPointer, "Unable to create a new Parrot interpreter");
		}
		
	}
}