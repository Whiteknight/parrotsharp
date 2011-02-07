using System;

namespace ParrotSharp.Pmc
{
	public class CallContext : Parrot_PMC, IParrot_PMC, IRole_Hash, IRole_Array
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
		
		#region Get/Set Parameters

		public new IParrot_PMC this[int key]
		{
			get { return new Parrot_PMC(this.Parrot, base[key]); }
			set { base[key] = value.RawPointer; }
		}

		public new IParrot_PMC this[string key]
		{
			get { return new Parrot_PMC(this.Parrot, base[key]); }
			set { base[key] = value.RawPointer; }
		}

		public new IParrot_PMC this[Parrot_String key]
		{
			get { return new Parrot_PMC(this.Parrot, base[key]); }
			set { base[key] = value.RawPointer; }
        }
		
		public void AddArgument(IParrot_PMC arg)
		{
			this[ArgNum] = arg;
			ArgNum++;
		}
		
		#endregion
		
		#region Signature String

		public Parrot_String Signature
		{
			get { return ParrotStringValue; }
			set { ParrotStringValue = value; }
		}
		
		#endregion

	}
}

