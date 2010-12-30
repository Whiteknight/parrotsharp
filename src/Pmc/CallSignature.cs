using System;

namespace ParrotSharp.Pmc
{
	public class CallContext : Parrot_PMC, IParrot_PMC
	{
		int ArgNum = 0;
		
		public CallContext (Parrot parrot, IntPtr ptr) : base(parrot, ptr) {}
		
		public static IPMCFactory<CallContext> GetFactory(Parrot parrot)
		{
			return new PMCFactory<CallContext>(parrot, "CallContext");
		}
		
		public Parrot_String Signature {
			get { return StringValue; }
			set { StringValue = value; }
		}
		
		public void AddArgument(Parrot_PMC arg) {
			this[ArgNum] = arg;
			ArgNum++;
		}
	}
}

