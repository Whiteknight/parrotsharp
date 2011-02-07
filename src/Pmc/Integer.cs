using System;

namespace ParrotSharp.Pmc
{
	public class Integer : Parrot_PMC, IParrot_PMC, IPMCRole_Integer
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
		
		#region IPMC_Integer
		
		public new int IntegerValue
		{
			get { return base.IntegerValue; }
			set { base.IntegerValue = value; }
		}		
		
		#endregion
	}
}

