using System;
namespace ParrotSharp.Pmc
{
	public class Integer : Parrot_PMC, IParrot_PMC
	{
		#region Constructor
		
		public Integer (Parrot parrot, IntPtr ptr) : base(parrot, ptr) { } 
		
		#endregion
		
		#region IPMCFactory
		
		public static IPMCFactory<Integer> GetFactory(Parrot parrot)
		{
			return new PMCFactory<Integer>(parrot, "Integer");
		}
		
		#endregion
	}
}

