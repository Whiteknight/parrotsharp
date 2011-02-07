using System;
using System.Runtime.InteropServices;

namespace ParrotSharp.Pmc
{
	// String PMC type (Not Parrot_String)
	public class String : Parrot_PMC, IParrot_PMC, IPMCRole_String
	{
		#region Constructor
		
		public String (Parrot parrot, IntPtr ptr) : base(parrot, ptr) {}
		
		#endregion
		
		#region Box Parrot_String
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_box_string(IntPtr interp, IntPtr str, out IntPtr pmc);
		
		public static String BoxParrotString(Parrot parrot, Parrot_String str)
		{
			IntPtr pmc_ptr = IntPtr.Zero;
			int result = Parrot_api_pmc_box_string(parrot.RawPointer, str.RawPointer, out pmc_ptr);
			if (result != 1)
				parrot.GetErrorResult();
			return new String(parrot, pmc_ptr);
		}
		
		#endregion
		
		#region IPMCFactory
		
		public static IPMCFactory<String> GetFactory(Parrot parrot)
		{
			return new PMCFactory<String>(parrot, "String");
		}
		
		#endregion
		
		#region Parrot String PMC Methods
		
		public Integer ToIntegerPMC(int int_base)
		{
			CallContext cc = CallContext.GetFactory(this.Parrot).Instance();
			cc.StringValue = "PiI->P";
			cc.AddArgument(this);
			cc.AddArgument(int_base.ToParrotIntegerPMC(this.Parrot));
			this.InvokeMethod("to_int".ToParrotString(this.Parrot), cc);
			return Integer.GetFactory(this.Parrot).Cast(cc[0]);
		}
		
		public void Replace(string old_s, string new_s)
		{
			CallContext cc = CallContext.GetFactory(this.Parrot).Instance();
			cc.StringValue = "PiSS->";
			cc.AddArgument(this);
			cc.AddArgument(old_s.ToParrotStringPMC(this.Parrot));
			cc.AddArgument(new_s.ToParrotStringPMC(this.Parrot));
			this.InvokeMethod("replace".ToParrotString(this.Parrot), cc);
		}
		
		#endregion
		
		#region IPMCRole_String
		
		public new Parrot_String ParrotStringValue
		{
			get { return base.ParrotStringValue; }
			set { base.ParrotStringValue = value; }
		}
		
		public string StringValue
		{
			get { return base.ParrotStringValue.ToString(); }
			set { base.ParrotStringValue = value.ToParrotString(this.Parrot); }
		}		
		
		#endregion 
	}
}

