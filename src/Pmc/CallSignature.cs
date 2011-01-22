using System;

namespace ParrotSharp.Pmc
{
	public class CallContext : Parrot_PMC, IParrot_PMC, IRole_Hash, IRole_Array
	{
		public CallContext (Parrot parrot, IntPtr ptr) : base(parrot, ptr) {}
		
		public static IPMCFactory<CallContext> GetFactory(Parrot parrot)
		{
			return new PMCFactory<CallContext>(parrot, "CallContext");
		}
		
		public IParrot_PMC this[int key] 
		{
			get { return new Parrot_PMC(this.Parrot, base[key]); }
			set { base[key] = value.RawPointer; }
		}
		
		public IParrot_PMC this[string key] 
		{
			get { return new Parrot_PMC(this.Parrot, base[key]); }
			set { base[key] = value.RawPointer; }
		}
		
		public IParrot_PMC this[Parrot_String key] 
		{
			get { return new Parrot_PMC(this.RawPointer, base[key]); }
			set { base[key] = value.RawPointer; }
		}
	}
}

