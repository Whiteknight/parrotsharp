using System;

namespace ParrotSharp.Pmc
{
	public class CallContext : Parrot_PMC, IParrot_PMC,
		IPMCRole_Hash, IPMCRole_Array, IPMCRole_String
	{
		#region Private Fields
		
		private int ArgNum = 0;
		
		#endregion
		
		#region Constructor

		public CallContext (Parrot parrot, IntPtr ptr) : base(parrot, ptr) {}
		
		#endregion
		
		#region IPMCFactory>

		public static IPMCFactory<CallContext> GetFactory(Parrot parrot)
		{
			return new PMCFactory<CallContext>(parrot, "CallContext");
		}
		
		#endregion
		
		#region Get/Set Parameters IPMCRole_Array and IPMCRole_Hash

		public new IParrot_PMC this[int key]
		{
			get { return new Generic(this.Parrot, base[key]); }
			set { base[key] = value.RawPointer; }
		}

		public new IParrot_PMC this[string key]
		{
			get { return new Generic(this.Parrot, base[key]); }
			set { base[key] = value.RawPointer; }
		}

		public new IParrot_PMC this[Parrot_String key]
		{
			get { return new Generic(this.Parrot, base[key]); }
			set { base[key] = value.RawPointer; }
        }
		
		public void AddArgument(IParrot_PMC arg)
		{
			this[this.ArgNum] = arg;
			this.ArgNum++;
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

